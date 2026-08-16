using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace JadooTravel.Services.LanguageServices;

public class LanguageService : ILanguageService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _apiKey;
    private readonly Dictionary<string, Dictionary<string, string>> _translations = [];
    private readonly HashSet<string> _inProgress = [];
    private readonly List<string> _pendingKeys = [];
    private Task? _batchTask = null;
    private readonly object _lock = new();
    private static readonly HttpClient _httpClient = new();

    public LanguageService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _apiKey = configuration["GeminiApiKey"] ?? string.Empty;
        
        try
        {
            var filePath = GetFilePath();
            Console.WriteLine($"[LanguageService] Çeviri dosyası yolu: {filePath}");
            
            if (File.Exists(filePath))
            {
                var jsonContent = File.ReadAllText(filePath);
                var data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(jsonContent);
                if (data != null)
                {
                    _translations = data;
                    Console.WriteLine($"[LanguageService] Sözlük başarıyla yüklendi. Kelime sayısı: {_translations.Count}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[LanguageService] Sözlük yükleme hatası: {ex.Message}");
        }
    }

    private string GetFilePath()
    {
        return Path.Combine(Directory.GetCurrentDirectory(), "Resources", "translations.json");
    }

    public string CurrentLanguage
    {
        get
        {
            var context = _httpContextAccessor.HttpContext;
            if (context != null && context.Request.Cookies.TryGetValue("SelectedLanguage", out var lang))
            {
                return lang.ToUpper();
            }
            return "TR";
        }
    }

    public string T(string key, string? defaultValue = null)
    {
        if (string.IsNullOrEmpty(key)) return string.Empty;
        
        var cleanKey = Regex.Replace(key.Trim(), @"\s+", " ");
        var lang = CurrentLanguage;

        lock (_lock)
        {
            if (_translations.TryGetValue(cleanKey, out var dict))
            {
                if (dict.TryGetValue(lang, out var val) && !string.IsNullOrEmpty(val))
                {
                    return val;
                }
                return defaultValue ?? cleanKey;
            }

            if (!_inProgress.Contains(cleanKey))
            {
                _inProgress.Add(cleanKey);
                
                if (!string.IsNullOrEmpty(_apiKey) && _apiKey != "YOUR_API_KEY_HERE")
                {
                    lock (_pendingKeys)
                    {
                        _pendingKeys.Add(cleanKey);
                        
                        if (_batchTask == null || _batchTask.IsCompleted)
                        {
                            _batchTask = Task.Delay(300).ContinueWith(_ => ProcessPendingTranslations());
                        }
                    }
                }
                else
                {
                    _translations[cleanKey] = new Dictionary<string, string>
                    {
                        { "TR", cleanKey },
                        { "EN", cleanKey },
                        { "ESP", cleanKey },
                        { "FR", cleanKey }
                    };
                    Task.Run(() => SaveTranslationsToFile());
                }
            }
        }

        return defaultValue ?? cleanKey;
    }

    private async Task ProcessPendingTranslations()
    {
        List<string> keysToTranslate;
        
        lock (_pendingKeys)
        {
            keysToTranslate = _pendingKeys.ToList();
            _pendingKeys.Clear();
        }

        if (keysToTranslate.Count == 0) return;

        try
        {
            Console.WriteLine($"[Gemini Batch] {keysToTranslate.Count} adet yeni kelime tek bir JSON parçasında toplanıp çeviriye gönderiliyor...");

            var prompt = "You are a professional translator. Translate the following list of Turkish texts into TR (Turkish), EN (English), ESP (Spanish), and FR (French). " +
                         "Output ONLY a raw JSON object where each key is the original Turkish text, and the value is a JSON object containing TR, EN, ESP, and FR translations. " +
                         "Do not include markdown code block formatting, backticks, or any explanations. " +
                         $"List of texts to translate: {JsonSerializer.Serialize(keysToTranslate)}";

            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                }
            };

            var jsonRequest = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync($"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.5-flash-lite:generateContent?key={_apiKey}", content);
            
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(responseString);
                
                var text = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                if (!string.IsNullOrEmpty(text))
                {
                    var cleanJson = text.Replace("```json", "").Replace("```", "").Trim();
                    var batchTranslations = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(cleanJson);
                    
                    if (batchTranslations != null)
                    {
                        lock (_lock)
                        {
                            foreach (var item in batchTranslations)
                            {
                                _translations[item.Key] = new Dictionary<string, string>
                                {
                                    { "TR", item.Value.GetValueOrDefault("TR", item.Key) },
                                    { "EN", item.Value.GetValueOrDefault("EN", item.Key) },
                                    { "ESP", item.Value.GetValueOrDefault("ESP", item.Key) },
                                    { "FR", item.Value.GetValueOrDefault("FR", item.Key) }
                                };
                            }
                        }
                        
                        await SaveTranslationsToFile();
                        Console.WriteLine($"[Gemini Batch] {batchTranslations.Count} adet kelime tek seferde başarıyla toplu çevrildi ve diske yazıldı!");
                    }
                }
            }
            else
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[Gemini Batch] API Hatası! Durum Kodu: {response.StatusCode}, Detay: {errorMsg}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Gemini Batch] Beklenmedik hata: {ex.Message}");
        }
        finally
        {
            lock (_lock)
            {
                foreach (var key in keysToTranslate)
                {
                    _inProgress.Remove(key);
                }
            }
        }
    }

    private async Task SaveTranslationsToFile()
    {
        try
        {
            var filePath = GetFilePath();
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string jsonString;
            lock (_lock)
            {
                jsonString = JsonSerializer.Serialize(_translations, new JsonSerializerOptions { WriteIndented = true });
            }
            
            await File.WriteAllTextAsync(filePath, jsonString, Encoding.UTF8);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[LanguageService] Dosya kaydetme hatası: {ex.Message}");
        }
    }
}
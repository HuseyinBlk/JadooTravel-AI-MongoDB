using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace JadooTravel.Controllers;

[Authorize]
[Route("/Admin/AiRecommend")]
public class AiRecommendController : Controller
{
    private readonly string _apiKey;
    private static readonly HttpClient _httpClient = new();

    public AiRecommendController(IConfiguration configuration)
    {
        _apiKey = configuration["GeminiApiKey"] ?? string.Empty;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("GetPlaces")]
    public async Task<IActionResult> GetPlaces(string destination, string? customApiKey)
    {
        if (string.IsNullOrWhiteSpace(destination))
        {
            return Json(new { success = false, message = "Lütfen bir şehir ve ülke adı girin." });
        }

        var keyToUse = !string.IsNullOrWhiteSpace(customApiKey) ? customApiKey.Trim() : _apiKey;

        if (string.IsNullOrEmpty(keyToUse) || keyToUse == "YOUR_API_KEY_HERE")
        {
            return Json(new { success = false, message = "Gemini API Key bulunamadı! Lütfen ekrandaki API Key alanını doldurun." });
        }

        try
        {
            var prompt = $"You are a professional travel guide. Provide a list of the 10 must-visit tourist attraction places in '{destination.Trim()}'. " +
                         "For each place, write a concise 1-sentence description in Turkish. " +
                         "Output a valid JSON array of objects with keys 'Name' and 'Description'.";

            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                }
            };

            var jsonRequest = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                $"https://generativelanguage.googleapis.com/v1/models/gemini-3.5-flash-lite:generateContent?key={keyToUse}",
                content
            );

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(responseString);
                
                var parts = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts");

                var sb = new StringBuilder();
                foreach (var part in parts.EnumerateArray())
                {
                    if (part.TryGetProperty("text", out var textElem))
                    {
                        sb.Append(textElem.GetString());
                    }
                }

                var text = sb.ToString();

                if (!string.IsNullOrEmpty(text))
                {
                    var cleanJson = text.Replace("```json", "").Replace("```", "").Trim();
                    var firstBracket = cleanJson.IndexOf('[');
                    var lastBracket = cleanJson.LastIndexOf(']');
                    if (firstBracket >= 0 && lastBracket > firstBracket)
                    {
                        cleanJson = cleanJson.Substring(firstBracket, lastBracket - firstBracket + 1);
                    }
                    
                    var places = JsonSerializer.Deserialize<List<PlaceRecommendation>>(cleanJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (places != null && places.Count > 0)
                    {
                        return Json(new { success = true, places = places });
                    }
                }
                
                return Json(new { success = false, message = "Öneri listesi alınamadı." });
            }
            else
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                return Json(new { success = false, message = $"Gemini API Hatası! Detay: {errorMsg}" });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Bir hata oluştu: {ex.Message}" });
        }
    }

    public class PlaceRecommendation
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
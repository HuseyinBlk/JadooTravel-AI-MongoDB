namespace JadooTravel.Services.LanguageServices;

public interface ILanguageService
{
    string CurrentLanguage { get; }
    string T(string key, string? defaultValue = null);
}
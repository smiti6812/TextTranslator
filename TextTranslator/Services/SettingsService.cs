using System.Text.Json;

using TextTranslator.Data.Interfaces;
using TextTranslator.Models;

namespace TextTranslator.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly string settingsPath;

        public SettingsService() => settingsPath = Path.Combine(FileSystem.AppDataDirectory, "appsettings.json");

        public AppSettings Get()
        {
            if (!File.Exists(settingsPath))
            {
                return new AppSettings();
            }

            var json = File.ReadAllText(settingsPath);
            return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
        }

        public void Save(AppSettings settings)
        {
            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(settingsPath, json);
        }
    }
}

using TextTranslator.Data.Interfaces;

namespace TextTranslator.Services
{
    public class SpeechService : ISpeechService
    {
        public async Task SpeakAsync(string text, string languageCode)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            var locales = await TextToSpeech.Default.GetLocalesAsync();
            var locale = locales.FirstOrDefault(l =>
                l.Language.StartsWith(languageCode, StringComparison.OrdinalIgnoreCase));

            await TextToSpeech.Default.SpeakAsync(text, new SpeechOptions
            {
                Locale = locale,
                Pitch = 1.0f,
                Volume = 1.0f
            });
        }
    }
}

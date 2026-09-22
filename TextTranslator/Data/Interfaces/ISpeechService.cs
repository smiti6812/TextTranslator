namespace TextTranslator.Data.Interfaces
{
    public interface ISpeechService
    {
        Task SpeakAsync(string text, string languageCode);
    }
}

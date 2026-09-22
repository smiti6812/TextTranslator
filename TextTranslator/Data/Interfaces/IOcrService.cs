using TextTranslator.Models;

namespace TextTranslator.Data.Interfaces
{
    public interface IOcrService
    {
        Task<OcrImportResult> ExtractTextAsync(FileResult file, string languageCode);
    }
}

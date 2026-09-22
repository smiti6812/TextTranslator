using TextTranslator.Models;

namespace TextTranslator.Data.Interfaces
{
    public interface ISettingsService
    {
        AppSettings Get();
        void Save(AppSettings settings);
    }
}

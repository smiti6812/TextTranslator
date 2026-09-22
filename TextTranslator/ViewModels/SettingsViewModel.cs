using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using TextTranslator.Data.Interfaces;
using TextTranslator.Models;

namespace TextTranslator.ViewModels
{
    public partial class SettingsViewModel : BaseViewModel
    {
        private readonly ISettingsService settingsService;

        [ObservableProperty]
        private string ocrApiKey = "";

        [ObservableProperty]
        private string translationApiKey = "";

        [ObservableProperty]
        private string translationEndpoint = "";

        [ObservableProperty]
        private bool autoTranslateOnImport = true;

        public SettingsViewModel(ISettingsService settingsServ)
        {
            settingsService = settingsServ;
            Title = "Beállítások";
            Load();
        }

        private void Load()
        {
            AppSettings settings = settingsService.Get();
            OcrApiKey = settings.OcrApiKey;
            TranslationApiKey = settings.TranslationApiKey;
            TranslationEndpoint = settings.TranslationEndpoint;
            AutoTranslateOnImport = settings.AutoTranslateOnImport;
        }

        [RelayCommand]
        public async Task SaveAsync()
        {
            settingsService.Save(new AppSettings
            {
                OcrApiKey = OcrApiKey,
                TranslationApiKey = TranslationApiKey,
                TranslationEndpoint = TranslationEndpoint,
                AutoTranslateOnImport = AutoTranslateOnImport
            });

            await Shell.Current.DisplayAlert("OK", "Beállítások mentve.", "OK");
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System.Collections.ObjectModel;

using TextTranslator.Data.Interfaces;
using TextTranslator.Models;
namespace TextTranslator.ViewModels
{
    public partial class ImportViewModel : BaseViewModel
    {
        private readonly IFilePickerService filePickerService;
        private readonly IOcrService ocrService;
        private readonly ITranslationService translationService;
        private readonly ISettingsService settingsService;
        private readonly IVocabularyRepository repository;

        private FileResult? selectedFile;

        [ObservableProperty]
        private string selectedFileName = "";

        [ObservableProperty]
        private string sourceLanguage = "nl";

        [ObservableProperty]
        private string targetLanguage = "hu";

        [ObservableProperty]
        private string rawText = "";

        public ObservableCollection<VocabularyItem> ImportedItems { get; } = new();

        public ImportViewModel(
            IFilePickerService filePickerServ,
            IOcrService ocrServ,
            ITranslationService translationServ,
            ISettingsService settingsServ,
            IVocabularyRepository repo)
        {
            filePickerService = filePickerServ;
            ocrService = ocrServ;
            translationService = translationServ;
            settingsService = settingsServ;
            repository = repo;

            Title = "OCR Import";
        }

        [RelayCommand]
        public async Task PickImageAsync()
        {
            selectedFile = await filePickerService.PickImageAsync();
            SelectedFileName = selectedFile?.FileName ?? "";
        }

        [RelayCommand]
        public async Task ImportAsync()
        {
            if (selectedFile == null)
            {
                await Shell.Current.DisplayAlert("Hiba", "Először válassz ki egy képet.", "OK");
                return;
            }

            try
            {
                IsBusy = true;
                ImportedItems.Clear();

                var result = await ocrService.ExtractTextAsync(selectedFile, SourceLanguage);
                RawText = result.RawText;

                var settings = settingsService.Get();

                foreach (var word in result.Words)
                {
                    string translated = "";

                    if (settings.AutoTranslateOnImport)
                    {
                        try
                        {
                            var translation = await translationService.TranslateAsync(word, SourceLanguage, TargetLanguage);
                            translated = translation.TranslatedText;
                        }
                        catch
                        {
                            translated = "";
                        }
                    }

                    ImportedItems.Add(new VocabularyItem
                    {
                        SourceLanguage = SourceLanguage,
                        TargetLanguage = TargetLanguage,
                        SourceWord = word,
                        TargetWord = translated,
                        PartOfSpeech = "",
                        Note = "",
                        SourceText = RawText
                    });
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task SaveImportedAsync()
        {
            var validItems = ImportedItems
                .Where(x => !string.IsNullOrWhiteSpace(x.SourceWord))
                .ToList();

            foreach (var item in validItems)
            {
                await repository.AddAsync(item);
            }

            await Shell.Current.DisplayAlert("OK", $"{validItems.Count} szó mentve.", "OK");
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public void RemoveImportedItem(VocabularyItem? item)
        {
            if (item != null)
                ImportedItems.Remove(item);
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using TextTranslator.Data.Interfaces;
using TextTranslator.Models;


namespace TextTranslator.ViewModels
{
    [QueryProperty(nameof(ItemId), "id")]
    public partial class VocabularyEditViewModel : BaseViewModel
    {
        private readonly IVocabularyRepository repository;

        [ObservableProperty]
        private int itemId;

        [ObservableProperty]
        private string sourceLanguage = "nl";

        [ObservableProperty]
        private string sourceWord = string.Empty;

        [ObservableProperty]
        private string targetLanguage = "hu";

        [ObservableProperty]
        private string targetWord = string.Empty;

        [ObservableProperty]
        private string partOfSpeech = string.Empty;

        [ObservableProperty]
        private string note = string.Empty;

        [ObservableProperty]
        private string sourceText = string.Empty;

        public bool IsEditMode => ItemId > 0;

        public VocabularyEditViewModel(IVocabularyRepository repo)
        {
            repository = repo;
            Title = "Új szó";
        }

        partial void OnItemIdChanged(int value) => MainThread.BeginInvokeOnMainThread(async () => await LoadItemAsync(value));

        private async Task LoadItemAsync(int id)
        {
            if (id <= 0)
            {
                return;
            }

            var item = await repository.GetByIdAsync(id);
            if (item == null)
            {
                return;
            }

            SourceLanguage = item.SourceLanguage;
            SourceWord = item.SourceWord;
            TargetLanguage = item.TargetLanguage;
            TargetWord = item.TargetWord;
            PartOfSpeech = item.PartOfSpeech;
            Note = item.Note;
            SourceText = item.SourceText;
            Title = "Szó szerkesztése";
            OnPropertyChanged(nameof(IsEditMode));
        }

        [RelayCommand]
        public async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(SourceWord) || string.IsNullOrWhiteSpace(TargetWord))
            {
                await Shell.Current.DisplayAlert("Hiba", "A forrás és célszó kötelező.", "OK");
                return;
            }

            if (ItemId > 0)
            {
                var existing = await repository.GetByIdAsync(ItemId);
                if (existing == null)
                {
                    return;
                }

                existing.SourceLanguage = SourceLanguage;
                existing.SourceWord = SourceWord;
                existing.TargetLanguage = TargetLanguage;
                existing.TargetWord = TargetWord;
                existing.PartOfSpeech = PartOfSpeech;
                existing.Note = Note;
                existing.SourceText = SourceText;

                await repository.UpdateAsync(existing);
            }
            else
            {
                await repository.AddAsync(new VocabularyItem
                {
                    SourceLanguage = SourceLanguage,
                    SourceWord = SourceWord,
                    TargetLanguage = TargetLanguage,
                    TargetWord = TargetWord,
                    PartOfSpeech = PartOfSpeech,
                    Note = Note,
                    SourceText = SourceText
                });
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}

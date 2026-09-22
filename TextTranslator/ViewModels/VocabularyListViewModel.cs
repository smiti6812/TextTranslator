using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System.Collections.ObjectModel;

using TextTranslator.Data.Interfaces;
using TextTranslator.Models;


namespace TextTranslator.ViewModels
{
    public partial class VocabularyListViewModel : BaseViewModel
    {
        private readonly IVocabularyRepository repository;
        private readonly ISpeechService speechService;

        public ObservableCollection<VocabularyItem> Items { get; } = new();

        [ObservableProperty]
        private string searchText = string.Empty;

        public VocabularyListViewModel(
            IVocabularyRepository repo,
            ISpeechService speechServ)
        {
            repository = repo;
            speechService = speechServ;
            Title = "Szótár";
        }

        [RelayCommand]
        public async Task LoadAsync()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;
                Items.Clear();

                var items = string.IsNullOrWhiteSpace(SearchText)
                    ? await repository.GetAllAsync()
                    : await repository.SearchAsync(SearchText);

                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task SearchAsync() => await LoadAsync();

        [RelayCommand]
        public async Task DeleteAsync(VocabularyItem? item)
        {
            if (item == null)
            {
                return;
            }

            await repository.DeleteAsync(item.Id);
            Items.Remove(item);
        }

        [RelayCommand]
        public async Task SpeakAsync(VocabularyItem? item)
        {
            if (item == null)
            {
                return;
            }

            await speechService.SpeakAsync(item.SourceWord, item.SourceLanguage);
        }
    }
}

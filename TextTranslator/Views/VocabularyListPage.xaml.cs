using TextTranslator.ViewModels;

namespace TextTranslator.Views;

public partial class VocabularyListPage : ContentPage, IDisposable
{
    private readonly VocabularyListViewModel vocabularyListViewModel;
    private bool disposedValue;

    public VocabularyListPage(VocabularyListViewModel viewModel)
    {
        vocabularyListViewModel = viewModel;
        InitializeComponent();
        BindingContext = vocabularyListViewModel;
        Loaded += VocabularyListPage_Loaded;
    }

    private void VocabularyListPage_Loaded(object? sender, EventArgs e) => vocabularyListViewModel.LoadAsync();
    private async void OnAddClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(VocabularyEditPage));

    private async void OnImportClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(ImportPage));

    private async void OnEditClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int id)
        {
            await Shell.Current.GoToAsync($"{nameof(VocabularyEditPage)}?id={id}");
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                Loaded -= VocabularyListPage_Loaded;
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
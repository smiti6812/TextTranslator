using TextTranslator.ViewModels;

namespace TextTranslator.Views;

public partial class ImportPage : ContentPage
{
    public ImportPage(ImportViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
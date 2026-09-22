using TextTranslator.ViewModels;

namespace TextTranslator.Views;

public partial class VocabularyEditPage : ContentPage
{
    public VocabularyEditPage(VocabularyEditViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
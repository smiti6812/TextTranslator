using TextTranslator.ViewModels;

namespace TextTranslator.Views;

public partial class QuizPage : ContentPage
{
    public QuizPage(QuizViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
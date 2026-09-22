using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using TextTranslator.Data.Interfaces;
using TextTranslator.Models;

namespace TextTranslator.ViewModels
{
    public partial class QuizViewModel : BaseViewModel
    {
        private readonly IVocabularyRepository repository;
        private readonly IQuizService quizService;
        private readonly ISpeechService speechService;

        private List<VocabularyItem> items = new();
        private QuizQuestion? currentQuestion;

        [ObservableProperty]
        private string questionText = "";

        [ObservableProperty]
        private string resultText = "";

        [ObservableProperty]
        private string scoreText = "0 / 0";

        [ObservableProperty]
        private string option1 = "";

        [ObservableProperty]
        private string option2 = "";

        [ObservableProperty]
        private string option3 = "";

        [ObservableProperty]
        private string option4 = "";

        [ObservableProperty]
        private bool sourceToTarget = true;

        private int _total;
        private int _correct;

        public QuizViewModel(
            IVocabularyRepository repo,
            IQuizService quizServ,
            ISpeechService speechServ)
        {
            repository = repo;
            quizService = quizServ;
            speechService = speechServ;
            Title = "Quiz";
        }

        [RelayCommand]
        public async Task LoadAsync()
        {
            items = await repository.GetAllAsync();

            if (items.Count < 4)
            {
                await Shell.Current.DisplayAlert("Kevés adat", "Legalább 4 szó kell a quizhez.", "OK");
                return;
            }

            LoadNextQuestion();
        }

        [RelayCommand]
        public void LoadNextQuestion()
        {
            currentQuestion = quizService.GenerateQuestion(items, SourceToTarget);

            if (currentQuestion == null)
                return;

            QuestionText = currentQuestion.Prompt;
            ResultText = "";

            Option1 = currentQuestion.Options.ElementAtOrDefault(0)?.Text ?? "";
            Option2 = currentQuestion.Options.ElementAtOrDefault(1)?.Text ?? "";
            Option3 = currentQuestion.Options.ElementAtOrDefault(2)?.Text ?? "";
            Option4 = currentQuestion.Options.ElementAtOrDefault(3)?.Text ?? "";
        }

        [RelayCommand]
        public async Task SpeakQuestionAsync()
        {
            if (currentQuestion?.SourceItem == null)
                return;

            var lang = SourceToTarget
                ? currentQuestion.SourceItem.SourceLanguage
                : currentQuestion.SourceItem.TargetLanguage;

            await speechService.SpeakAsync(QuestionText, lang);
        }

        [RelayCommand]
        public async Task AnswerAsync(string selectedOption)
        {
            if (currentQuestion?.SourceItem == null)
                return;

            _total++;

            var correctOption = currentQuestion.Options.FirstOrDefault(x => x.IsCorrect);
            bool isCorrect = correctOption?.Text == selectedOption;

            if (isCorrect)
            {
                _correct++;
                ResultText = "Helyes!";
            }
            else
            {
                ResultText = $"Hibás! Helyes válasz: {correctOption?.Text}";
            }

            ScoreText = $"{_correct} / {_total}";

            quizService.RegisterAnswer(currentQuestion.SourceItem, isCorrect);
            await repository.UpdateAsync(currentQuestion.SourceItem);
        }

        [RelayCommand]
        public async Task ShowSummaryAsync()
        {
            var result = quizService.CreateResult(_total, _correct);
            await Shell.Current.DisplayAlert(
                "Összesítés",
                $"Kérdések: {result.TotalQuestions}\nHelyes: {result.CorrectAnswers}\nHibás: {result.WrongAnswers}\nEredmény: {result.Percentage:0.##}%",
                "OK");
        }
    }
}

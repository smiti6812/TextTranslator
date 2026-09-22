namespace TextTranslator.Models
{
    public class QuizSessionResult
    {
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers => TotalQuestions - CorrectAnswers;
        public double Percentage => TotalQuestions == 0 ? 0 : (double)CorrectAnswers / TotalQuestions * 100.0;
    }
}

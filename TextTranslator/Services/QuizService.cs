using TextTranslator.Data.Interfaces;
using TextTranslator.Models;

namespace TextTranslator.Services
{
    public class QuizService : IQuizService
    {
        private readonly Random random = new();

        public QuizQuestion? GenerateQuestion(List<VocabularyItem> items, bool sourceToTarget = true)
        {
            if (items == null || items.Count < 4)
            {
                return null;
            }

            var correct = items[random.Next(items.Count)];

            var wrongCandidates = items
                .Where(x => x.Id != correct.Id)
                .OrderBy(_ => random.Next())
                .Take(3)
                .ToList();

            var options = new List<QuizAnswerOption>();

            if (sourceToTarget)
            {
                options.AddRange(wrongCandidates.Select(x => new QuizAnswerOption
                {
                    Text = x.TargetWord,
                    IsCorrect = false
                }));

                options.Add(new QuizAnswerOption
                {
                    Text = correct.TargetWord,
                    IsCorrect = true
                });

                options = options.OrderBy(_ => random.Next()).ToList();

                return new QuizQuestion
                {
                    SourceItem = correct,
                    Prompt = correct.SourceWord,
                    Options = options
                };
            }
            else
            {
                options.AddRange(wrongCandidates.Select(x => new QuizAnswerOption
                {
                    Text = x.SourceWord,
                    IsCorrect = false
                }));

                options.Add(new QuizAnswerOption
                {
                    Text = correct.SourceWord,
                    IsCorrect = true
                });

                options = options.OrderBy(_ => random.Next()).ToList();

                return new QuizQuestion
                {
                    SourceItem = correct,
                    Prompt = correct.TargetWord,
                    Options = options
                };
            }
        }

        public void RegisterAnswer(VocabularyItem item, bool isCorrect)
        {
            item.LastReviewedUtc = DateTime.UtcNow;
            if (isCorrect)
            {
                item.SuccessCount++;
            }
            else
            {
                item.FailCount++;
            }
        }

        public QuizSessionResult CreateResult(int total, int correct)
        {
            return new QuizSessionResult
            {
                TotalQuestions = total,
                CorrectAnswers = correct
            };
        }
    }
}

using CommunityToolkit.Maui;

using Microsoft.Extensions.Logging;

using TextTranslator.Data;
using TextTranslator.Data.Interfaces;
using TextTranslator.Services;
using TextTranslator.ViewModels;
using TextTranslator.Views;

namespace TextTranslator
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            // Services
            builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
            builder.Services.AddSingleton<IVocabularyRepository, VocabularyRepository>();
            builder.Services.AddSingleton<ISettingsService, SettingsService>();
            builder.Services.AddSingleton<IFilePickerService, FilePickerService>();
            builder.Services.AddSingleton<ISpeechService, SpeechService>();
            builder.Services.AddSingleton<IQuizService, QuizService>();

            builder.Services.AddHttpClient<IOcrService, OcrSpaceOcrService>();
            builder.Services.AddHttpClient<ITranslationService, TranslationService>();

            builder.Services.AddTransient<VocabularyListViewModel>();
            builder.Services.AddTransient<VocabularyEditViewModel>();
            builder.Services.AddTransient<ImportViewModel>();
            builder.Services.AddTransient<QuizViewModel>();
            builder.Services.AddTransient<SettingsViewModel>();

            builder.Services.AddTransient<VocabularyListPage>();
            builder.Services.AddTransient<VocabularyEditPage>();
            builder.Services.AddTransient<ImportPage>();
            builder.Services.AddTransient<QuizPage>();
            builder.Services.AddTransient<SettingsPage>();

            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<App>();


            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<App>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
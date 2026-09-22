using TextTranslator.Views;

namespace TextTranslator
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(VocabularyEditPage), typeof(VocabularyEditPage));
            Routing.RegisterRoute(nameof(ImportPage), typeof(ImportPage));
        }
    }
}

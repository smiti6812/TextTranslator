using Android.App;
using Android.Content.PM;
using Android.OS;

using System.Runtime.CompilerServices;

namespace TextTranslator
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected async CompilerMarshalOverride void OnCreate(Bundle saveInstanceState)
        {
            Microsoft.Maui.Essentials.Platform.Init(this, saveInstanceState);
        }
    }
}

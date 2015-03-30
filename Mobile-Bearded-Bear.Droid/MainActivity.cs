using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace Mobile_Bearded_Bear.Droid
{
    [Activity(Label = "Mobile_Bearded_Bear", 
        MainLauncher = true,
        Icon = "@drawable/icon", 
        AlwaysRetainTaskState = true, 
        LaunchMode = LaunchMode.SingleInstance,
        ScreenOrientation = ScreenOrientation.SensorLandscape, 
        ConfigurationChanges = 
        ConfigChanges.Orientation |
        ConfigChanges.Keyboard |
        ConfigChanges.KeyboardHidden)]
    public class MainActivity : Microsoft.Xna.Framework.AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var g = new SnakeGame();

            SetContentView((View)g.Services.GetService(typeof(View)));
            g.Run();
        }
    }
}
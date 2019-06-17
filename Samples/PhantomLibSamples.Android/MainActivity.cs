using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PhantomLib.Droid.Effects;
using Xamarin.Forms;
using PhantomLib.Utilities;
using System.Threading.Tasks;

[assembly: ResolutionGroupName("OST.PhantomLib")]
namespace PhantomLibSamples.Droid
{
    [Activity(Label = "PhantomLibSamples", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            PhantomLib.Droid.Effects.Effects.Init();

            LoadApplication(new App());

            AnalyticsTimer.ENABLED = true;

            AnalyticsTimer.WithMethod(AnalyticsTimerExampleVoid).Time();
            var someBoolean = AnalyticsTimer.WithMethod(AnalyticsTimerExampleWithReturn).Time();
            AnalyticsTimer.WithMethod(AnalyticsTimerExampleAsync);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void AnalyticsTimerExampleVoid()
        {
            //Does nothing
        }

        private bool AnalyticsTimerExampleWithReturn()
        {
            return true;
        }

        private async Task AnalyticsTimerExampleAsync()
        {
            await Task.Delay(1000);
        }
    }
}
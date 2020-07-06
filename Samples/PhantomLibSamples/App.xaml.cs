using System;
using System.Reflection;
using Autofac;
using PhantomLib.DendencyInjection;
using PhantomLibSamples.Converters;
using PhantomLibSamples.Effects;
using PhantomLibSamples.FloatingActionButton;
using PhantomLibSamples.Misc;
using PhantomLibSamples.UltimateControl;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhantomLibSamples
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            PhantomResolutionHelper.Register(builder => {

                builder.RegisterNamespace(typeof(ConvertersPage).Namespace);

                builder.RegisterNamespace(typeof(EffectsPage).Namespace);

                builder.RegisterView<FloatingActionButtonPage>();

                builder.RegisterView<MiscPage>();
                builder.RegisterViewModel<MiscViewModel>();

                builder.RegisterView<UltimateControlPage>();
                builder.RegisterViewModel<UltimateControlViewModel>();

                builder.RegisterView<MainPage>();
            });

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

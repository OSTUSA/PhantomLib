using System;
using Autofac;
using PhantomLib.DendencyInjection;
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

                //builder.RegisterType<ChurchService>()
                //    .As<IChurchService>()
                //    .SingleInstance();

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

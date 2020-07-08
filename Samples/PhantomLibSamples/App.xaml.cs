using System;
using System.Reflection;
using Autofac;
using PhantomLib.DendencyInjection;
using PhantomLibSamples.Converters;
using PhantomLibSamples.DependencyInjection;
using PhantomLibSamples.Effects;
using PhantomLibSamples.FloatingActionButton;
using PhantomLibSamples.Misc;
using PhantomLibSamples.Services;
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
                builder.RegisterNamespace(typeof(MiscPage).Namespace);
                builder.RegisterNamespace(typeof(UltimateControlPage).Namespace);
                builder.RegisterNamespace(typeof(DependencyInjectionPage).Namespace);

                builder.RegisterView<FloatingActionButtonPage>();

                builder.RegisterView<MainPage>();
                builder.RegisterViewModel<MainViewModel>();

#if DEBUG
                builder.RegisterSingleton<MockMyService, IMyService>();
#elif RELEASE
                builder.RegisterSingleton<MyService, IMyService>();
#endif

            });

            MainPage mainPage = PhantomResolutionHelper.ResolvePage<MainPage, MainViewModel>();
            MainPage = new NavigationPage(mainPage);
        }
    }
}

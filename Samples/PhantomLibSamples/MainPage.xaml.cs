using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhantomLib.CustomControls;
using PhantomLibSamples.Converters;
using PhantomLibSamples.Effects;
using PhantomLibSamples.FloatingActionButton;
using PhantomLibSamples.Misc;
using PhantomLibSamples.UltimateControl;
using PhantomLibSamples.Utilities;
using PhantomLib.DendencyInjection;
using Xamarin.Forms;
using PhantomLibSamples.DependencyInjection;

namespace PhantomLibSamples
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //Analytics Timing Sample
            new AnalyticsTimerSample();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {           
            await Navigation.PushAsync<UltimateControlPage, UltimateControlViewModel>();
        }

        async void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync<EffectsPage, EffectsViewModel>();
        }

        async void Handle_Clicked_2(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync<ConvertersPage, ConvertersViewModel>();
        }

        async void Handle_Clicked_FloatingActionButton(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new FloatingActionButtonPage());
        }

        async void Handle_Clicked_3(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync<MiscPage, MiscViewModel>();
        }

        async void Handle_Clicked_DI(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync<DependencyInjectionPage, DependencyInjectionViewModel>();
        }
    }
}

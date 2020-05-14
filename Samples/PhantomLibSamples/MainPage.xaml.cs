using System;
using System.ComponentModel;
using PhantomLibSamples.Converters;
using PhantomLibSamples.Effects;
using PhantomLibSamples.FloatingActionButton;
using PhantomLibSamples.Misc;
using PhantomLibSamples.UltimateEntry;
using PhantomLibSamples.Utilities;
using Xamarin.Forms;

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

        private async void Handle_Tapped_UltimateEntry(object sender, EventArgs e)
        {
            var page = new UltimateEntryPage();
            page.BindingContext = new UltimateEntryViewModel();
            
            await Navigation.PushAsync(page);
        }

        private async void Handle_Tapped_Effects(object sender, EventArgs e)
        {
            var page = new EffectsPage();
            page.BindingContext = new EffectsViewModel();

            await Navigation.PushAsync(page);
        }

        private async void Handle_Tapped_Converters(object sender, EventArgs e)
        {
            var page = new ConvertersPage();
            page.BindingContext = new ConvertersViewModel();

            await Navigation.PushAsync(page);
        }

        private async void Handle_Tapped_FloatingActionButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FloatingActionButtonPage());
        }

        private async void Handle_Tapped_Misc(object sender, EventArgs e)
        {
            var navPage = new NavigationPage(new MiscPage())
            {
                BarBackgroundColor = (Color)Application.Current.Resources["BluePrimary"],
                BarTextColor = Color.White
            };

            await Navigation.PushModalAsync(navPage);
        }

        private async void Handle_Tapped_Repeater(object sender, EventArgs e)
        {
            var page = new RepeaterPage();
            page.BindingContext = new RepeaterPageModel();

            await Navigation.PushAsync(page);
        }

        private async void Handle_Tapped_Alert(object sender, EventArgs e)
        {
            var page = new ExampleAlertPage();

            // Do not show an animation for alert pages
            await Navigation.PushModalAsync(page, false);
        }
    }
}

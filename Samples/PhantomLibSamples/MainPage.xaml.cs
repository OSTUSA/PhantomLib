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

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var page = new UltimateEntryPage();
            page.BindingContext = new UltimateEntryViewModel();
            
            await Navigation.PushAsync(page);
        }

        async void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            var page = new EffectsPage();
            page.BindingContext = new EffectsViewModel();

            await Navigation.PushAsync(page);
        }

        async void Handle_Clicked_2(object sender, System.EventArgs e)
        {
            var page = new ConvertersPage();
            page.BindingContext = new ConvertersViewModel();

            await Navigation.PushAsync(page);
        }

        async void Handle_Clicked_FloatingActionButton(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new FloatingActionButtonPage());
        }

        async void Handle_Clicked_3(object sender, System.EventArgs e)
        {
            var page = new MiscPage();
            page.BindingContext = new MiscViewModel();

            await Navigation.PushAsync(page);
        }
    }
}

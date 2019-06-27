using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            RoundedEntry.NextView = RoundedEntry1;
            //Analytics Timing Sample
            new AnalyticsTimerSample();
        }

        protected override void OnAppearing()
        {
            RoundedEntry.TextChanged += RoundedEntry_TextChanged;

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            RoundedEntry.TextChanged -= RoundedEntry_TextChanged;

            base.OnDisappearing();
        }

        void RoundedEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 10)
            {
                RoundedEntry.ShowError = true;
                ErrorLabel.IsVisible = true;
            }
            else
            {
                RoundedEntry.ShowError = false;
                ErrorLabel.IsVisible = false;
            }
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new MainPage());
        }

    }
}

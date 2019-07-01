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

            UltimateEntry.NextView = UltimateEntry1;
            //Analytics Timing Sample
            new AnalyticsTimerSample();
        }

        protected override void OnAppearing()
        {
            UltimateEntry.TextChanged += UltimateEntry_TextChanged;

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            UltimateEntry.TextChanged -= UltimateEntry_TextChanged;

            base.OnDisappearing();
        }

        void UltimateEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 10)
            {
                UltimateEntry.ShowError = true;
                ErrorLabel.IsVisible = true;
            }
            else
            {
                UltimateEntry.ShowError = false;
                ErrorLabel.IsVisible = false;
            }
        }
    }
}

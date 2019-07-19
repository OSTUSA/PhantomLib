using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhantomLib.CustomControls;
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

            StackLayoutAndroidEntries.IsVisible = Device.RuntimePlatform == Device.Android;

            UltimateControl1.NextView = UltimateControl2;
            UltimateControl2.NextView = UltimateControl3;
            UltimateControl3.NextView = UltimateControl4;

            //Analytics Timing Sample
            new AnalyticsTimerSample();
        }

        protected override void OnAppearing()
        {
            UltimateControl1.UltimateEntryInstance.TextChanged += UltimateEntry_TextChanged;
            UltimateControl3.UltimateEntryInstance.TextChanged += UltimateEntry_TextChanged;
            FloatingUltimateControl1.UltimateEntryInstance.TextChanged += UltimateEntry_TextChanged;

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            UltimateControl1.UltimateEntryInstance.TextChanged -= UltimateEntry_TextChanged;
            UltimateControl3.UltimateEntryInstance.TextChanged -= UltimateEntry_TextChanged;
            FloatingUltimateControl1.UltimateEntryInstance.TextChanged -= UltimateEntry_TextChanged;

            base.OnDisappearing();
        }

        void UltimateEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(sender is UltimateControl.UltimateEntry entry)
            {
                entry.ParentUltimateControl.ShowError = e.NewTextValue.Length > 10;
            }
        }
    }
}

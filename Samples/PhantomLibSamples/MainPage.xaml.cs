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

            FloatingUltimateEntry1.UltimateEntry = new UltimateEntry
            {
                RightImageSource = "icon_close_black",
                ImageButtonType = UltimateEntryImageButton.ClearContents,
                AlwaysShowRightImage = true,
                IsRoundedEntry = false,
                BackgroundColor = Color.Beige,
                FocusedBackgroundColor = Color.Aquamarine,
                HeightRequest = 60,
                ThicknessPadding = new Thickness(20,30,20,10),
            };

            UltimateEntry1.NextView = UltimateEntry2;
            UltimateEntry2.NextView = UltimateEntry3;
            UltimateEntry3.NextView = UltimateEntry4;
            UltimateEntry4.NextView = FloatingUltimateEntry1.UltimateEntry;

            //Analytics Timing Sample
            new AnalyticsTimerSample();
        }

        protected override void OnAppearing()
        {
            UltimateEntry1.TextChanged += UltimateEntry_TextChanged;
            UltimateEntry3.TextChanged += UltimateEntry_TextChanged;

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            UltimateEntry1.TextChanged -= UltimateEntry_TextChanged;
            UltimateEntry3.TextChanged -= UltimateEntry_TextChanged;

            base.OnDisappearing();
        }

        void UltimateEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(sender is UltimateEntry entry)
            {
                if (e.NewTextValue.Length > 10)
                {
                    entry.ShowError = true;
                    entry.RightImageSource = "icon_error";
                }
                else
                {
                    entry.ShowError = false;
                }
            }
        }
    }
}

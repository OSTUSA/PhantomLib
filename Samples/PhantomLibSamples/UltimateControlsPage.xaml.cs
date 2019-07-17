using System;
using System.Collections.Generic;
using PhantomLib.CustomControls;
using Xamarin.Forms;

namespace PhantomLibSamples
{
    public partial class UltimateControlsPage : ContentPage
    {
        public UltimateControlsPage()
        {
            InitializeComponent();
            StackLayoutAndroidEntries.IsVisible = Device.RuntimePlatform == Device.Android;

            UltimateEntry1.NextView = UltimateEntry2;
            UltimateEntry2.NextView = UltimateEntry3;
            UltimateEntry3.NextView = UltimateEntry4;
            UltimateEntry4.NextView = FloatingUltimateEntry1.UltimateEntry;

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
            if (sender is UltimateEntry entry)
            {
                entry.ShowError = e.NewTextValue.Length > 10;
            }
        }
    }
}

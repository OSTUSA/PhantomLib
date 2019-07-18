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
            //Analytics Timing Sample
            new AnalyticsTimerSample();
        }

        void Handle_UltimateControlButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UltimateControlsPage());
        }

        void Handle_ConvertersButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConvertersPage());
        }

        void Handle_EffectsButtonClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EffectsAndMisc());
        }
    }
}

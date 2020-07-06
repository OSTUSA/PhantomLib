using System;
using Xamarin.Forms;

namespace PhantomLibSamples.Misc
{
    public partial class MiscPage : ContentPage
    {
        public MiscPage()
        {
            InitializeComponent();
        }

        private async void Handle_Tapped_Close(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}

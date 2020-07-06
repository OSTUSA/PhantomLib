using PhantomLib.Pages;
using Xamarin.Forms;

namespace PhantomLibSamples
{
    public partial class ExampleAlertPage : AlertPage
    {
        public ExampleAlertPage()
        {
            InitializeComponent();

            TappedCommand = new Command(Close);
        }

        private async void Close()
        {
            await Navigation.PopModalAsync(false);
        }
    }
}

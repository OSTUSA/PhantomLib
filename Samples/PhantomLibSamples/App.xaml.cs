using Xamarin.Forms;

namespace PhantomLibSamples
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = (Color)Current.Resources["BluePrimary"],
                BarTextColor = Color.White
            };
        }
    }
}

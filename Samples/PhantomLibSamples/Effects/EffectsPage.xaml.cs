using System.Collections.Generic;
using Xamarin.Forms;

namespace PhantomLibSamples.Effects
{
    public partial class EffectsPage : ContentPage
    {
        public EffectsPage()
        {
            InitializeComponent();

            _picker.ItemsSource = new List<string>
            {
                "Option 1",
                "Option 2",
                "Option 3"
            };

            _picker.SelectedIndex = 0;
        }
    }
}

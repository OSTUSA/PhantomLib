using Xamarin.Forms;

namespace PhantomLibSamples.UltimateEntry
{
    public partial class UltimateEntryPage : ContentPage
    {
        public UltimateEntryPage()
        {
            InitializeComponent();
        }

        void UltimateEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is PhantomLib.CustomControls.UltimateEntry entry)
            {
                entry.ShowError = e.NewTextValue.Length > 4;
            }
        }
    }
}

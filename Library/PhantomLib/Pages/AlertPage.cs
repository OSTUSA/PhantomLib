using System.Windows.Input;
using Xamarin.Forms;

namespace PhantomLib.Pages
{
    public class AlertPage : ContentPage
    {
        public static readonly BindableProperty IsTapCommandEnabledProperty = BindableProperty.Create(nameof(IsTapCommandEnabledProperty), typeof(bool), typeof(AlertPage), true);

        public bool IsTapCommandEnabled
        {
            get { return (bool)GetValue(IsTapCommandEnabledProperty); }
            set { SetValue(IsTapCommandEnabledProperty, value); }
        }

        public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create(nameof(TappedCommand), typeof(ICommand), typeof(AlertPage), null);

        public ICommand TappedCommand
        {
            get { return (ICommand)GetValue(TappedCommandProperty); }
            set { SetValue(TappedCommandProperty, value); }
        }

        public AlertPage()
        {
            BackgroundColor = Color.FromRgba(0d, 0d, 0d, 0.75d);
        }
    }
}

using System;
using PhantomLib.Templates;
using Xamarin.Forms;

namespace PhantomLib.Extensions
{
    public partial class Pages
    {
        public Pages()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty FloatingActionButtonProperty = BindableProperty.CreateAttached(
            "FloatingActionButton",
            typeof(Button),
            typeof(Pages),
            default(Button),
            propertyChanged: OnFloatingActionButtonPropertyChanged);

        private static void OnFloatingActionButtonPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is ContentPage contentPage))
                throw new NotSupportedException($"{FloatingActionButtonProperty.PropertyName} may only be set on instances of ContentPage");

            if (!(contentPage.ControlTemplate is FloatingActionButtonControlTemplate template))
            {
                contentPage.ControlTemplate = template = new FloatingActionButtonControlTemplate();
            }
        }

        public static Button GetFloatingActionButton(BindableObject bindable)
        {
            return (Button)bindable.GetValue(FloatingActionButtonProperty);
        }

        public static void SetFloatingActionButton(BindableObject bindable, Button value)
        {
            if (!(bindable is ContentPage))
                throw new NotSupportedException($"{FloatingActionButtonProperty.PropertyName} may only be set on instances of ContentPage");

            bindable.SetValue(FloatingActionButtonProperty, value);
        }
    }
}

using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhantomLib.Behaviors
{
    public class TapCommandBehavior : Behavior<View>
    {
        private TapGestureRecognizer _tapGestureRecognizer;
        public View AssociatedView { get; private set; }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(TapCommandBehavior), propertyChanged: OnCommandChanged);
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        private static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is TapCommandBehavior tapCommandBehavior) || tapCommandBehavior._tapGestureRecognizer == null)
                return;

            tapCommandBehavior._tapGestureRecognizer.Command = tapCommandBehavior.Command;
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(TapCommandBehavior), propertyChanged: OnCommandParameterChanged);
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        private static void OnCommandParameterChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is TapCommandBehavior tapCommandBehavior) || tapCommandBehavior._tapGestureRecognizer == null)
                return;

            tapCommandBehavior._tapGestureRecognizer.CommandParameter = tapCommandBehavior.CommandParameter;
        }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);

            AssociatedView = bindable;

            _tapGestureRecognizer = new TapGestureRecognizer()
            {
                Command = Command,
                CommandParameter = CommandParameter
            };

            bindable.GestureRecognizers.Add(_tapGestureRecognizer);

            bindable.BindingContextChanged += OnBindingContextChanged;
            BindingContext = bindable.BindingContext;
        }

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.BindingContextChanged -= OnBindingContextChanged;

            bindable.GestureRecognizers.Remove(_tapGestureRecognizer);
            AssociatedView = null;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            _tapGestureRecognizer.Command = Command;
            _tapGestureRecognizer.CommandParameter = CommandParameter;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            BindingContext = AssociatedView.BindingContext;
        }
    }
}

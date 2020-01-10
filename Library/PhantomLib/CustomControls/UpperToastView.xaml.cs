using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using PhantomLib.CustomControls;
using PhantomLib.Services;
using Xamarin.Forms;

namespace Fulfillment.UI.CustomViews
{
    public partial class UpperToastView : RoundedFrame
    {
        public static readonly BindableProperty MessageProperty = BindableProperty.Create(nameof(Message), typeof(string), typeof(UpperToastView), string.Empty);
        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(UpperToastView), "ic_check");
        public static readonly BindableProperty ShowToastProperty = BindableProperty.Create(nameof(ShowToast), typeof(bool), typeof(UpperToastView), false, BindingMode.TwoWay);


        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public bool ShowToast
        {
            get => (bool)GetValue(ShowToastProperty);
            set => SetValue(ShowToastProperty, value);
        }

        private double _viewHeight;
        private CancellationTokenSource _cancelationToken;
        private IAnimationsService _animationHelper;

        public UpperToastView()
        {
            InitializeComponent();

            MessageIcon.Source = Icon;

            _animationHelper = (IAnimationsService)Prism.PrismApplicationBase.Current.Container.Resolve(typeof(IAnimationsService));
        }

        protected async override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (nameof(Message).Equals(propertyName))
            {
                //update the message text for the toast
                MessageText.Text = Message;
            }
            else if (nameof(Icon).Equals(propertyName))
            {
                //update the message icon for the toast
                MessageIcon.Source = Icon;
            }
            else if (nameof(ShowToast).Equals(propertyName))
            {
                if (ShowToast)
                {
                    await Show();
                }
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            //we only need to measure the height once, as the height doesn't dynamically change
            if (_viewHeight <= 0)
            {
                SizeRequest size = Measure(width, height, MeasureFlags.None);

                _viewHeight = size.Request.Height;

                Debug.WriteLine($"UpperToastView Size = {_viewHeight}");
            }
        }

        private async Task Show()
        {
            //cancel any current delay, as we are starting a new animation
            _cancelationToken?.Cancel();

            if (_animationHelper.AnimationsEnabled())
            {
                //start the animation to show the toast
                Animation animateIn = new Animation(d => TranslationY = d, 0, _viewHeight, Easing.SinIn);
                this.Animate("animateIn", animateIn, finished: OnAnimationFinished, length: 400);
            }
            else
            {

                Margin = new Thickness(0);
                ForceLayout();
                await Task.Run(() => Thread.Sleep(2000)).ConfigureAwait(false);
                Hide();
                ShowToast = false;
            }
        }


        private void Hide()
        {
            if (_animationHelper.AnimationsEnabled())
            {
                //start the animation to hide the toast
                Animation animateOut = new Animation(p => TranslationY = p, _viewHeight, 0, Easing.SinOut);
                this.Animate("animateOut", animateOut);
            }
            else
            {
                Margin = new Thickness(0, _viewHeight * -1, 0, 0);
                ForceLayout();
            }
        }


        private async void OnAnimationFinished(double d, bool b)
        {
            _cancelationToken = new CancellationTokenSource();

            try
            {
                await Task.Delay(2000, _cancelationToken.Token).ConfigureAwait(false);
                ShowToast = false;
                Hide();
            }
            catch (Exception)
            {
                //an exception in the animation was encountered, translate back to end of animation
                Debug.WriteLine("Animation delay task canceled");
                TranslationY = 0;
            }
            _cancelationToken = null;
        }

    }
}

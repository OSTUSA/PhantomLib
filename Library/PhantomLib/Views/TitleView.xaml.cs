using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PhantomLib.Utilities;
using Xamarin.Forms;

namespace PhantomLib.Views
{
    public partial class TitleView : AbsoluteLayout
    {
        private IDeviceHelper _deviceHelper;

        public static readonly BindableProperty TitleIconProperty = BindableProperty.Create(nameof(TitleIcon), typeof(ImageSource), typeof(TitleView), null);

        public ImageSource TitleIcon
        {
            get => (ImageSource)GetValue(TitleIconProperty);
            set => SetValue(TitleIconProperty, value);
        }

        public ObservableCollection<View> LeftToolbarItems { get; private set; } = new ObservableCollection<View>();
        public ObservableCollection<View> RightToolbarItems { get; private set; } = new ObservableCollection<View>();

        public TitleView()
        {
            InitializeComponent();

            _deviceHelper = DependencyService.Get<IDeviceHelper>();

            LeftToolbarItems.CollectionChanged += OnLeftToolbarItemsChanged;
            RightToolbarItems.CollectionChanged += OnRightToolbarItemsChanged;
        }

        private void OnLeftToolbarItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateToolbarItems(_leftToolbar, sender as ObservableCollection<View>);
        }

        private void OnRightToolbarItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateToolbarItems(_rightToolbar, sender as ObservableCollection<View>);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > -1)
            {
                double pageWidth = _deviceHelper.GetDisplayWidth();
                double rightMargin = Margin.Right;

                if (Device.RuntimePlatform == Device.iOS)
                {
                    // Add the default right margin for iOS (Android is 0)
                    rightMargin += 8d;
                }

                // Position at half page width, offset half image width, subtract the width taken by back buttons and margins, add back right margin
                double logoPos = (pageWidth / 2d) - (_logoImage.Width / 2d) - (pageWidth - width) + rightMargin;

                SetLayoutBounds(_logoImage, new Rectangle(logoPos, 0.5d, -1d, -1d)); // YProportional
                SetLayoutBounds(_leftToolbar, new Rectangle(0, 0, logoPos, 1)); // PositionProportional,HeightProportional

                double rightToolbarWidth = width - _logoImage.Width - logoPos;

                SetLayoutBounds(_rightToolbar, new Rectangle(1, 1, rightToolbarWidth, 1)); // PositionProportional,HeightProportional
            }
        }

        private void UpdateToolbarItems(StackLayout toolbar, ObservableCollection<View> items)
        {
            if (toolbar != null)
            {
                toolbar.Children.Clear();

                if (items?.Count > 0)
                {
                    foreach (var item in items)
                    {
                        toolbar.Children.Add(item);
                    }
                }
            }
        }
    }
}

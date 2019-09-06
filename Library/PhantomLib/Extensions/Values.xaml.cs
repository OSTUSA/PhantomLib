using System;
using System.Collections.Generic;
using PhantomLib.Utilities;
using Xamarin.Forms;

namespace PhantomLib.Extensions
{
    public partial class Values
    {
        public const string Double_SafeAreaInset_L = "Double_SafeAreaInset_L";
        public const string Double_SafeAreaInset_T = "Double_SafeAreaInset_T";
        public const string Double_SafeAreaInset_R = "Double_SafeAreaInset_R";
        public const string Double_SafeAreaInset_B = "Double_SafeAreaInset_B";

        public const string Thickness_SafeArea = "Thickness_SafeArea";

        public const string Thickness_SafeAreaInsets_V = "Thickness_SafeAreaInsets_V";
        public const string Thickness_SafeAreaInsets_H = "Thickness_SafeAreaInsets_H";

        public const string Thickness_SafeAreaInsets_L = "Thickness_SafeAreaInsets_L";
        public const string Thickness_SafeAreaInsets_T = "Thickness_SafeAreaInsets_T";
        public const string Thickness_SafeAreaInsets_R = "Thickness_SafeAreaInsets_R";
        public const string Thickness_SafeAreaInsets_B = "Thickness_SafeAreaInsets_B";

        public const string Thickness_SafeAreaInsets_LRB = "Thickness_SafeAreaInsets_LRB";

        private readonly IDeviceHelper _deviceHelper;

        public Values()
            : this(DependencyService.Get<IDeviceHelper>())
        {

        }

        public Values(IDeviceHelper deviceHelper)
        {
            _deviceHelper = deviceHelper;

            InitializeComponent();

            SetSafeAreaValues();
        }

        private void SetSafeAreaValues()
        {
            var insets = _deviceHelper.GetSafeAreaInsets();

            this[Double_SafeAreaInset_L] = insets.Left;
            this[Double_SafeAreaInset_T] = insets.Top;
            this[Double_SafeAreaInset_R] = insets.Right;
            this[Double_SafeAreaInset_B] = insets.Bottom;

            this[Thickness_SafeArea] = insets;

            this[Thickness_SafeAreaInsets_V] = new Thickness(0, insets.Top, 0, insets.Bottom);
            this[Thickness_SafeAreaInsets_H] = new Thickness(insets.Left, 0, insets.Right, 0);

            this[Thickness_SafeAreaInsets_L] = new Thickness(insets.Left, 0, 0, 0);
            this[Thickness_SafeAreaInsets_T] = new Thickness(0, insets.Top, 0, 0);
            this[Thickness_SafeAreaInsets_R] = new Thickness(0, 0, insets.Right, 0);
            this[Thickness_SafeAreaInsets_B] = new Thickness(0, 0, 0, insets.Bottom);

            this[Thickness_SafeAreaInsets_LRB] = new Thickness(insets.Left, 0, insets.Right, insets.Bottom);
        }
    }
}

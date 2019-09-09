using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PhantomLibSamples.FloatingActionButton
{
    public partial class FloatingActionButtonPage
    {
        private readonly Random _random;

        public FloatingActionButtonPage()
        {
            _random = new Random();

            InitializeComponent();
        }

        private void OnClicked(object sender, EventArgs e)
        {
            var newText = GenerateRandomString();
            _lblText.Text += newText;
        }

        private string GenerateRandomString(int length = 256)
        {
            var buffer = new byte[length];
            _random.NextBytes(buffer);

            return $"\n\n{Convert.ToBase64String(buffer)}";
        }
    }
}

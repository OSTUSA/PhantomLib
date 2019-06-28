using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhantomLib.Extensions
{
    public static class VisualElementExtensions
    {
        /// <summary>
        /// Animates the background color from its initial color to the desired
        /// color passed as <paramref name="toColor"/>.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        /// <param name="toColor">The final color.</param>
        /// <param name="length">The length of the animation.</param>
        /// <param name="easing">The easing for the animation.</param>
        public static Task<bool> BackgroundColorTo(this VisualElement visualElement, Color toColor, uint length = 250, Easing easing = null)
        {
            var fromColor = visualElement.BackgroundColor;
            Color transform(double t) =>
                Color.FromRgba(
                    fromColor.R + t * (toColor.R - fromColor.R),
                    fromColor.G + t * (toColor.G - fromColor.G),
                    fromColor.B + t * (toColor.B - fromColor.B),
                    fromColor.A + t * (toColor.A - fromColor.A)
                );

            return ColorAnimation(visualElement, nameof(BackgroundColorTo), transform, c => visualElement.BackgroundColor = c, length, easing);
        }

        private static Task<bool> ColorAnimation(this VisualElement element, string name, Func<double, Color> transform, Action<Color> callback, uint length, Easing easing)
        {
            easing = easing ?? Easing.Linear;
            var taskCompletionSource = new TaskCompletionSource<bool>();

            element.Animate<Color>(name, transform, callback, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));

            return taskCompletionSource.Task;
        }
    }
}

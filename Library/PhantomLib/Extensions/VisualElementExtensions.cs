using System;
using System.Threading.Tasks;
using PhantomLib.Services;
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
            return ColorAnimation(visualElement, nameof(BackgroundColorTo), toColor, c => visualElement.BackgroundColor = c, length, easing);
        }

        private static async Task<bool> ColorAnimation(this VisualElement element, string name, Color toColor, Action<Color> callback, uint length, Easing easing)
        {
            bool result = false;

            easing = easing ?? Easing.Linear;
            var taskCompletionSource = new TaskCompletionSource<bool>();

            if (DependencyService.Get<IAnimationsService>().AnimationsEnabled())
            {
                var fromColor = element.BackgroundColor;
                Color transform(double t) =>
                Color.FromRgba(
                    fromColor.R + t * (toColor.R - fromColor.R),
                    fromColor.G + t * (toColor.G - fromColor.G),
                    fromColor.B + t * (toColor.B - fromColor.B),
                    fromColor.A + t * (toColor.A - fromColor.A)
                );

                element.Animate<Color>(name, transform, callback, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));

                result = await taskCompletionSource.Task;
            }
            else
            {
                element.BackgroundColor = toColor;
                if (length > 0)
                {
                    await Task.Delay((int)length);
                }

                result = true;
            }

            return  result;
        }
    }
}

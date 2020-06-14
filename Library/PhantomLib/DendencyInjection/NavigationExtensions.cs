using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhantomLib.DendencyInjection
{
    public static class NavigationExtensions
    {
        public static Task PushAsync<P, M>(this INavigation navigation, Action<M> initialize = null, bool animated = true)
            where P : Page
            where M : InjectablePageModel
        {
            var page = PhantomResolutionHelper.ResolvePage<P, M>(initialize);
            return navigation.PushAsync(page, animated);
        }

        public static Task PushModalAsync<P, M>(this INavigation navigation, Action<M> initialize = null, bool animated = true)
            where P : Page
            where M : InjectablePageModel
        {
            var page = PhantomResolutionHelper.ResolvePage<P, M>(initialize);
            return navigation.PushModalAsync(page, animated);
        }

        public static Task PushNavigationModalAsync<P, M>(this INavigation navigation, Action<M> initialize = null, bool animated = true)
            where P : Page
            where M : InjectablePageModel
        {
            var page = PhantomResolutionHelper.ResolvePage<P, M>(initialize);
            return navigation.PushModalAsync(new NavigationPage(page), animated);
        }
    }
}

using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhantomLib.DendencyInjection
{
    public class InjectablePageModel
    {
        public INavigation Navigation;
        public virtual Task OnAppearing()
        {
            return Task.FromResult(0);
        }
    }
}

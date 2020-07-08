using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhantomLib.DendencyInjection
{
    public class InjectableViewModel
    {
        public INavigation Navigation { get; set; }

        public virtual Task OnAppearing()
        {
            return Task.FromResult(0);
        }
    }
}

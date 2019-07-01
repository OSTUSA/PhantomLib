using System;
using System.Threading.Tasks;
using System.Windows.Input;
using PhantomLib.Models;
using Xamarin.Forms;

namespace PhantomLibSamples.ViewModels
{
    public class TapCommandBehaviorViewModel : BaseAttachable
    {
        #region bindables

        public ICommand DisplayAlert { get; private set; }

        #endregion

        #region constructor

        public TapCommandBehaviorViewModel()
        {
            DisplayAlert = new Command(async (p) => await OnDisplayAlert(p));
        }

        #endregion

        #region command implementations

        private async Task OnDisplayAlert(object parameter)
        {
            await Application.Current.MainPage.DisplayAlert("Here's your secret message:", parameter?.ToString() ?? "<< null >>", "Close");
        }

        #endregion
    }
}

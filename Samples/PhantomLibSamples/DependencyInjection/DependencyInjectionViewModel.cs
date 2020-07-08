using System;
using PhantomLib.DendencyInjection;
using PhantomLibSamples.Services;

namespace PhantomLibSamples.DependencyInjection
{
    public class DependencyInjectionViewModel : InjectableViewModel
    {
        public string Text { get; set; }

        public DependencyInjectionViewModel(IMyService testService)
        {
            Text = testService.GetText();
        }
    }
}

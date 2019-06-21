using System;
using System.Threading;
using System.Threading.Tasks;
using PhantomLib.Utilities;

namespace PhantomLibSamples.Utilities
{
    public class AnalyticsTimerSample
    {
        public AnalyticsTimerSample()
        {
            AnalyticsTimer.ENABLED = true;

            AnalyticsTimer
                .WithMethod(AnalyticsTimerExampleVoid)
                .WithMethodName("Void")
                .Time();

            var someBoolean = AnalyticsTimer
                .WithMethod(AnalyticsTimerExampleWithReturn)
                .WithMethodName("Return Boolean")
                .Time();

            AnalyticsTimer
                .WithMethod(AnalyticsTimerExampleAsync)
                .WithMethodName("Async 1")
                .Time();

            AnalyticsTimer
                .WithMethod(async () => Thread.Sleep(2000))
                .WithMethodName("Async 2")
                .Time();
        }


        private void AnalyticsTimerExampleVoid()
        {
            //Does nothing
        }

        private bool AnalyticsTimerExampleWithReturn()
        {
            return true;
        }

        private async Task AnalyticsTimerExampleAsync()
        {
            Thread.Sleep(1000);
        }
    }
}

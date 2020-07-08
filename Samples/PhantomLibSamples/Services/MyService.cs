using System;
namespace PhantomLibSamples.Services
{
    public class MyService : IMyService
    {
        public string GetText()
        {
            return "Production Text";
        }
    }
}

using System;
namespace PhantomLibSamples.Services
{
    public class MockMyService : IMyService
    {
        public string GetText()
        {
            return "Mock text";
        }
    }
}
using System.Collections.Generic;

namespace PhantomLibSamples
{
    public class RepeaterPageModel
    {
        public List<string> Items { get; private set; }

        public RepeaterPageModel()
        {
            Items = new List<string>();

            for (int i = 0; i < 20; i++)
            {
                Items.Add($"Example item {i}");
            }
        }
    }
}

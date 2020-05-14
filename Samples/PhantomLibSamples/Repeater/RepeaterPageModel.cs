using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PhantomLibSamples
{
    public class RepeaterPageModel
    {
        private int _index;

        public ObservableCollection<string> Items { get; private set; } = new ObservableCollection<string>();

        public Command AddItemCommand { get; private set; }
        public Command RemoveItemCommand { get; private set; }

        public RepeaterPageModel()
        {
            for (int i = 0; i < 5; i++)
            {
                AddItem();
            }

            AddItemCommand = new Command(AddItem);
            RemoveItemCommand = new Command(RemoveItem);
        }

        private void AddItem()
        {
            _index++;
            Items.Add($"Example item {_index}");
        }

        private void RemoveItem()
        {
            if (_index > 0)
            {
                Items.RemoveAt(_index - 1);
                _index--;
            }
        }
    }
}

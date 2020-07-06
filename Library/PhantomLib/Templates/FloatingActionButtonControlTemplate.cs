using System.ComponentModel;
using Xamarin.Forms;

namespace PhantomLib.Templates
{
    public class FloatingActionButtonControlTemplate : ControlTemplate
    {
        public FloatingActionButtonControlTemplate() : base(typeof(Container)) { }

        public class Container : Grid
        {
            public Container()
            {
                Children.Add(new ContentPresenter());
            }

            private Element _parent;
            protected override void OnParentSet()
            {
                base.OnParentSet();

                if (_parent is INotifyPropertyChanged oldParent)
                {
                    oldParent.PropertyChanged -= Parent_PropertyChanged;
                }

                _parent = Parent;

                if (_parent is INotifyPropertyChanged newParent)
                {
                    newParent.PropertyChanged += Parent_PropertyChanged;
                }

                SetButton();
            }

            private void Parent_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == Extensions.Pages.FloatingActionButtonProperty.PropertyName)
                    SetButton();
            }

            private void SetButton()
            {
                Button = Extensions.Pages.GetFloatingActionButton(Parent);
            }

            private Button _button;
            public Button Button
            {
                get { return _button; }
                set
                {
                    if (_button != null && Children.Contains(_button))
                    {
                        Children.Remove(_button);
                    }

                    _button = value;

                    if (_button != null && !Children.Contains(_button))
                    {
                        Children.Add(_button);
                    }
                }
            }
        }
    }
}

using System;
using UIKit;

namespace PhantomLib.iOS
{
    public static class UITextFieldViewModeExtensions
    {
        public static UITextFieldViewMode Opposite(this UITextFieldViewMode viewMode)
        {
            switch (viewMode)
            {
                case UITextFieldViewMode.Always:
                    return UITextFieldViewMode.Never;
                case UITextFieldViewMode.UnlessEditing:
                    return UITextFieldViewMode.WhileEditing;
                case UITextFieldViewMode.WhileEditing:
                    return UITextFieldViewMode.UnlessEditing;
                default: // UITextFieldViewMode.Never
                    return UITextFieldViewMode.Always;
            }
        }
    }
}

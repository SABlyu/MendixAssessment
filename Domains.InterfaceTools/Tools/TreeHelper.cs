using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Domains.InterfaceTools.Tools
{
    public static class TreeHelper
    {
        public static T FindAncestor<T>(this DependencyObject dObj) where T : DependencyObject
        {
            var uiElement = dObj;
            while (uiElement != null)
            {
                uiElement = VisualTreeHelper.GetParent(uiElement as Visual ?? new UIElement())
                    //?? VisualTreeHelper.GetParent(uiElement as Visual3D ?? new UIElement())
                    ?? LogicalTreeHelper.GetParent(uiElement);

                if (uiElement is T) return (T)uiElement;
            }
            return null;
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Caliburn.Micro.Navigation
{
    public static class NavigationConvention
    {
        public static void  AddNavigationConventions()
        {
            var conv=ConventionManager.AddElementConvention<Frame>(ContentControl.ContentProperty, "DataContext", "Loaded");
            conv.GetBindableProperty =
                delegate(DependencyObject foundControl)
                {
                    var element = (ContentControl)foundControl;
                    return element.ContentTemplate == null && !(element.Content is DependencyObject)
                        ? NavigationView.ModelProperty
                        : ContentControl.ContentProperty;
                };

            //conv.ApplyBinding = (viewModelType, path, property, element, convention) =>
            //{
            //    //?????????????????????????????/
            //    if (!ConventionManager.SetBinding(viewModelType, path, property, element, convention, ))
            //        return false;

            //    return true;

            //};
        }
    }
}

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using T2T.Web;

namespace T2T
{
    public class ReadOnlyMaterialDefinitionTemplateConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Carcass)
                return App.Current.Resources["ReadOnlyCarcassDataTemplate"] as DataTemplate;
            if (value is Ply)
                return App.Current.Resources["ReadOnlyPlyDataTemplate"] as DataTemplate;
            if (value is Inerliner)
                return App.Current.Resources["ReadOnlyInerlinerDataTemplate"] as DataTemplate;
            if (value is Lank)
                return App.Current.Resources["ReadOnlyLankDataTemplate"] as DataTemplate;
            if (value is Protector)
                return App.Current.Resources["ReadOnlyProtectorDataTemplate"] as DataTemplate;
            if (value is RawTyre)
                return App.Current.Resources["ReadOnlyRawTyreDataTemplate"] as DataTemplate;
            if (value is Tyre)
                return App.Current.Resources["ReadOnlyTyreDataTemplate"] as DataTemplate;

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

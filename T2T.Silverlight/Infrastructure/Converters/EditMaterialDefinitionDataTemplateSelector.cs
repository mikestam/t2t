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
using T2T.Web;

namespace T2T
{
    public class EditMaterialDefinitionDataTemplateSelector : DataTemplateSelector 
    {
        public override DataTemplate SelectTemplate(object parametar)
        {
            MaterialDefinition value = (MaterialDefinition) parametar;

            if (value is Carcass)
                return App.Current.Resources["EditCarcassDataTemplate"] as DataTemplate;
            if (value is Ply)
                return App.Current.Resources["EditPlyDataTemplate"] as DataTemplate;
            if (value is Inerliner)
                return App.Current.Resources["EditInerlinerDataTemplate"] as DataTemplate;
            if (value is Lank)
                return App.Current.Resources["EditLankDataTemplate"] as DataTemplate;
            if (value is Protector)
                return App.Current.Resources["EditProtectorDataTemplate"] as DataTemplate;
            if (value is RawTyre)
                return App.Current.Resources["EditRawTyreDataTemplate"] as DataTemplate;
            if (value is Tyre)
                return App.Current.Resources["EditTyreDataTemplate"] as DataTemplate;

            return null;
        }
    }
}

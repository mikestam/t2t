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

namespace T2T
{
    public interface IDataTemplateSelector
    {
        DataTemplate SelectTemplate(object parametar);
    }

    public abstract class DataTemplateSelector : IDataTemplateSelector
    {
        public abstract DataTemplate SelectTemplate(object parametar);
    }
}

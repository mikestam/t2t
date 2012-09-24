using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using T2T.Model;

namespace T2T.Resources.Services
{
    public interface IResourceBrowser
    {
        IEnumerable<ResourceClass> ResourceClasses { get; set; }
        ResourceClass CurrentResourceClass { get; set; }
        ResourceDefinition SelectedResourceDefinition { get; set; }
        string FilterCriteria { get; set; }
        Uri Uri { get; set; }
    }
}

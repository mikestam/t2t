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
using Caliburn.Micro;
using System.ComponentModel.Composition;
using Caliburn.Micro.Navigation;

namespace T2T
{
    [Export(typeof(PageTwoViewModel)), PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class PageTwoViewModel : PersitableScreen
    {

    }
}

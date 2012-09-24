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
    [Export(typeof(PageOneViewModel)), PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class PageOneViewModel : PersitableScreen
    {
        public PageOneViewModel()
        {
            ShouldClose = false;
        }

        int _count = -3;
        public int Count
        { get { return _count; } set { if (value != _count) { _count = value; NotifyOfPropertyChange("Count"); } } }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            this.ShouldClose = false;
        }
     
    }
}

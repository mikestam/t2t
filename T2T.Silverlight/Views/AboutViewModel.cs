using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Caliburn.Micro.Navigation;

namespace T2T
{
    [Export(typeof(AboutViewModel)),PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class AboutViewModel : PersitableScreen
    {
        public AboutViewModel()
        {
            this.DisplayName = ApplicationStrings.AboutPageTitle;
        }

        int _count = 0;

        public int Count
        {
            get { return _count; }
            set { _count = value; NotifyOfPropertyChange("Count"); }
        }

        //public ICollection<DependencyObject> ActiveScreens { get; set; }
        private CollectionViewSource _activeScreens = new CollectionViewSource();
        public CollectionViewSource ActiveScreens { get { return _activeScreens; } }

        protected override void OnActivate()
        {
            base.OnActivate();

            ActiveScreens.Source = ((NavigationConductor)this.Parent).Items ;
            Count = ((NavigationConductor)this.Parent).Names.Count;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            this.ShouldClose = false;
        }
        
    }
}

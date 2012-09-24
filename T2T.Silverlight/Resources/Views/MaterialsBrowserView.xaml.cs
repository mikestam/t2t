using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.ComponentModel;
using T2T.Web;
using System.Text;
using System.Globalization;
using System.ServiceModel.DomainServices.Client;
using T2T.Model;
using T2T.Views;
using T2T.Resources.ViewModels;
using T2T;

namespace T2T.Resources.Views
{
    public partial class MaterialsBrowserView : Page, INotifyPropertyChanged
    {
     
        public MaterialsBrowserView()
        {
            InitializeComponent();

            Model = new MaterialsBrowserViewModel();
            Model.PropertyChanged += Model_PropertyChanged;
            this.DataContext = Model;
        }

        void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Uri")
            {
                NavigationService.Navigate(Model.Uri);
            }
        }

        private MaterialsBrowserViewModel Model { get; set; }

        protected override void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            base.OnFragmentNavigation(e);
            Model.FragmentNavigation(e.Fragment);          
        }

        private void submitButtom_Click(object sender, RoutedEventArgs e)
        {
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            MaterialDefinitionWindowView window = new MaterialDefinitionWindowView(Model.CurrentResourceClass);
            window.Closed += new EventHandler(window_Closed);
            window.Show();
        }

        void window_Closed(object sender, EventArgs e)
        {
            MaterialDefinitionWindowView window = (MaterialDefinitionWindowView)sender;
            ResourceDefinition material = window.Material;
        }

        #region INotifyPropertyChanged Members

        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}

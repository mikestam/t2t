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
using T2T.Web;
using System.Diagnostics;
using T2T.Model;
using T2T.Resources.ViewModels;
using System.ComponentModel;
using T2T.Resources.Services;
using System.Collections.ObjectModel;

namespace T2T
{
    public partial class ResourceClassSelector : UserControl
    {
        private IEnumerable<ResourceClass> _resourceClasses = null;

        public ResourceClass CurrentResourceClass
        {
            get { return (ResourceClass)GetValue(CurrentResourceClassProperty); }
            set { SetValue(CurrentResourceClassProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentResourceClass.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentResourceClassProperty =
            DependencyProperty.Register("CurrentResourceClass", typeof(ResourceClass), typeof(ResourceClassSelector), new PropertyMetadata(OnCurrentResourceClassChanged));

        private  static void OnCurrentResourceClassChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
 	       ResourceClassSelector rcs = d as ResourceClassSelector;
            rcs.UpdateData();
        }

        

        public event EventHandler ResourceClassChanged;

        public ResourceClassSelector()
        {
            InitializeComponent();
        }

        public void SetResourceClasses(IEnumerable<ResourceClass> classes)
        {
           // _resourceClasses.Clear();
           // _resourceClasses.AddRange(classes);

            //if (_currentResourceClass != null && !_resourceClasses.Contains(_currentResourceClass))
            //    CurrentResourceClass = null;
            //else
            //    UpdateData();
        }

      

        public IEnumerable<ResourceClass> ResourceClasses
        {
            get { return _resourceClasses; }
            set
            {
                if (_resourceClasses != value)
                {
                    _resourceClasses = value;
                }
            }
        }

        private Nullable<int> CurrentResourceClassID
        {
            get { return (CurrentResourceClass == null) ? null : CurrentResourceClass.ResourceClassID as int?; }
        }

        private  void UpdateData()
        {
            lbResourceClasses.ItemsSource = new ObservableCollection<ResourceClass>(ResourceClasses.Where(m => m.ParentClassID == CurrentResourceClassID).ToList());
            hlResourceClass.Visibility = (CurrentResourceClass == null) ? Visibility.Collapsed : Visibility.Visible;
            hlResourceClass.Content = (CurrentResourceClass == null) ? String.Empty : CurrentResourceClass.Name;
        }

      
        private void hlResourceClass_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentResourceClass != null)
            {
                CurrentResourceClass = CurrentResourceClass.ParentClass;
            }
        }

        private void hlResourceSubclass_Click(object sender, RoutedEventArgs e)
        {
            CurrentResourceClass = (sender as HyperlinkButton).Tag as ResourceClass;
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            ((INotifyPropertyChanged)DataContext).PropertyChanged += ResourceClassSelector_PropertyChanged;
        }

        void ResourceClassSelector_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ResourceClasses")
            {
                ResourceClasses = ((IResourceBrowser)DataContext).ResourceClasses;
                UpdateData();
            }

        }

    }
}

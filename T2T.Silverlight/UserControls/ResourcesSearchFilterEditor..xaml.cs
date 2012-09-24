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

namespace T2T
{
    public partial class ResourcesSearchFilterEditor : UserControl
    {
      
        public event EventHandler SearchFilterChanged;
 
        public ResourcesSearchFilterEditor()
        {
            InitializeComponent();

            this.Loaded += ResourcesSearchFilterEditor_Loaded;
            this.DataContextChanged += ResourcesSearchFilterEditor_DataContextChanged;

        }

        void ResourcesSearchFilterEditor_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Model.Keyword != null)
                txtKeyword.Text = Model.Keyword;
        }

        void ResourcesSearchFilterEditor_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateUIKeywordChanged();

        }

        public ResourcesSearchFilter Model
        {
            get { return DataContext as ResourcesSearchFilter; }
            set
            {
                DataContext = value;
            }
        }
      

        private void txtKeyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            //txtKeyword.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            UpdateUIKeywordChanged();
        }


        private void UpdateUIKeywordChanged() 
        {
            applyFilterButton.IsEnabled = !string.IsNullOrEmpty(txtKeyword.Text);
            //applyFilterButton.IsChecked = txtKeyword.Text == null ? false : true;
            tbCaption.Text = (bool)applyFilterButton.IsChecked ? "Current keyword filter" : "Search by keyword";
            tbKeywordLabel.Visibility = (bool)applyFilterButton.IsChecked ? Visibility.Visible : Visibility.Collapsed;
            tbKeywordLabel.Text = txtKeyword.Text;
            txtKeyword.Visibility = (bool)applyFilterButton.IsChecked ? Visibility.Collapsed : Visibility.Visible;

        }

        protected virtual void OnSearchFilterChanged()
        {
            if (SearchFilterChanged != null)
                SearchFilterChanged(this,EventArgs.Empty);
        }

        private void applyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)applyFilterButton.IsChecked)
            {
                Model = new ResourcesSearchFilter() { Keyword = txtKeyword.Text };
            }
            OnSearchFilterChanged();
            UpdateUIKeywordChanged();

        }
    }
}

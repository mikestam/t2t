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
using T2T.Model;
using T2T.Web;

namespace T2T
{
    public partial class ResourceClassPathNavigator : UserControl
    {
      
        public event EventHandler ResourceClassSelected;

        public ResourceClassPathNavigator()
        {
            InitializeComponent();
        }

        public ResourceClass CurrentResourceClass
        {
            get { return (ResourceClass)GetValue(CurrentResourceClassProperty); }
            set { SetValue(CurrentResourceClassProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentResourceClass.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentResourceClassProperty =
            DependencyProperty.Register("CurrentResourceClass", typeof(ResourceClass), typeof(ResourceClassPathNavigator), new PropertyMetadata(OnCurrentResourceClassChanged));

        private static void OnCurrentResourceClassChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ResourceClassPathNavigator rcs = d as ResourceClassPathNavigator;
            rcs.SetPath();
        }



        public ResourceClass Model
        {
            get { return this.DataContext as ResourceClass; }
            set
            {
                DataContext = value;
                SetPath();

                if (ResourceClassSelected != null)
                    ResourceClassSelected(this, EventArgs.Empty);

            }
        }

        public string Separator { get; set; }


        private void SetPath()
        {
            spContent.Children.Clear();
            spContent.Children.Add(tbRoot);

            ResourceClass rc = CurrentResourceClass;

            while (rc != null)
            {
                spContent.Children.Insert(1, CreatePartPath(rc));
                spContent.Children.Insert(1, new TextBlock() { Text = Separator });
                rc = rc.ParentClass;
            }

        }

        private UIElement CreatePartPath(ResourceClass mc)
        {
            TextBlock tb = new TextBlock() { Text = mc.Name, Tag = mc, Cursor = Cursors.Hand };
            tb.MouseLeftButtonDown += new MouseButtonEventHandler(tbRoot_MouseLeftButtonDown);
            tb.MouseEnter += new MouseEventHandler(tbRoot_MouseEnter);
            tb.MouseLeave += new MouseEventHandler(tbRoot_MouseLeave);

            return tb;
        }   

        private void tbRoot_MouseEnter(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Foreground = new SolidColorBrush(Colors.Blue);
        }

        private void tbRoot_MouseLeave(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Foreground = new SolidColorBrush(Colors.Black);
        }

        private void tbRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CurrentResourceClass = ((TextBlock)sender).Tag as ResourceClass;
        }
    }
}

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

namespace T2T.Infrastructure
{
    public partial class ErrorWindow2 : ChildWindow
    {
         public ErrorWindow2(Exception e) {
            InitializeComponent();
            if (e != null) {
                this.ErrorTextBox.Text = e.Message + Environment.NewLine + Environment.NewLine + e.StackTrace;

            }
        }

        public ErrorWindow2(Uri uri) {
            InitializeComponent();
            if (uri != null) {
                this.ErrorTextBox.Text = "Page not found: \"" + uri.ToString() + "\"";
            }
        }

        public ErrorWindow2(string message, string details) {
            InitializeComponent();
            this.ErrorTextBox.Text = message + Environment.NewLine + Environment.NewLine + details;

        }

        private void OKButton_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = true;
        }
    }
}


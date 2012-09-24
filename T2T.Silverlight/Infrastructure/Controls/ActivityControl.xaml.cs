// ActivityControl.xaml.cs
//

using System;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using System.Windows.Controls;

namespace T2T.Controls {

    public partial class ActivityControl : UserControl {

        public static readonly DependencyProperty OperationProperty =
            DependencyProperty.Register("Operation", typeof(OperationBase),
                                        typeof(ActivityControl),
                                        new PropertyMetadata(OnOperationPropertyChanged));

        public ActivityControl() {
            InitializeComponent();
        }

        public OperationBase Operation {
            get {
                return (OperationBase)GetValue(OperationProperty);
            }
            set {
                SetValue(OperationProperty, value);
            }
        }

        private void OnOperationCompleted(object sender, EventArgs e) {
            OperationBase operation = (OperationBase)sender;

            progressIndicator.Visibility = Visibility.Collapsed;
            operation.Completed -= OnOperationCompleted;

            if (operation.HasError && (operation.IsErrorHandled == false)) {
                MessageBox.Show(operation.Error.Message, "Error", MessageBoxButton.OK);
                operation.MarkErrorAsHandled();
            }
        }

        private void OnOperationPropertyChanged(OperationBase oldOperation, OperationBase newOperation) {
            if (oldOperation != null) {
                oldOperation.Completed -= OnOperationCompleted;
                progressIndicator.Visibility = Visibility.Collapsed;
            }

            if (newOperation != null) {
                newOperation.Completed += OnOperationCompleted;
                progressIndicator.Visibility = Visibility.Visible;
            }
        }

        private static void OnOperationPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            ((ActivityControl)o).OnOperationPropertyChanged((OperationBase)e.OldValue, (OperationBase)e.NewValue);
        }
    }
}

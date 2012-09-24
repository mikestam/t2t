// WatermarkedTextBox.cs
//

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace T2T.Controls {

    [TemplateVisualState(Name = "Watermarked", GroupName = "WatermarkStates")]
    [TemplateVisualState(Name = "Unwatermarked", GroupName = "WatermarkStates")]
    public class WatermarkedTextBox : TextBox {

        public static readonly DependencyProperty WatermarkContentProperty =
            DependencyProperty.Register("WatermarkContent", typeof(object), typeof(WatermarkedTextBox), null);

        public static readonly DependencyProperty WatermarkBrushProperty =
            DependencyProperty.Register("WatermarkBrush", typeof(Brush), typeof(WatermarkedTextBox), null);

        private bool _hasFocus;

        public WatermarkedTextBox() {
            DefaultStyleKey = typeof(WatermarkedTextBox);

            this.Loaded += OnLoaded;
            this.LostFocus += OnLostFocus;
            this.GotFocus += OnGotFocus;
            this.TextChanged += OnTextChanged;
            this.IsEnabledChanged += OnIsEnabledChanged;
        }

        public Brush WatermarkBrush {
            get {
                return (Brush)GetValue(WatermarkBrushProperty);
            }
            set {
                SetValue(WatermarkBrushProperty, value);
            }
        }

        public object WatermarkContent {
            get {
                return (object)GetValue(WatermarkContentProperty);
            }
            set {
                SetValue(WatermarkContentProperty, value);
            }
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            UpdateVisualState(false);
        }

        private void OnGotFocus(object sender, RoutedEventArgs e) {
            if (IsEnabled) {
                _hasFocus = true;
                UpdateVisualState(true);
            }
        }

        private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e) {
            UpdateVisualState(true);
        }

        private void OnLoaded(object sender, RoutedEventArgs e) {
            UpdateVisualState(false);
        }

        private void OnLostFocus(object sender, RoutedEventArgs e) {
            _hasFocus = false;
            UpdateVisualState(true);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e) {
            UpdateVisualState(true);
        }

        private void UpdateVisualState(bool useTransitions) {
            string state = (IsEnabled && (_hasFocus == false) && String.IsNullOrEmpty(Text)) ?
                           "Watermarked" : "Unwatermarked";

            VisualStateManager.GoToState(this, state, useTransitions);
        }
    }
}

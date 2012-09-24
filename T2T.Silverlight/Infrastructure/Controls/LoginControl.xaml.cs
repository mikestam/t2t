// LoginControl.xaml.cs
//

using System;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;

namespace T2T.Controls {

    public partial class LoginControl : UserControl {

        public LoginControl() {
            InitializeComponent();
        }

        public event EventHandler LoggedIn;

        private void OnLoaded(object sender, RoutedEventArgs e) {
            VisualStateManager.GoToState(this, "Anonymous", false);
        }

        private void OnLoginButtonClick(object sender, RoutedEventArgs e) {
            VisualStateManager.GoToState(this, "CredentialInput", true);
        }

        private void OnLogoutButtonClick(object sender, RoutedEventArgs e) {
            WebContextBase.Current.Authentication.Logout(/* throwOnError */ false);
            VisualStateManager.GoToState(this, "Anonymous", true);

            // Reload the page to recreate app in fresh initial anonymous state
            ScriptObject location = (ScriptObject)HtmlPage.Window.GetProperty("location");
            location.Invoke("reload");
        }

        private void OnSubmitButtonClick(object sender, RoutedEventArgs e) {
            string userName = userNameTextBox.Text;
            string password = passwordBox.Password;

            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password)) {
                return;
            }

            VisualStateManager.GoToState(this, "Authenticating", true);

            LoginParameters loginParameters = new LoginParameters(userName, password);
            WebContextBase.Current.Authentication.Login(loginParameters,
                delegate(LoginOperation operation) {
                    if (operation.HasError || (operation.LoginSuccess == false)) {
                        MessageBox.Show("There was an error logging in.\r\nPlease verify your credentials and try again.", "Login", MessageBoxButton.OK);
                        passwordBox.Password = String.Empty;

                        VisualStateManager.GoToState(this, "CredentialInput", /* useTransitions */ false);

                        operation.MarkErrorAsHandled();
                        return;
                    }

                    userLabel.Text = "Welcome " + WebContext.Current.User.DisplayName + "!";
                    VisualStateManager.GoToState(this, "Authenticated", /* useTransitions */ true);

                    userNameTextBox.Text = String.Empty;
                    passwordBox.Password = String.Empty;

                    if (LoggedIn != null) {
                        LoggedIn(this, EventArgs.Empty);
                    }
                }, null);
        }
    }
}

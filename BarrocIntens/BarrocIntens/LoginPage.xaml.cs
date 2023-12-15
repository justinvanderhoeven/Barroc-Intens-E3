using ABI.Windows.UI;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System; 
using BarrocIntens.Data;
using BarrocIntens.MaintenanceViews;
using BarrocIntens.UserViews;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static System.Net.Mime.MediaTypeNames;
using Windows.Security.Cryptography.Core;
using Windows.UI.Popups;
using static System.Net.WebRequestMethods;
using System.Net;
using BarrocIntens.PurchaseViews;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        //Login Event.
        internal async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            //Navigate to an xaml easier
            //Frame.Navigate(typeof(StockSearchView));

            //Put input in variable . 
            string email = Email.Text;
            string inputPassword = Password.Password;

            using (var db = new AppDbContext())
            {
                var user = db.Users.FirstOrDefault(e => e.Email == email);
                //Check if password is correct. 
                if (user != null && VerifyPassword(inputPassword, user.Password, email))
                {
                    //MainPage.CurrentUser = user;
                    Frame.Navigate(typeof(MainPage), user);
                }
                else
                {
                    //Removes input from input boxes.
                    Email.Text = null;
                    Password.Password = null;

                    //Error message
                    ContentDialog wrongCredentialsDialog = new ContentDialog
                    {
                        Title = "Login Failed",
                        Content = "Please check your credentials.",
                        CloseButtonText = "Ok",
                        XamlRoot = this.XamlRoot,
                    };

                    ContentDialogResult result = await wrongCredentialsDialog.ShowAsync();
                }
            }
        }
        //Password verifyer uses SecureHasher class. 
        private bool VerifyPassword(string inputPassword, string hashedPassword, string email)
        {
            return SecureHasher.Verify(inputPassword, hashedPassword);
        }

        //Email Checker
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void loginButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
           // loginButton.Background = new SolidColorBrush(Colors.Gold);
        }
    }
}

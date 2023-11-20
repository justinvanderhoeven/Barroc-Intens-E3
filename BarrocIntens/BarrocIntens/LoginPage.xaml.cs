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

            //Put input in variable . 
            string email = Email.Text;
            string inputPassword = Password.Password;

            using (var db = new AppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == email);

                //Check if password is correct. 
                if (user != null && VerifyPassword(inputPassword, user.Password))
                {
                    NavigateToPage(user.DepartmentId);
                }
                else
                {
                    //Removes in put from input boxes.
                    Email.Text = null; 

                    Password.Password = null;

                    //Error message
                    ContentDialog wrongCredentialsDialog = new ContentDialog
                    {
                        Title = "Login Failed",
                        Content = "Please check your credentials.",
                        CloseButtonText = "Ok",
                        XamlRoot= this.XamlRoot,
                    };

                    ContentDialogResult result = await wrongCredentialsDialog.ShowAsync();
                }
            }
        }
        //Password verifyer uses SecureHasher class. 
        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return SecureHasher.Verify(inputPassword, hashedPassword); 
        }

        private void NavigateToPage(int departmentId)
        {
            switch (departmentId)
            {
                case 1:
                    Frame.Navigate(typeof(CustomerPage));
                    break;
                case 2:
                    Frame.Navigate(typeof(FinancePage));
                    break;
                case 3:
                    Frame.Navigate(typeof(MaintenancePage));
                    break;
                case 4:
                    Frame.Navigate(typeof(SalesPage));
                    break;
                case 5:
                    Frame.Navigate(typeof(PurchasePage));
                    break;
                case 6:
                    Frame.Navigate(typeof(CreateMalfunctionMessagePage));
                    break;

            }
        }

        private void loginButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
           // loginButton.Background = new SolidColorBrush(Colors.Gold);
        }
    }
}

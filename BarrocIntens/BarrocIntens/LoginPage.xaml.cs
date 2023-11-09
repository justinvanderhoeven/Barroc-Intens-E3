using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System; 
using BarrocIntens.Data;
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

        internal void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string inputPassword = Password.Password;

            using (var db = new AppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);

                if (user != null && VerifyPassword(inputPassword, user.Password))
                {
                    Frame.Navigate(typeof(FinancePage));
                }
                else
                {
                    Console.WriteLine("Login failed. Please check your credentials.");
                }
            }
        }
        

        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return SecureHasher.Verify(inputPassword, hashedPassword); 
        }

    }
}

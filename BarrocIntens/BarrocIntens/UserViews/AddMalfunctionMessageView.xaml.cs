using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using BarrocIntens.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.UserViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddMalfunctionMessageView : Page
    {
        private ObservableCollection<MaintenanceAppointment> MaintenanceAppointments = new ObservableCollection<MaintenanceAppointment>();
        public User CurrentUser { get; }
        public AddMalfunctionMessageView(User user)
        {
            CurrentUser = user;
            test.Text = CurrentUser.Name;
            this.InitializeComponent();
        }

        private async void OnSubmitClicked(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            if (coffeeMachineComboBox.SelectedItem != null)
            {
                string selectedCoffeeMachine = coffeeMachineComboBox.SelectedItem.ToString();
                int productId = 0;

                if (selectedCoffeeMachine == "Barroc Intens Italian Light")
                {
                    productId = 1;
                }
                else if (selectedCoffeeMachine == "Barroc Intens Italian")
                {
                    productId = 2;
                }
                else if (selectedCoffeeMachine == "Barroc Intens Italian Deluxe")
                {
                    productId = 3;
                }
                else if (selectedCoffeeMachine == "Barroc Intens Italian Deluxe Special")
                {
                    productId = 4;
                }
                else
                {
                    ShowMessageDialog("Er is iets fout gegaan.", "Geen invoer gegeven.", false);
                    return;
                }

                string problemDescription = txtProblemDescription.Text;
                // Call the CheckInputs method with the appropriate parameters
                CheckInputs(problemDescription, productId);

                db.MaintenanceAppointments.Add(new MaintenanceAppointment
                {
                    ProductId = productId,
                    Description = problemDescription
                });
                db.SaveChanges();

                // Reset input fields
                txtProblemDescription.Text = string.Empty;
                coffeeMachineComboBox.SelectedItem = null;
            }

            async Task ShowMessageDialog(string title, string content, bool isMalfunction, string additionalContent = null)
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = title,
                    XamlRoot = this.XamlRoot,
                    CloseButtonText = "OK",
                    Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 33, 33, 33)), // Background color #212121
                    Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 215, 0)), // Foreground color #ffd700
                };

                dialog.Content = isMalfunction ? content : additionalContent ?? content;

                await dialog.ShowAsync();
            }

            void CheckInputs(string problemDescription, int productId)
            {
                if (string.IsNullOrEmpty(problemDescription) || productId == 0)
                {
                    ShowMessageDialog("Er is iets fout gegaan.", "Geen invoer gegeven.", false);
                }
                else
                {
                    ShowMessageDialog("Storingsmelding", "De storing is gemeld.", true);
                }
            }
        }
    }
}
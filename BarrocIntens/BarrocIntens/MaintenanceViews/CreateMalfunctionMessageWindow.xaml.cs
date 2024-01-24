using BarrocIntens.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.MaintenanceViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateMalfunctionMessageWindow : Window
    {
        public Company CurrentCompany { get; set; }
        private ObservableCollection<MaintenanceAppointment> MaintenanceAppointments = new ObservableCollection<MaintenanceAppointment>();
        public CreateMalfunctionMessageWindow(Company company)
        {
            this.InitializeComponent();
            this.CurrentCompany = company;
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
                    CompanyId = CurrentCompany.Id,
                    ProductId = productId,
                    Description = problemDescription,
                    Status = 1
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
                    CloseButtonText = "OK",
                    XamlRoot = SubmitButton.XamlRoot,
                    Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 33, 33, 33)), // Background color #212121
                    Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 215, 0)), // Foreground color #ffd700
                };

                dialog.Content = isMalfunction ? content : additionalContent ?? content;

                await dialog.ShowAsync();
                this.Close();
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

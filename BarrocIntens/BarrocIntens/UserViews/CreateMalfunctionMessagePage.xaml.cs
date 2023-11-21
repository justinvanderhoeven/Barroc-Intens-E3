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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.UserViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateMalfunctionMessagePage : Page
    {
        private ObservableCollection<MaintenanceAppointment> MaintenanceAppointments = new ObservableCollection<MaintenanceAppointment>();

        public CreateMalfunctionMessagePage()
        {
            this.InitializeComponent();
        }

        private void OnSubmitClicked(object sender, RoutedEventArgs e)
        {
            
            using var db = new AppDbContext();
            if (coffeeMachineComboBox.SelectedItem != null)
            {
                string selectedCoffeeMachine = coffeeMachineComboBox.SelectedItem.ToString();
                int productId = -1;
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
                    ShowMessageDialog("Er is iets fout gegeaan.", "Geen invoer gegeven.", false);
                }

                string problemDescription = txtProblemDescription.Text;
                // Call the CheckInputs method with the appropriate parameters
                CheckInputs(txtProblemDescription.Text, productId);


                void CheckInputs(string problemDescription, int productId)
                {
                    if (string.IsNullOrEmpty(problemDescription))
                    {
                        ShowMessageDialog("Er is iets fout gegaan.", "Geen invoer gegeven.", false);
                    }
                    else if  (productId == -1)
                    {
                        ShowMessageDialog("Er is iets fout gegaan.", "Geen invoer gegeven.", false);
                    }
                    else
                    {
                        ShowMessageDialog("Storingsmelding", "De storing is gemeld.", true);
                    }
                }
                db.MaintenanceAppointments.Add(new MaintenanceAppointment
                {
                    ProductId = productId,
                    Description = txtProblemDescription.Text
                });
                db.SaveChanges();
                

                async void ShowMessageDialog(string title, string content, bool isMalfunction, string additionalContent = null)
                {
                    ContentDialog dialog = new ContentDialog
                    {
                        Title = title,
                        XamlRoot = this.XamlRoot,
                        CloseButtonText = "OK",
                        Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 33, 33, 33)), // Background color #212121
                        Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 215, 0)), // Foreground color #ffd700
                    };

                    if (isMalfunction)
                    {
                        dialog.Content = isMalfunction ? content : additionalContent;
                    }
                    else
                    {
                        dialog.Content = content;
                    }

                    await dialog.ShowAsync();
                }

            }
        }
    }
}

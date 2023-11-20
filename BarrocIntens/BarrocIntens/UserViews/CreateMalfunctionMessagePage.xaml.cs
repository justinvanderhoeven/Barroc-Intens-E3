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
        private bool isMalfunction;

        public CreateMalfunctionMessagePage()
        {
            this.InitializeComponent();
        }

        private void OnSubmitClicked(object sender, RoutedEventArgs e)
        {

            ShowMessageDialog("Storingsmelding", "De storing is gemeld.", true);

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
                    ShowMessageDialog("Er is iets fout gegeaan.","Geen Koffiemachine geselecteerd.", false);
                }

                db.MaintenanceAppointments.Add(new MaintenanceAppointment
                {
                    ProductId = productId,
                    Description = txtProblemDescription.Text
                }); 
                db.SaveChanges();
            }

             async void ShowMessageDialog(string title, string content ,bool isMalfunction, string additionalContent = null)
            {

                ContentDialog malfunctionDialog = new ContentDialog
                {
                    Title = title,
                    Content = isMalfunction ? content : additionalContent,
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };

                if (isMalfunction)
                {
                    await malfunctionDialog.ShowAsync();
                }
                else
                {
                    ContentDialog wrongInputDialog = new ContentDialog
                    {
                        Title = title,
                        Content = content,
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };

                    await wrongInputDialog.ShowAsync();
                }
            }
        }
    } 
}

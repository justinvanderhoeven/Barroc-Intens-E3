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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.SalesViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddCustomerView : Page
    {
        public Company CurrentCompany { get; set; }
        public AddCustomerView()
        {
            this.InitializeComponent();
            using var db = new AppDbContext();
            CompanySuggestBox.ItemsSource = db.Companies.Where(c => c.ContactMail == null && c.ContactName == null).ToList();
        }

        private void CompanySuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem is Company selectedCompany)
            {
                CompanySuggestBox.Text = selectedCompany.Name;
                CurrentCompany = selectedCompany;
                checkForInput();
            }
            else
            {
                CompanySuggestBox.Text = "No selection";
            }
        }

        private async void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                db.Attach(CurrentCompany);
                CurrentCompany.ContactMail = emailInput.Text;
                CurrentCompany.ContactName = nameInput.Text;
                nameInput.Text = null;
                emailInput.Text = null;
                db.SaveChanges();
            }

            ContentDialog createdCredentialsDialog = new ContentDialog
            {
                Title = "Klant toegevoegd",
                Content = "Continue",
                CloseButtonText = "Ok",
                XamlRoot = this.XamlRoot,
            };

            ContentDialogResult result = await createdCredentialsDialog.ShowAsync();
        }

        public void checkForInput()
        {
            if (nameInput.Text == "")
            {
                AddCustomerButton.IsEnabled = false;
                return;
            }
            if(emailInput.Text == "")
            {
                AddCustomerButton.IsEnabled = false;
                return;
            }
            if(CurrentCompany == null)
            {
                return;
            }
            AddCustomerButton.IsEnabled = true;
        }

        private void nameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkForInput();
        }

        private void emailInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkForInput();
        }

        private void CompanySuggestBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CompanySuggestBox.IsSuggestionListOpen = true;
        }

        private void CompanySuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            using var db = new AppDbContext();
            CompanySuggestBox.ItemsSource = db.Companies.
                Where(c => c.ContactMail == null 
                && c.ContactName == null
                && c.Name.Contains(CompanySuggestBox.Text)).ToList();
        }
    }
}

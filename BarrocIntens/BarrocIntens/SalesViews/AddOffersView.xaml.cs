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
    public sealed partial class AddOffersView : Page
    {
        public AddOffersView()
        {

            InitializeComponent();
            using (var db = new AppDbContext())
            {
                ProductsCategoryComboBox.ItemsSource = db.ProductsCategories;
            }

            List<string> list = new List<string>{
                "bedrijf1",
                "bedrijf2",
                "bedrijf3"
            };

            this.InitializeComponent();
            CompanySuggestBox.ItemsSource = list;
        }
    }
}

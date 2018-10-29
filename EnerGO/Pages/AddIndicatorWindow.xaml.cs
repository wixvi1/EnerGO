using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EnerGO.Pages
{
    /// <summary>
    /// Interaction logic for AddIndicator.xaml
    /// </summary>
    public partial class AddIndicatorWindow : Window
    {
        energoEntities db = new energoEntities(); // db context

        public AddIndicatorWindow()
        {
            InitializeComponent();

        }

        //Provides data from database for combobox. Measurments table
        private void energiesCBox_Loaded(object sender, RoutedEventArgs e)
        {
            energiesCBox.ItemsSource = db.Measurments.ToList();
            energiesCBox.DisplayMemberPath = "energy";
            energiesCBox.SelectedValuePath = "id";
        }

        //Provides data from database for combobox. Customers table
        private void customersCBox_Loaded(object sender, RoutedEventArgs e)
        {
            customersCBox.ItemsSource = db.Customers.ToList();
            customersCBox.DisplayMemberPath = "fullname";
            customersCBox.SelectedValuePath = "id";
        }

        //Adds new Indicator to database and then refreshes the grid
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (customersCBox.Text == "" || energiesCBox.Text == "")
            {
                MessageBox.Show("You need to provide all the data", "Whoops...", MessageBoxButton.OK);
                return;
            }

            if (dateFrom.SelectedDate.Value > dateTo.SelectedDate.Value)
            {
                MessageBox.Show("Date from can't be greater than date to...","Whoops...", MessageBoxButton.OK);
                return;
            }
            Indicator newIndicator = new Indicator()
            {
                customer = customersCBox.Text,
                value = Convert.ToInt32(valueTextBox.Text),
                energy = energiesCBox.Text,
                startDate = dateFrom.SelectedDate.Value,
                endDate = dateTo.SelectedDate.Value
            };

            
            db.Indicators.Add(newIndicator);
            db.SaveChanges();
            HomePage.datagrid.ItemsSource = db.Indicators.ToList();
            this.Hide();
        }
    }
}

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
    /// Interaction logic for InsertWindow.xaml
    /// </summary>
    public partial class AddCustomerWindow : Window
    {
        energoEntities db = new energoEntities(); // db context
        public AddCustomerWindow()
        {
            InitializeComponent();
        }

        //Adds new customer to database and then refreshes the grid
        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                MessageBox.Show("There is nothing to add", "Whoops...", MessageBoxButton.OK);
                return;

            }

            Customer newCustomer = new Customer()
            {
                fullname = nameTextBox.Text
            };

            
            db.Customers.Add(newCustomer);
            db.SaveChanges();
            CustomersPage.datagrid.ItemsSource = db.Customers.ToList();
            this.Hide();
        }
    }
}

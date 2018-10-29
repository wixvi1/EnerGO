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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EnerGO.Pages
{
    /// <summary>
    /// Interaction logic for CustomersPage.xaml
    /// </summary>
    public partial class CustomersPage : Page
    {
        energoEntities db = new energoEntities();
        public static DataGrid datagrid;
        public CustomersPage()
        {
            InitializeComponent();
            Load();
        }

        //Loads data from database and assigns it to datagrid
        private void Load()
        {
            myDataGrid.ItemsSource = db.Customers.ToList();
            datagrid = myDataGrid;
        }

        //Pops window where you can add new customer
        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            AddCustomerWindow addPage = new AddCustomerWindow();
            addPage.ShowDialog();
        }

        //Deletes selected row both from grid and database
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((myDataGrid.SelectedItem as Customer) == null)
            {
                MessageBox.Show("There is nothing to delete", "Whoops...", MessageBoxButton.OK);
                return;
            }
            int Id = (myDataGrid.SelectedItem as Customer).id;
            var deleteCustomer = db.Customers.Where(x => x.id == Id).Single();
            db.Customers.Remove(deleteCustomer);
            db.SaveChanges();
            myDataGrid.ItemsSource = db.Customers.ToList();
        }
    }
}

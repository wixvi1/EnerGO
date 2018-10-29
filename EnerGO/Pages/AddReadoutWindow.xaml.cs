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
    /// Interaction logic for ReadoutWindow.xaml
    /// </summary>
    public partial class AddReadoutWindow : Window
    {
        energoEntities db = new energoEntities(); // db context
        public AddReadoutWindow()
        {
            InitializeComponent();
        }

        //Provides data from database for combobox. Customers table
        private void customersCBox_Loaded(object sender, RoutedEventArgs e)
        {
            customersCBox.ItemsSource = db.Customers.ToList();
            customersCBox.DisplayMemberPath = "fullname";
            customersCBox.SelectedValuePath = "id";
        }

        //Fills combo box with data
        private void valuesCBox_Loaded(object sender, RoutedEventArgs e)
        {
            valuesCBox.Items.Add("Payed");
            valuesCBox.Items.Add("Not payed");
        }


        //Inserts new readout into the database and then refreshes the grid
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if(customersCBox.Text == "" || valuesCBox.Text == "" || commentTextBox.Text == "")
            {
                MessageBox.Show("Please, provide all the data", "Whoops...", MessageBoxButton.OK);
                return;
            }

            Readout newReadout = new Readout()
            {
                evaluatedCustomer = customersCBox.Text,
                reviewDate = DateTime.Now,
                value = valuesCBox.Text,
                comment = commentTextBox.Text
            };
            db.Readouts.Add(newReadout);
            db.SaveChanges();
            ReadoutsPage.datagrid.ItemsSource = db.Readouts.ToList();
            this.Hide();
        }
    }
}

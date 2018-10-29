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
    /// Interaction logic for UpdateReadoutWindow.xaml
    /// </summary>
    public partial class UpdateReadoutWindow : Window
    {
        energoEntities db = new energoEntities();
        int Id;
        public UpdateReadoutWindow(int selectedItem)
        {
            InitializeComponent();
            Id = selectedItem;
        }

        // Updates the current row and then refreshes the grid
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (customersCBox.Text == "" || valuesCBox.Text == "" || commentTextBox.Text == "")
            {
                MessageBox.Show("Please, provide all the data", "Whoops...", MessageBoxButton.OK);
                return;
            }

            Readout updateReadout = (from m in db.Readouts
                                     where m.id == Id
                                     select m).Single();

            updateReadout.evaluatedCustomer = customersCBox.Text;
            updateReadout.value = valuesCBox.Text;
            updateReadout.reviewDate = DateTime.Now;
            updateReadout.comment = commentTextBox.Text;

            db.SaveChanges();
            ReadoutsPage.datagrid.ItemsSource = db.Readouts.ToList();
            this.Hide();
        }

        //Adds data to values combobox
        private void valuesCBox_Loaded(object sender, RoutedEventArgs e)
        {
            valuesCBox.Items.Add("Payed");
            valuesCBox.Items.Add("Not payed");
        }

        // Loads data to customers combobox
        private void customersCBox_Loaded(object sender, RoutedEventArgs e)
        {
            customersCBox.ItemsSource = db.Customers.ToList();
            customersCBox.DisplayMemberPath = "fullname";
            customersCBox.SelectedValuePath = "id";
        }
    }
}

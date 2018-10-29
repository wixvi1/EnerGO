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
    /// Interaction logic for UpdateIndicatorWindow.xaml
    /// </summary>
    public partial class UpdateIndicatorWindow : Window
    {
        energoEntities db = new energoEntities();
        int Id;
        public UpdateIndicatorWindow(int indicatorId)
        {
            InitializeComponent();
            Id = indicatorId;
        }


        // Updates the current row and then refreshes the grid
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (customersCBox.Text == "" || energiesCBox.Text == "")
            {
                MessageBox.Show("You need to provide all the data", "Whoops...", MessageBoxButton.OK);
                return;
            }

            if (dateFrom.SelectedDate.Value > dateTo.SelectedDate.Value)
            {
                MessageBox.Show("Date from can't be greater than date to...", "Whoops...", MessageBoxButton.OK);
                return;
            }
            Indicator updateIndicator = (from m in db.Indicators
                                        where m.id == Id
                                        select m).Single();
            updateIndicator.customer = customersCBox.Text;
            updateIndicator.energy = energiesCBox.Text;
            updateIndicator.value = Convert.ToInt32(valueTextBox.Text);
            updateIndicator.startDate = dateFrom.SelectedDate.Value;
            updateIndicator.endDate = dateTo.SelectedDate.Value;
            db.SaveChanges();
            HomePage.datagrid.ItemsSource = db.Indicators.ToList();
            this.Hide();
        }

        // Loads data to energies combobox
        private void energiesCBox_Loaded(object sender, RoutedEventArgs e)
        {
            energiesCBox.ItemsSource = db.Measurments.ToList();
            energiesCBox.DisplayMemberPath = "energy";
            energiesCBox.SelectedValuePath = "id";
        }

        // Loads data to customers combo box
        private void customersCBox_Loaded(object sender, RoutedEventArgs e)
        {
            customersCBox.ItemsSource = db.Customers.ToList();
            customersCBox.DisplayMemberPath = "fullname";
            customersCBox.SelectedValuePath = "id";
        }
    }
}

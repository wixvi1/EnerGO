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
    /// Interaction logic for AddEnergyMeterWindow.xaml
    /// </summary>
    public partial class AddEnergyMeterWindow : Window
    {
        energoEntities db = new energoEntities(); // db context

        public AddEnergyMeterWindow()
        {
            InitializeComponent();
        }

        //Adds new measurment to database and then refreshes the grid
        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            if (energyTextBox.Text == "" || measurmentTextBox.Text == "")
            {

                MessageBox.Show("You need to provide all the data", "Whoops...", MessageBoxButton.OK);
                return;

            }
            Measurment newMeasurment = new Measurment()
            {
                energy = energyTextBox.Text,
                measurment1 = measurmentTextBox.Text
            };

            
            db.Measurments.Add(newMeasurment);
            db.SaveChanges();
            SettingsPage.datagrid.ItemsSource = db.Measurments.ToList();
            this.Hide();
        }
    }
}

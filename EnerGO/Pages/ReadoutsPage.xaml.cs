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
    /// Interaction logic for ReadoutsPage.xaml
    /// </summary>
    public partial class ReadoutsPage : Page
    {
        energoEntities db = new energoEntities();
        public static DataGrid datagrid;
        public ReadoutsPage()
        {
            InitializeComponent();
            Load();
        }

        //Loads data from database and assigns it to datagrid
        private void Load()
        {
            myDataGrid.ItemsSource = db.Readouts.ToList();
            datagrid = myDataGrid;
        }

        //Pops window where you can add new Readout
        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            AddReadoutWindow newWindow = new AddReadoutWindow();
            newWindow.ShowDialog();
            
        }

        //Deletes selected row both from grid and database
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((myDataGrid.SelectedItem as Readout) == null)
            {
                MessageBox.Show("There is nothing to delete", "Whoops...", MessageBoxButton.OK);
                return;
            }
            int Id = (myDataGrid.SelectedItem as Readout).id;
            var deleateReadout = db.Readouts.Where(x => x.id == Id).Single();
            db.Readouts.Remove(deleateReadout);
            db.SaveChanges();
            myDataGrid.ItemsSource = db.Readouts.ToList();
        }

        //Pops window where you can update existing readout
        private void updteBtn_Click(object sender, RoutedEventArgs e)
        {
            if((myDataGrid.SelectedItem as Readout) == null)
            {
                MessageBox.Show("Select an existing row to update", "Whoops...", MessageBoxButton.OK);
                return;
            }
            int id = (myDataGrid.SelectedItem as Readout).id;//selecteditems Id
            UpdateReadoutWindow updWindow = new UpdateReadoutWindow(id);
            updWindow.ShowDialog();
        }
    }
}

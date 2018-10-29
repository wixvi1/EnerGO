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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        energoEntities db = new energoEntities();
        public static DataGrid datagrid;
        public HomePage()
        {
            InitializeComponent();
            Load();
        }

        //Loads data from database and assigns it to datagrid
        private void Load()
        {
            myDataGrid.ItemsSource = db.Indicators.ToList();
            datagrid = myDataGrid;
        }

        //Pops window where you can add new Indicator
        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            AddIndicatorWindow addPage = new AddIndicatorWindow();
            addPage.ShowDialog();
        }

        //Pops window where you can update existing indicator
        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((myDataGrid.SelectedItem as Indicator) == null)
            {
                MessageBox.Show("Select an existing row to update", "Whoops...", MessageBoxButton.OK);
                return;
            }
            int Id = (myDataGrid.SelectedItem as Indicator).id;
            UpdateIndicatorWindow updatePage = new UpdateIndicatorWindow(Id);
            updatePage.ShowDialog();
        }

        //Deletes selected row both from grid and database
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((myDataGrid.SelectedItem as Indicator) == null)
            {
                MessageBox.Show("There is nothing to delete", "Whoops...", MessageBoxButton.OK);
                return;
            }
            int Id = (myDataGrid.SelectedItem as Indicator).id;
            var deleteIndicator = db.Indicators.Where(x => x.id == Id).Single();
            db.Indicators.Remove(deleteIndicator);
            db.SaveChanges();
            myDataGrid.ItemsSource = db.Indicators.ToList();
        }

    }
}

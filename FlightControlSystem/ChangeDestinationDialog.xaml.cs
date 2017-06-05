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

namespace FlightControlSystem
{
    /// <summary>
    /// Logika interakcji dla klasy ChangeDestinationDialog.xaml
    /// </summary>
    public partial class ChangeDestinationDialog : Window
    {
        public Airport newDestination = null;
        public ChangeDestinationDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (CbDestination.SelectedItem != null)
            {
                foreach (Airport a in MainWindow.sys.Airports)
                {
                    if (a.Name == CbDestination.SelectedItem.ToString())
                    {
                        newDestination = a;
                    }
                }
                DialogResult = true;
                Close();
            }
            else
            {
                Close();
            }
        }
    }
}

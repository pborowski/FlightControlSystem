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
    /// Logika interakcji dla klasy AirportSelectionDialog.xaml
    /// </summary>
    public partial class AirportSelectionDialog : Window
    {
        public Airport Selection;

        public AirportSelectionDialog()
        {
            InitializeComponent();
            foreach(Airport a in MainWindow.sys.Airports)
            {
                AirportBox.Items.Add(a.Name);
            }
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            if(AirportBox.SelectedItem != null)
            {
                foreach (Airport a in MainWindow.sys.Airports)
                {
                    if (a.Name == AirportBox.SelectedItem.ToString())
                    {
                        Selection = a;
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Nie wybrano lotniska!");
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Selection = null;
            this.Close();
        }
    }
}

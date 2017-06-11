using System.Windows;

namespace FlightControlSystem
{
    /// <summary>
    /// Logika interakcji dla klasy AirportSelectionDialog.xaml
    /// </summary>
    public partial class AirportSelectionDialog
    {
        public Airport Selection;

        public AirportSelectionDialog()
        {
            InitializeComponent();
            foreach(Airport a in MainWindow.Sys.Airports)
            {
                AirportBox.Items.Add(a.Name);
            }
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            if(AirportBox.SelectedItem != null)
            {
                foreach (Airport a in MainWindow.Sys.Airports)
                {
                    if (a.Name == AirportBox.SelectedItem.ToString())
                    {
                        Selection = a;
                    }
                }
                Close();
            }
            else
            {
                MessageBox.Show("Nie wybrano lotniska!");
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Selection = null;
            Close();
        }
    }
}

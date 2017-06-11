using System.Windows;

namespace FlightControlSystem
{
    /// <summary>
    /// Logika interakcji dla klasy ChangeDestinationDialog.xaml
    /// </summary>
    public partial class ChangeDestinationDialog
    {
        public Airport NewDestination;
        public ChangeDestinationDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (CbDestination.SelectedItem != null)
            {
                foreach (Airport a in MainWindow.Sys.Airports)
                {
                    if (a.Name == CbDestination.SelectedItem.ToString())
                    {
                        NewDestination = a;
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

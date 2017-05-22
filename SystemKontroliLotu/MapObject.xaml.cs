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

namespace SystemKontroliLotu
{
    /// <summary>
    /// Logika interakcji dla klasy MapObject.xaml
    /// </summary>
    public partial class MapObject : UserControl
    {
        #region Properties
        public Point Coordinates { get; set; }
        public string MapObjectName { get; private set; }
        #endregion

        #region Constructors
        public MapObject()
        {
            InitializeComponent();
        }
        public MapObject(string name, Point coordinates)
        {
            MapObjectName = name;
            Coordinates = coordinates;
        }
        #endregion

        #region Methods

        #endregion
    }
}

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

namespace FlightControlSystem
{
    /// <summary>
    /// Logika interakcji dla klasy MapObject.xaml
    /// </summary>
    public abstract partial class MapObject : UserControl
    {
        #region Properties

        public Point Coordinates { get; set; }
        public string MapObjectName { get; protected set; }

        #endregion

        #region Constructors

        protected MapObject()
        {
            InitializeComponent();
        }

        protected MapObject(string name, Point coordinates)
        {
            InitializeComponent();
            this.Name = name;
            this.Coordinates = coordinates;
            ImageMapObject.Width = 50;
            ImageMapObject.Height = 50;
        }
        #endregion

        #region Methods

        public abstract void RenderMapObject(Canvas c);

        #endregion

        public abstract void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e);

        public abstract void MapObject_OnMouseEnter(object sender, MouseEventArgs e);

        public abstract void ImageMapObject_OnMouseLeave(object sender, MouseEventArgs e);
    }
}

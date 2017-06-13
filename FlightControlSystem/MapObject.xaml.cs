using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            Name = name;
            Coordinates = coordinates;
            ImageMapObject.Width = 50;
            ImageMapObject.Height = 50;
        }
        #endregion

        #region Methods

        public abstract void RenderMapObject(Canvas c);

        #endregion

        #region EventHandlers
        public abstract void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e);

        public abstract void MapObject_OnMouseEnter(object sender, MouseEventArgs e);

        public abstract void ImageMapObject_OnMouseLeave(object sender, MouseEventArgs e);
        #endregion

    }
}

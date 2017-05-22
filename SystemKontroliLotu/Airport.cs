using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SystemKontroliLotu
{
    class Airport : MapObject
    {
        #region Constructors
        public Airport()
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"/SystemKontroliLotu;component/Images/airport.png", UriKind.Relative);
            bi.EndInit();
            ImageMapObject.Width = 50;
            ImageMapObject.Height = 50;
            CanvasMapObject.Width = 50;
            CanvasMapObject.Height = 50;
            ImageMapObject.Source = bi;
        }
        #endregion

        #region Methods

        public override void RenderMapObject(Point p, Canvas c)
        {
            this.Coordinates = p;
            Canvas.SetLeft(this, this.Coordinates.X);
            Canvas.SetTop(this, this.Coordinates.Y);
            c.Children.Add(this);
        }
        #endregion
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace FlightControlSystem
{
    public class Aircraft : MapObject
    {
        #region Properties
        public AircraftType Type { get; set; }
        public double Altitude { get; set; }
        public double Speed { get; set; }
        #endregion

        #region Constructors
        public Aircraft(string name, Point coordinates, AircraftType t) 
            : base(name, coordinates)
        {
            Type = t;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"/FlightControlSystem;component/Images/plane.png", UriKind.Relative);
            bi.EndInit();
            ImageMapObject.Width = 30;
            ImageMapObject.Height = 30;
            ImageMapObject.Source = bi;
        }
        #endregion

        #region Methods

        public override void RenderMapObject(Canvas c)
        {
            Canvas.SetLeft(this, this.Coordinates.X);
            Canvas.SetTop(this, this.Coordinates.Y);
            c.Children.Add(this);
        }

        public override void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(Name);
        }

        public override void MapObject_OnMouseEnter(object sender, MouseEventArgs e)
        {
           // if (MapObjectName != null) NameLabel.Content = MapObjectName;
           // NameLabel.Visibility = Visibility.Visible;
        }

        public override void ImageMapObject_OnMouseLeave(object sender, MouseEventArgs e)
        {
           // NameLabel.Visibility = Visibility.Collapsed;
        }

        #endregion
    }
}

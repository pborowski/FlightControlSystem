using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            ImageMapObject.Source = bi;
            //dodać miejsce
        }
        #endregion

    }
}

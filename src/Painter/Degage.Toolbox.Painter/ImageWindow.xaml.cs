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

namespace Degage.Toolbox.Painter
{
    /// <summary>
    /// ImageViewControl.xaml 的交互逻辑
    /// </summary>
    public partial class ImageWindow : UserControl
    {
        public ImageWindow()
        {
            InitializeComponent();
        }

        public void SetImage(String filePath)
        {
            BitmapImage bitmap = new BitmapImage(new Uri(filePath, UriKind.Absolute));
            var imageCtl = this._image;
            if (bitmap.Width < imageCtl.Width && bitmap.Height < imageCtl.Height)
            {
                imageCtl.Stretch = Stretch.None;
            }

            imageCtl.Source = bitmap;
        }
    }
}

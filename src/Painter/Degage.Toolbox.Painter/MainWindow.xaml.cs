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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Degage.Toolbox.Painter
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.InitAnimation();

        }

        private void InitAnimation()
        {


            #region 左上角 LOGO 动画
            Boolean logoAnimationFlag = false;
            DoubleAnimation aniLogo = new DoubleAnimation();
            RotateTransform rotateTran = new RotateTransform(0);

            this._imageLogo.RenderTransform = rotateTran;
            this._imageLogo.RenderTransformOrigin = new Point(0.5, 0.5);

            aniLogo.From = 0;
            aniLogo.To = 360;
            aniLogo.Duration = TimeSpan.FromSeconds(4);

            this._imageLogo.MouseEnter += (s, e) =>
              {
                  if (!logoAnimationFlag)
                  {
                      logoAnimationFlag = true;
                      rotateTran.BeginAnimation(RotateTransform.AngleProperty, aniLogo);
                  }
              };

            aniLogo.Completed += (s, e) =>
              {
                  logoAnimationFlag = false;
              };
            #endregion


            #region  右下角 Setting 动画
            Storyboard storyboard = new Storyboard();
            Boolean settingAnimationFlag = false;

            DoubleAnimation aniSettingScaleX = new DoubleAnimation();
            DoubleAnimation aniSettingScaleY = new DoubleAnimation();
            DoubleAnimation aniSettingOpacity = new DoubleAnimation();


            ScaleTransform scaleTran = new ScaleTransform();
            this._elpSettingAnimation.RenderTransform = scaleTran;
            this._elpSettingAnimation.RenderTransformOrigin = new Point(0.5, 0.5);

            aniSettingScaleX.From = 1;
            aniSettingScaleX.To = 10;
            aniSettingScaleX.Duration = TimeSpan.FromSeconds(0.5);

            aniSettingScaleY.From = 1;
            aniSettingScaleY.To = 10;
            aniSettingScaleY.Duration = TimeSpan.FromSeconds(0.5);

            aniSettingOpacity.From = 0;
            aniSettingOpacity.To = 1;
            aniSettingOpacity.Duration = TimeSpan.FromSeconds(0.4);
            aniSettingOpacity.AutoReverse = true;

            storyboard.Children.Add(aniSettingScaleX);
            storyboard.Children.Add(aniSettingScaleY);
            storyboard.Children.Add(aniSettingOpacity);


            Storyboard.SetTarget(aniSettingScaleX, this._elpSettingAnimation);
            Storyboard.SetTargetProperty(aniSettingScaleX, new PropertyPath("RenderTransform.ScaleX"));

            Storyboard.SetTarget(aniSettingScaleY, this._elpSettingAnimation);
            Storyboard.SetTargetProperty(aniSettingScaleY, new PropertyPath("RenderTransform.ScaleY"));


            Storyboard.SetTarget(aniSettingOpacity, this._elpSettingAnimation);
            Storyboard.SetTargetProperty(aniSettingOpacity, new PropertyPath(nameof(Ellipse.Opacity)));

            this._imageSetting.MouseDown += (s, e) =>
              {
                  if (!settingAnimationFlag)
                  {
                      settingAnimationFlag = true;
                      var point = e.GetPosition(this._gridMain);
                      //  this._elpSettingAnimation.Margin = new Thickness(point.X, point.Y, 0, 0);
                      storyboard.Begin(this._elpSettingAnimation);
                  }

              };
            storyboard.Completed += (s, e) =>
              {
                  settingAnimationFlag = false;
              };
            #endregion




        }




        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void _gridMain_DragEnter(Object sender, DragEventArgs e)
        {
            var data = e.Data;
            e.Effects =DragDropEffects.None;
            if (data.GetDataPresent(DataFormats.FileDrop))
            {
                var fileName = (data.GetData(DataFormats.FileDrop) as String[])?[0];
                if (String.IsNullOrEmpty(fileName))
                {
                    return;
                }

                var ext = System.IO.Path.GetExtension(fileName).ToLower().TrimStart('.');
                switch (ext)
                {
                    case "jpg":
                    case "png":
                    case "bmp":
                    case "ico":
                    case "jpeg":
                        {
                            e.Effects = DragDropEffects.Link;
                        }
                        break;
                    default:return;
                }
            }

        }

        private void _gridMain_Drop(object sender, DragEventArgs e)
        {
            var data = e.Data;
            var fileName = (data.GetData(DataFormats.FileDrop) as String[])?[0];
            Image image = new Image();
            image.Width = 100;
            image.Height = 100;
            image.Stretch = Stretch.Fill;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
          
            bitmap.UriSource = new Uri(fileName, UriKind.Absolute);
            bitmap.EndInit();
            image.Source = bitmap;

            this._wpImagePreview.Children.Add(image);
        }
    }
}

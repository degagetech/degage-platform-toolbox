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

        private Storyboard _storyboard;
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this._imageLogo.MouseEnter += _imageLogo_MouseEnter;
            var location = this._imageLogo.Margin;
            DoubleAnimation ani = new DoubleAnimation();
            RotateTransform transform = new RotateTransform(0);
            this._imageLogo.RenderTransform = transform;
            this._imageLogo.RenderTransformOrigin = new Point(0.5,0.5);
            ani.From = 0;
            ani.To = 360;
            ani.Duration = TimeSpan.FromSeconds(4);
            _storyboard = new Storyboard();
            _storyboard.Children.Add(ani);
            Storyboard.SetTarget(ani, this._imageLogo);
            Storyboard.SetTargetProperty(ani, new PropertyPath("RenderTransform.Angle"));
            _storyboard.Completed += _storyboard_Completed;

        }

        private void _storyboard_Completed(object sender, EventArgs e)
        {
            _flag = false;
        }

        private Boolean _flag = false;
        private void _imageLogo_MouseEnter(object sender, MouseEventArgs e)
        {
            //if (_flag) goto StartAni;
            if (!_flag)
            {
                _flag = true;
                _storyboard.Begin();
            }
        



            //  _flag = true;

            //StartAni:

        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}

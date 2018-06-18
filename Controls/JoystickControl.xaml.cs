using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MyTeletouch.Controls
{
    public sealed partial class JoystickControl : UserControl
    {
        private DateTime lastOffsetTime;
        private Point startLocation;
        private Point previousLocation;
        int pointerId = -1;
        private Point offsetXY;
        private bool button1Down;
        private bool button2Down;
        private bool button3Down;
        private bool button4Down;
        private bool button5Down;

        public JoystickControl()
        {
            this.InitializeComponent();
        }

        private void Grid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if ((App.Current as App).MainController.UseAccelerometerForJoystick)
                return;

            if (pointerId == -1)
            {
                (sender as Grid).CapturePointer(e.Pointer);
                pointerId = (int)e.Pointer.PointerId;
                lastOffsetTime = DateTime.Now;
                startLocation = new Point((sender as Grid).ActualWidth / 2.0, (sender as Grid).ActualHeight / 2.0);
                previousLocation = startLocation;
                Point currentXY = e.GetCurrentPoint(sender as Grid).Position;
                GridJoystick.Opacity = 0.85;
                HandleMoved(currentXY, true);
            }
        }

        private void Grid_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if ((App.Current as App).MainController.UseAccelerometerForJoystick)
                return;

            if (pointerId == e.Pointer.PointerId)
            {
                Point currentXY = e.GetCurrentPoint(sender as Grid).Position;
                HandleMoved(currentXY, false);
            }
        }

        private void HandleMoved(Point location, bool forceMove)
        {
            Point newOffset = new Point();

            newOffset.X = Math.Min(Math.Max(location.X - startLocation.X, -127), 127);
            newOffset.Y = Math.Min(Math.Max(location.Y - startLocation.Y, -127), 127);

            TimeSpan ts = DateTime.Now - lastOffsetTime;
            double timeSinceLastExecutionMs = ts.TotalMilliseconds;
            bool needToUpdate = forceMove || ((timeSinceLastExecutionMs > 20) &&
                    (Math.Abs(offsetXY.X - newOffset.X) >= 1.0 || Math.Abs(offsetXY.Y - newOffset.Y) >= 1.0));

            if (needToUpdate)
            {
                Point newVisualOffset = new Point();
                newVisualOffset.X = Math.Min(Math.Max(newOffset.X, -10), 10);
                newVisualOffset.Y = Math.Min(Math.Max(newOffset.Y, -10), 10);

                offsetXY = newOffset;
                (App.Current as App).MainController.SendJoystickData(offsetXY, button1Down, button2Down, button3Down, button4Down, button5Down, forceMove);
                Point elTransform = PutInConstantRange(offsetXY, 10);
                EllipseTrnsform1.X = elTransform.X;
                EllipseTrnsform1.Y = elTransform.Y;

                previousLocation = location;
                lastOffsetTime = DateTime.Now;
            }
        }

        private Point PutInRange(Grid grid, Point offsetXY, double maxRange)
        {
            double scale = Math.Min(maxRange / (grid.ActualWidth / 2.0), maxRange / (grid.ActualHeight / 2.0));
            offsetXY.X = (int)(offsetXY.X * scale);
            offsetXY.Y = (int)(offsetXY.Y * scale);
            return offsetXY;
        }

        private Point PutInConstantRange(Point offsetXY, double maxRange)
        {
            offsetXY.X *= 20;
            offsetXY.Y *= 20;
            double scaleX = offsetXY.X != 0 ? maxRange / Math.Abs(offsetXY.X) : 1;
            double scaleY = offsetXY.Y != 0 ? maxRange / Math.Abs(offsetXY.Y) : 1;
            double scale = Math.Min(scaleX, scaleY);
            offsetXY.X = (int)(offsetXY.X * scale);
            offsetXY.Y = (int)(offsetXY.Y * scale);
            return offsetXY;
        }

        private void Grid_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if ((App.Current as App).MainController.UseAccelerometerForJoystick)
                return;

            if (pointerId == e.Pointer.PointerId)
            {
                (sender as Grid).ReleasePointerCapture(e.Pointer);
                pointerId = -1;
                offsetXY = new Point();
                (App.Current as App).MainController.SendJoystickData(offsetXY, button1Down, button2Down, button3Down, button4Down, button5Down, true);
                GridJoystick.Opacity = 1;
                EllipseTrnsform1.X = 0;
                EllipseTrnsform1.Y = 0;
            }
        }

        private void PressedControl1_DownChanged(object sender, EventArgs e)
        {
            button1Down = (sender as PressedControl).IsButtonDown;
            (App.Current as App).MainController.SendJoystickData(offsetXY, button1Down, button2Down, button3Down, button4Down, button5Down, true);
        }

        private void PressedControl2_DownChanged(object sender, EventArgs e)
        {
            button2Down = (sender as PressedControl).IsButtonDown;
            (App.Current as App).MainController.SendJoystickData(offsetXY, button1Down, button2Down, button3Down, button4Down, button5Down, true);
        }

        private void PressedControl3_DownChanged(object sender, EventArgs e)
        {
            button3Down = (sender as PressedControl).IsButtonDown;
            (App.Current as App).MainController.SendJoystickData(offsetXY, button1Down, button2Down, button3Down, button4Down, button5Down, true);
        }

        private void PressedControl4_DownChanged(object sender, EventArgs e)
        {
            button4Down = (sender as PressedControl).IsButtonDown;
            (App.Current as App).MainController.SendJoystickData(offsetXY, button1Down, button2Down, button3Down, button4Down, button5Down, true);
        }

        private void PressedControl5_DownChanged(object sender, EventArgs e)
        {
            button5Down = (sender as PressedControl).IsButtonDown;
            (App.Current as App).MainController.SendJoystickData(offsetXY, button1Down, button2Down, button3Down, button4Down, button5Down, true);
        }
    }
}

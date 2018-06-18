using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class MouseControl : UserControl
    {
        private int downPointerId = -1;
        private DateTime downTime;
        private DateTime lastOffsetTime;
        private Point startLocation;
        private Point previousLocation;
        private Point offset;
        private bool leftButtonDown;
        private bool middleButtonDown;
        private bool rightButtonDown;

        public MouseControl()
        {
            this.InitializeComponent();
        }

        private void Grid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if ((App.Current as App).MainController.UseGyrometerForMouse)
                return;

            if (downPointerId == -1)
            {
                (sender as Grid).CapturePointer(e.Pointer);
                downPointerId = (int)e.Pointer.PointerId;
                startLocation = e.GetCurrentPoint(sender as Grid).Position;
                offset = new Point();
                previousLocation = startLocation;
                lastOffsetTime = DateTime.Now;
                downTime = lastOffsetTime;

                (App.Current as App).MainController.SendMouseData(offset, leftButtonDown, middleButtonDown, rightButtonDown, false);
                GridMouse.Opacity = 0.85;
            }
        }

        private void Grid_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if ((App.Current as App).MainController.UseGyrometerForMouse)
                return;

            if (downPointerId == e.Pointer.PointerId)
            {
                Point currentXY = e.GetCurrentPoint(sender as Grid).Position;
                HandleMoved(currentXY, false);
            }
        }

        private void HandleMoved(Point location, bool forceMove)
        {
            Point newOffset = new Point();

            float factor = 3;
            //var timeSinceStartExecutionMs = abs(NSDate().timeIntervalSince1970 - timeStart) * 1000
            //if(timeSinceStartExecutionMs > 400) {
            //    factor = 2
            //}

            newOffset.X = Math.Min(Math.Max((location.X - previousLocation.X) * factor, -127), 127);
            newOffset.Y = Math.Min(Math.Max((location.Y - previousLocation.Y) * factor, -127), 127);

            TimeSpan ts = DateTime.Now - lastOffsetTime;
            double timeSinceLastExecutionMs = ts.TotalMilliseconds;
            bool needToUpdate = forceMove || ((timeSinceLastExecutionMs > 20) &&
                    (Math.Abs(offset.X - newOffset.X) >= 1.0 || Math.Abs(offset.Y - newOffset.Y) >= 1.0));

            if (needToUpdate)
            {
                Point newVisualOffset = new Point();
                newVisualOffset.X = Math.Min(Math.Max(newOffset.X, -10), 10);
                newVisualOffset.Y = Math.Min(Math.Max(newOffset.Y, -10), 10);

                offset = newOffset;
                (App.Current as App).MainController.SendMouseData(offset, leftButtonDown, middleButtonDown, rightButtonDown, forceMove);
                Point elTransform = PutInConstantRange(offset, 10);
                EllipseTrnsform1.X = elTransform.X;
                EllipseTrnsform1.Y = elTransform.Y;

                previousLocation = location;
                lastOffsetTime = DateTime.Now;
            }
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
            if ((App.Current as App).MainController.UseGyrometerForMouse)
                return;

            if (downPointerId == e.Pointer.PointerId)
            {
                downPointerId = -1;
                offset = new Point();

                if (!leftButtonDown)
                {
                    Point currentXY = e.GetCurrentPoint(sender as Grid).Position;
                    Point fromStartXY = new Point(currentXY.X - startLocation.X, currentXY.Y - startLocation.Y);
                    TimeSpan ts = DateTime.Now - downTime;
                    if (ts.TotalMilliseconds < 250 && fromStartXY.X < 10 && fromStartXY.Y < 10)
                        (App.Current as App).MainController.SendMouseData(offset, true, middleButtonDown, rightButtonDown, true);
                }

                (sender as Grid).ReleasePointerCapture(e.Pointer);
                (App.Current as App).MainController.SendMouseData(offset, leftButtonDown, middleButtonDown, rightButtonDown, true);
                GridMouse.Opacity = 1;
                EllipseTrnsform1.X = 0;
                EllipseTrnsform1.Y = 0;
            }
        }

        private void PressedControlLeft_DownChanged(object sender, EventArgs e)
        {
            Button1.Fill = (sender as PressedControl).IsButtonDown ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Transparent);
            leftButtonDown = (sender as PressedControl).IsButtonDown;
            (App.Current as App).MainController.SendMouseData(offset, leftButtonDown, middleButtonDown, rightButtonDown, true);
        }

        private void PressedControlMiddle_DownChanged(object sender, EventArgs e)
        {
            Button2.Fill = (sender as PressedControl).IsButtonDown ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Transparent);
            middleButtonDown = (sender as PressedControl).IsButtonDown;
            (App.Current as App).MainController.SendMouseData(offset, leftButtonDown, middleButtonDown, rightButtonDown, true);
        }
        private void PressedControlRight_DownChanged(object sender, EventArgs e)
        {
            Button3.Fill = (sender as PressedControl).IsButtonDown ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Transparent);
            rightButtonDown = (sender as PressedControl).IsButtonDown;
            (App.Current as App).MainController.SendMouseData(offset, leftButtonDown, middleButtonDown, rightButtonDown, true);
        }
    }
}

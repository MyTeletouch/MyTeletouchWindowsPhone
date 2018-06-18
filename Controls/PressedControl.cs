using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace MyTeletouch
{
    public sealed class PressedControl : ContentControl
    {
        private int pressedPointerId = -1;

        public event EventHandler DownChanged;

        public PressedControl()
        {
            this.DefaultStyleKey = typeof(PressedControl);
            this.Background = new SolidColorBrush(Colors.Transparent);
        }

        public bool IsButtonDown { get; private set; }

        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            base.OnPointerPressed(e);
            if (pressedPointerId == -1)
            {
                this.CapturePointer(e.Pointer);
                
                pressedPointerId = (int)e.Pointer.PointerId;
                IsButtonDown = true;
                Opacity = 0.25;
                if (DownChanged != null)
                    DownChanged(this, EventArgs.Empty);
            }
        }

        protected override void OnPointerCanceled(PointerRoutedEventArgs e)
        {
            base.OnPointerCanceled(e);
            DoPointerReleased(e);
        }

        protected override void OnPointerExited(PointerRoutedEventArgs e)
        {
            base.OnPointerExited(e);
            DoPointerReleased(e);
        }

        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            base.OnPointerReleased(e);
            DoPointerReleased(e);
        }

        protected override void OnPointerCaptureLost(PointerRoutedEventArgs e)
        {
            base.OnPointerCaptureLost(e);
            DoPointerReleased(e);
        }

        private void DoPointerReleased(PointerRoutedEventArgs e)
        {
            if (pressedPointerId == e.Pointer.PointerId)
            {
                pressedPointerId = -1;

                this.ReleasePointerCapture(e.Pointer);

                IsButtonDown = false;
                Opacity = 1;
                if (DownChanged != null)
                    DownChanged(this, EventArgs.Empty);
            }
        }
    }
}

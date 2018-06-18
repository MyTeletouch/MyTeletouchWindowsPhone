using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
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
    public sealed partial class KeyboardControl : UserControl
    {
        private byte[] pressedKeys = new byte[3] { 0, 0, 0 };
        private int lenBefore;

        public KeyboardControl()
        {
            this.InitializeComponent();
        }

        internal void Initialize()
        {
            MainTextBox.TextChanged -= MainTextBox_TextChanged;
            MainTextBox.Text = string.Empty;
            lenBefore = 0;
            MainTextBox.Focus(Windows.UI.Xaml.FocusState.Programmatic);
            MainTextBox.IsTextPredictionEnabled = false;
            MainTextBox.IsSpellCheckEnabled = false;
            //MainTextBox.IsHitTestVisible = false;
            MainTextBox.TextChanged += MainTextBox_TextChanged;
        }

        private void MainTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int newLen = MainTextBox.Text.Length;

            if(newLen >= lenBefore)
            {
                ScanCodeInfo info = AnsiiToScanCodeConverter.ScanCodeFromChar(MainTextBox.Text.Last());
                if (info.ScanCode != 0)
                {
                    pressedKeys[0] = info.SpecialKeys;
                    pressedKeys[2] = info.ScanCode;
                    (App.Current as App).MainController.SendKeyboadData(pressedKeys);

                    pressedKeys[0] = 0;
                    pressedKeys[2] = 0;
                    (App.Current as App).MainController.SendKeyboadData(pressedKeys);
                }
            }

            lenBefore = newLen;
        }

        private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key != VirtualKey.Enter && e.Key != VirtualKey.Tab && e.Key != VirtualKey.Back)
                return;

            byte keyCode = VirtualKeyToKeyCode.GetKeyCode(e.Key);
            pressedKeys[0] = 0;
            pressedKeys[2] = keyCode;
            (App.Current as App).MainController.SendKeyboadData(pressedKeys);
        }

        private void TextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key != VirtualKey.Enter && e.Key != VirtualKey.Tab && e.Key != VirtualKey.Back)
                return;

            pressedKeys[0] = 0;
            pressedKeys[2] = 0;
            (App.Current as App).MainController.SendKeyboadData(pressedKeys);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace MyTeletouch
{
    public static class VirtualKeyToKeyCode
    {
        public static byte GetKeyCode(VirtualKey keyType)
        {
            byte c = 0;
            switch (keyType)
            {
                case VirtualKey.A:
                    c = 0x04;
                    break;
                case VirtualKey.Accept:
                    break;
                case VirtualKey.Add:
                    c = 0x2E;
                    break;
                case VirtualKey.Application:
                    break;
                case VirtualKey.B:
                    c = 0x05;
                    break;
                case VirtualKey.Back:
                    c = 0x2A;
                    break;
                case VirtualKey.C:
                    c = 0x06;
                    break;
                case VirtualKey.Cancel:
                    break;
                case VirtualKey.CapitalLock:
                    break;
                case VirtualKey.Clear:
                    break;
                case VirtualKey.Control:
                    break;
                case VirtualKey.Convert:
                    break;
                case VirtualKey.D:
                    c = 0x07;
                    break;
                case VirtualKey.Decimal:
                    c = 0x37;
                    break;
                case VirtualKey.Delete:
                    c = 0x2A;
                    break;
                case VirtualKey.Divide:
                    c = 0x31;
                    break;
                case VirtualKey.Down:
                    c = 0x5A;
                    break;
                case VirtualKey.E:
                    c = 0x08;
                    break;
                case VirtualKey.End:
                    break;
                case VirtualKey.Enter:
                    c = 0x28;
                    break;
                case VirtualKey.Escape:
                    c = 0x29;
                    break;
                case VirtualKey.Execute:
                    break;
                case VirtualKey.F:
                    c = 0x09;
                    break;
                case VirtualKey.F1:
                    break;
                case VirtualKey.F10:
                    break;
                case VirtualKey.F11:
                    break;
                case VirtualKey.F12:
                    break;
                case VirtualKey.F13:
                    break;
                case VirtualKey.F14:
                    break;
                case VirtualKey.F15:
                    break;
                case VirtualKey.F16:
                    break;
                case VirtualKey.F17:
                    break;
                case VirtualKey.F18:
                    break;
                case VirtualKey.F19:
                    break;
                case VirtualKey.F2:
                    break;
                case VirtualKey.F20:
                    break;
                case VirtualKey.F21:
                    break;
                case VirtualKey.F22:
                    break;
                case VirtualKey.F23:
                    break;
                case VirtualKey.F24:
                    break;
                case VirtualKey.F3:
                    break;
                case VirtualKey.F4:
                    break;
                case VirtualKey.F5:
                    break;
                case VirtualKey.F6:
                    break;
                case VirtualKey.F7:
                    break;
                case VirtualKey.F8:
                    break;
                case VirtualKey.F9:
                    break;
                case VirtualKey.Favorites:
                    break;
                case VirtualKey.Final:
                    break;
                case VirtualKey.G:
                    c = 0x0A;
                    break;
                case VirtualKey.GoBack:
                    break;
                case VirtualKey.GoForward:
                    break;
                case VirtualKey.GoHome:
                    break;
                case VirtualKey.H:
                    c = 0x0B;
                    break;
                case VirtualKey.Hangul:
                    break;
                case VirtualKey.Hanja:
                    break;
                case VirtualKey.Help:
                    break;
                case VirtualKey.Home:
                    break;
                case VirtualKey.I:
                    c = 0x0C;
                    break;
                case VirtualKey.Insert:
                    break;
                case VirtualKey.J:
                    c = 0x0D;
                    break;
                case VirtualKey.Junja:
                    break;
                case VirtualKey.K:
                    c = 0x0E;
                    break;
                //        case VirtualKey.VK_Kana:
                //            break;
                //        case VirtualKey.VK_Kanji:
                //            break;
                case VirtualKey.L:
                    c = 0x0F;
                    break;
                case VirtualKey.Left:
                    c = 0x5C;
                    break;
                case VirtualKey.LeftButton:
                    break;
                case VirtualKey.LeftControl:
                    break;
                case VirtualKey.LeftMenu:
                    break;
                case VirtualKey.LeftShift:
                    break;
                case VirtualKey.LeftWindows:
                    break;
                case VirtualKey.M:
                    c = 0x10;
                    break;
                case VirtualKey.Menu:
                    break;
                case VirtualKey.MiddleButton:
                    break;
                case VirtualKey.ModeChange:
                    break;
                case VirtualKey.Multiply:
                    break;
                case VirtualKey.N:
                    c = 0x11;
                    break;
                case VirtualKey.NonConvert:
                    break;
                case VirtualKey.None:
                    break;
                case VirtualKey.Number0:
                    c = 0x27;
                    break;
                case VirtualKey.Number1:
                    c = 0x1E;
                    break;
                case VirtualKey.Number2:
                    c = 0x1F;
                    break;
                case VirtualKey.Number3:
                    c = 0x20;
                    break;
                case VirtualKey.Number4:
                    c = 0x21;
                    break;
                case VirtualKey.Number5:
                    c = 0x22;
                    break;
                case VirtualKey.Number6:
                    c = 0x23;
                    break;
                case VirtualKey.Number7:
                    c = 0x24;
                    break;
                case VirtualKey.Number8:
                    c = 0x25;
                    break;
                case VirtualKey.Number9:
                    c = 0x26;
                    break;
                case VirtualKey.NumberKeyLock:
                    break;
                case VirtualKey.NumberPad0:
                    break;
                case VirtualKey.NumberPad1:
                    break;
                case VirtualKey.NumberPad2:
                    break;
                case VirtualKey.NumberPad3:
                    break;
                case VirtualKey.NumberPad4:
                    break;
                case VirtualKey.NumberPad5:
                    break;
                case VirtualKey.NumberPad6:
                    break;
                case VirtualKey.NumberPad7:
                    break;
                case VirtualKey.NumberPad8:
                    break;
                case VirtualKey.NumberPad9:
                    break;
                case VirtualKey.O:
                    c = 0x12;
                    break;
                case VirtualKey.P:
                    c = 0x13;
                    break;
                case VirtualKey.PageDown:
                    break;
                case VirtualKey.PageUp:
                    break;
                case VirtualKey.Pause:
                    break;
                case VirtualKey.Print:
                    break;
                case VirtualKey.Q:
                    c = 0x14;
                    break;
                case VirtualKey.R:
                    c = 0x15;
                    break;
                case VirtualKey.Refresh:
                    break;
                case VirtualKey.Right:
                    c = 0x5E;
                    break;
                case VirtualKey.RightButton:
                    break;
                case VirtualKey.RightControl:
                    break;
                case VirtualKey.RightMenu:
                    break;
                case VirtualKey.RightShift:
                    break;
                case VirtualKey.RightWindows:
                    break;
                case VirtualKey.S:
                    c = 0x16;
                    break;
                case VirtualKey.Scroll:
                    break;
                case VirtualKey.Search:
                    break;
                case VirtualKey.Select:
                    break;
                case VirtualKey.Separator:
                    break;
                case VirtualKey.Shift:
                    break;
                case VirtualKey.Sleep:
                    break;
                case VirtualKey.Snapshot:
                    c = 0x36;
                    break;
                case VirtualKey.Space:
                    c = 0x2C;
                    break;
                case VirtualKey.Stop:
                    break;
                case VirtualKey.Subtract:
                    c = 0x2D;
                    break;
                case VirtualKey.T:
                    c = 0x17;
                    break;
                case VirtualKey.Tab:
                    c = 0x2B;
                    break;
                case VirtualKey.U:
                    c = 0x18;
                    break;
                case VirtualKey.Up:
                    c = 0x60;
                    break;
                case VirtualKey.V:
                    c = 0x19;
                    break;
                case VirtualKey.W:
                    c = 0x1A;
                    break;
                case VirtualKey.X:
                    c = 0x1B;
                    break;
                case VirtualKey.XButton1:
                    break;
                case VirtualKey.XButton2:
                    break;
                case VirtualKey.Y:
                    c = 0x1C;
                    break;
                case VirtualKey.Z:
                    c = 0x1D;
                    break;
                case (VirtualKey)190://.
                    c = 0x37;
                    break;
                case (VirtualKey)189://-
                    c = 0x2D;
                    break;
                case (VirtualKey)188://,
                    c = 0x36;
                    break;
                case (VirtualKey)186://; | :
                    c = 0x33;
                    break;
                case (VirtualKey)222://' | "
                    c = 0x34;
                    break;
                case (VirtualKey)191://? | /
                    c = 0x38;
                    break;
                case (VirtualKey)220://\ | |
                    c = 0x31;
                    break;
                case (VirtualKey)219://[ | {
                    c = 0x2F;
                    break;
                case (VirtualKey)221://] | }
                    c = 0x30;
                    break;
                case (VirtualKey)187://+
                    c = 0x2E;
                    break;
                case (VirtualKey)192://~
                    c = 0x32;
                    break;
                default:
                    c = 0;
                    break;
            }
            return c;
        }

        internal static byte GetShiftCode(VirtualKey virtualKey, byte current, bool isDown)
        {
            byte result = 0;

            CoreVirtualKeyStates shiftState = Window.Current.CoreWindow.GetAsyncKeyState(VirtualKey.Shift);
            CoreVirtualKeyStates capitalLockState = Window.Current.CoreWindow.GetAsyncKeyState(VirtualKey.CapitalLock);

            bool shiftIsDown = shiftState != CoreVirtualKeyStates.None || 
                capitalLockState != CoreVirtualKeyStates.None ||
                virtualKey == VirtualKey.Shift ||
                virtualKey == VirtualKey.LeftShift ||
                virtualKey == VirtualKey.RightShift;

            if (shiftIsDown)
            {
                int bitIndex = 1;
                byte mask = (byte)(1 << bitIndex);
                result |= mask;
            }

            return result;
        }
    }
}

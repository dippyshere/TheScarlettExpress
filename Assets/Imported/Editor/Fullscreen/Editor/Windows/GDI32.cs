#region

using System;
using System.Runtime.InteropServices;

#endregion

namespace FullscreenEditor.Windows
{
    static class GDI32
    {
        // http://pinvoke.net/default.aspx/gdi32/GetDeviceCaps.html
        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117
        }

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(IntPtr hDc, int nIndex);
    }
}
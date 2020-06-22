using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace UnityLoader
{
    public class UnityLibrary
    {
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool CloseWindow(IntPtr hWnd);

        private const int WM_ACTIVATE = 6;
        private const int WA_INACTIVE = 0;
        private const int WA_ACTIVE = 1;

        protected void UpdateUnityHwnd(IntPtr windowHandle)
        {
            EnumChildWindows(windowHandle, (hwnd, lparam) =>
            {
                UnityHwnd = hwnd;
                ActivateUnityWindow();
                return false;
            }, IntPtr.Zero);
        }

        public IntPtr UnityHwnd { get; private set; } = IntPtr.Zero;


        public void ActivateUnityWindow()
        {
            SendMessage(UnityHwnd, WM_ACTIVATE, WA_ACTIVE, IntPtr.Zero);
        }

        public void DeactivateUnityWindow()
        {
            SendMessage(UnityHwnd, WM_ACTIVATE, WA_INACTIVE, IntPtr.Zero);
        }

        public void ResizeUnityWindow(int width, int height)
        {
            MoveWindow(UnityHwnd, 0, 0, width, height, true);
            ActivateUnityWindow();
        }

        public virtual void CloseUnityWindow()
        {
            CloseWindow(UnityHwnd);
        }

        protected string CreateArgument(IntPtr windowHandle) => string.Format("-ParentHWND {0}", Environment.Is64BitProcess ? windowHandle.ToInt64() : windowHandle.ToInt32());
    }
}

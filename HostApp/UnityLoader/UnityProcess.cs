using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace UnityLoader
{
    public class UnityProcess
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

        private Process _process = null;
        private IntPtr _unityHwnd = IntPtr.Zero;

        public UnityProcess(string unityExePath, IntPtr windowHandle)
        {
            var startInfo = new ProcessStartInfo()
            {
                FileName = unityExePath,
                Arguments = string.Format("-ParentHWND {0}", Environment.Is64BitProcess ? windowHandle.ToInt64() : windowHandle.ToInt32()),
                UseShellExecute = true,
                CreateNoWindow = true,
            };

            _process = Process.Start(startInfo);
            _process.WaitForInputIdle();

            EnumChildWindows(windowHandle, (hwnd, lparam) =>
            {
                _unityHwnd = hwnd;
                ActivateUnityWindow();
                return false;
            }, IntPtr.Zero);
        }

        public void ActivateUnityWindow()
        {
            SendMessage(_unityHwnd, WM_ACTIVATE, WA_ACTIVE, IntPtr.Zero);
        }

        public void DeactivateUnityWindow()
        {
            SendMessage(_unityHwnd, WM_ACTIVATE, WA_INACTIVE, IntPtr.Zero);
        }

        public void ResizeUnityWindow(int width, int height)
        {
            MoveWindow(_unityHwnd, 0, 0, width, height, true);
            ActivateUnityWindow();
        }

        public void CloseUnityWindow()
        {
            CloseWindow(_unityHwnd);
            Thread.Sleep(1000);
            if (!_process.HasExited)
            {
                _process.Kill();
            }
        }
    }
}

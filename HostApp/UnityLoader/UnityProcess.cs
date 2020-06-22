using System;
using System.Diagnostics;
using System.Threading;

namespace UnityLoader
{
    public class UnityProcess : UnityLibrary
    {
        private Process _process = null;

        public UnityProcess(string unityExePath, IntPtr windowHandle)
        {
            var startInfo = new ProcessStartInfo()
            {
                FileName = unityExePath,
                Arguments = CreateArgument(windowHandle),
                UseShellExecute = true,
                CreateNoWindow = true,
            };

            _process = Process.Start(startInfo);
            _process.WaitForInputIdle();

            UpdateUnityHwnd(windowHandle);
        }

        public override void CloseUnityWindow()
        {
            base.CloseUnityWindow();
            Thread.Sleep(1000);
            if (!_process.HasExited)
            {
                _process.Kill();
            }
        }
    }
}

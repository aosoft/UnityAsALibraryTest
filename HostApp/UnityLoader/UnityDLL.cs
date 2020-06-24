using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace UnityLoader
{
    public class UnityDLL : UnityLibrary, IDisposable
    {
		[DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
		private static extern IntPtr LoadLibrary(string lpFileName);

		[DllImport("kernel32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool FreeLibrary(IntPtr hModule);

		[DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        private IntPtr _dll = IntPtr.Zero;
		private delegate int FnUnityMain(IntPtr hInstance, IntPtr hPrevInstance, [MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine, int nShowCmd);

		public UnityDLL(string unityDllPath, IntPtr windowHandle)
        {
			_dll = LoadLibrary(unityDllPath);
			var p = GetProcAddress(_dll, "UnityMain");
			var fnUnityMain = Marshal.GetDelegateForFunctionPointer<FnUnityMain>(p);

			var arg = CreateArgument(windowHandle);
			fnUnityMain(Process.GetCurrentProcess().Handle, IntPtr.Zero, arg, 1);


			UpdateUnityHwnd(windowHandle);
        }

		public void Dispose()
		{
			if (_dll != IntPtr.Zero)
            {
				FreeLibrary(_dll);
				_dll = IntPtr.Zero;
            }
		}
	}
}

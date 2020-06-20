using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnityLoader;

namespace WinFormsHost1
{
    public partial class Form1 : Form
    {
        private UnityProcess _unityProcess = null;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _unityProcess = new UnityProcess(@"D:\Git\UnityAsALibraryTest\UnityLibrary\Build\Release\UnityLibrary.exe", _splitContainer.Panel2.Handle);

            _splitContainer.Panel2.SizeChanged += (s, e) => _unityProcess?.ResizeUnityWindow(_splitContainer.Panel2.Width, _splitContainer.Panel2.Height);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _unityProcess?.CloseUnityWindow();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            _unityProcess?.ActivateUnityWindow();
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            _unityProcess?.DeactivateUnityWindow();
        }
    }
}

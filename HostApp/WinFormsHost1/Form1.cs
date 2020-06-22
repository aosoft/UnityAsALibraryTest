using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnityLibrary.Server.Hubs;
using UnityLoader;

namespace WinFormsHost1
{
    public partial class Form1 : Form
    {
        private UnityLoader.UnityLibrary _unityProcess = null;
        private AnimeType _animeType = AnimeType.Standing;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _unityProcess = new UnityLoader.UnityLibrary(@"UnityLibrary\UnityLibrary.exe", _splitContainer.Panel2.Handle);

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

        private void BtnAnimation_OnClick(object sender, EventArgs e)
        {
            _animeType++;
            if (_animeType > AnimeType.Running)
            {
                _animeType = AnimeType.Standing;
            }
            UnityChanController.Current?.SetAnimation(_animeType);
        }

        private void TextBox_OnTextChanged(object sender, EventArgs e)
        {
            UnityChanController.Current?.SetMessageText(_textBox.Text);
        }
    }
}

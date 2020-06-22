using System;
using System.ComponentModel;
using System.Windows.Forms;
using UnityLibrary.Server.Hubs;
using UnityLoader;

namespace WinFormsHost1
{
    public partial class Form1 : Form
    {
        private UnityLoader.UnityLibrary _unityLibrary = null;
        private AnimeType _animeType = AnimeType.Standing;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _unityLibrary = new UnityProcess(@"UnityLibrary\UnityLibrary.exe", _splitContainer.Panel2.Handle);
            //_unityLibrary = new UnityDLL(@"UnityPlayer.dll", _splitContainer.Panel2.Handle);

            _splitContainer.Panel2.SizeChanged += (s, e) => _unityLibrary?.ResizeUnityWindow(_splitContainer.Panel2.Width, _splitContainer.Panel2.Height);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _unityLibrary?.CloseUnityWindow();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            (_unityLibrary as IDisposable)?.Dispose();
            _unityLibrary = null;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            _unityLibrary?.ActivateUnityWindow();
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            _unityLibrary?.DeactivateUnityWindow();
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

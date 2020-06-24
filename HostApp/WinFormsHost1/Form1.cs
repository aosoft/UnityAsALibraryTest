using System;
using System.ComponentModel;
using System.Windows.Forms;
using Grpc.Core;
using MagicOnion.Client;
using UnityLibrary.Server.Hubs;
using UnityLoader;

namespace WinFormsHost1
{
    public partial class Form1 : Form, IUnityChanControllerReceiver
    {
        private UnityLoader.UnityLibrary _unityLibrary = null;
        private AnimeType _animeType = AnimeType.Standing;

        private Channel _channel = null;
        private IUnityChanController _controller = null;

        public Form1()
        {
            InitializeComponent();
            _channel = new Channel("localhost:12345", ChannelCredentials.Insecure);
            _controller = StreamingHubClient.Connect<IUnityChanController, IUnityChanControllerReceiver>(this._channel, this);
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await _controller?.RegisterAsync();
            _unityLibrary = new UnityProcess(@"UnityLibrary\UnityLibrary.exe", _splitContainer.Panel2.Handle);
            //_unityLibrary = new UnityDLL(@"UnityPlayer.dll", _splitContainer.Panel2.Handle);

            _splitContainer.Panel2.SizeChanged += (s, e) => _unityLibrary?.ResizeUnityWindow(_splitContainer.Panel2.Width, _splitContainer.Panel2.Height);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _unityLibrary?.CloseUnityWindow();
        }

        protected override async void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            (_unityLibrary as IDisposable)?.Dispose();
            _unityLibrary = null;
            await _controller?.UnregisterAsync();
            await _controller?.DisposeAsync();
            _controller = null;
            await _channel?.ShutdownAsync();
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
            _controller?.SetAnimationAsync(_animeType);
        }

        private void TextBox_OnTextChanged(object sender, EventArgs e)
        {
            _controller?.SetMessageTextAsync(_textBox.Text);
        }

        void IUnityChanControllerReceiver.OnSetAnimation(AnimeType animeType)
        {
        }

        void IUnityChanControllerReceiver.OnSetMessageText(string msg)
        {
        }
    }
}

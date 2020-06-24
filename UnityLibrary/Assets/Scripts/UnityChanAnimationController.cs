using UnityEngine;
using UnityEngine.UI;
using UnityLibrary.Server.Hubs;
using Grpc.Core;
using MagicOnion.Client;

[RequireComponent(typeof(Animator))]
public class UnityChanAnimationController : MonoBehaviour, IUnityChanControllerReceiver
{
    private Animator _animator;

    [SerializeField]
    private Canvas _rootUI;
    [SerializeField]
    private Text _text;

    private Channel _channel = null;
    private IUnityChanController _controller = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _channel = new Channel("localhost:12345", ChannelCredentials.Insecure);
        _controller = StreamingHubClient.Connect<IUnityChanController, IUnityChanControllerReceiver>(this._channel, this);
    }
    private async void Start()
    {
        await _controller?.RegisterAsync();
    }

    private async void OnDestroy()
    {
        await _controller?.UnregisterAsync();
        await _controller?.DisposeAsync();
        _controller = null;
        await _channel?.ShutdownAsync();
        _channel = null;
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - 110, 10, 100, 90), "Change Motion");
        if (GUI.Button(new Rect(Screen.width - 100, 40, 80, 20), "Standing"))
        {
            (this as IUnityChanControllerReceiver).OnSetAnimation(AnimeType.Standing);
        }
        if (GUI.Button(new Rect(Screen.width - 100, 70, 80, 20), "Walking"))
        {
            (this as IUnityChanControllerReceiver).OnSetAnimation(AnimeType.Walking);
        }
    }
#endif

    void IUnityChanControllerReceiver.OnSetAnimation(AnimeType animeType)
    {
        switch (animeType)
        {
            case AnimeType.Standing: _animator.Play("Standing@loop"); break;
            case AnimeType.Walking: _animator.Play("Walking@loop"); break;
            case AnimeType.Running: _animator.Play("Running@loop"); break;
            case AnimeType.Jump: _animator.Play("JumpToTop"); break;
        }
    }

    void IUnityChanControllerReceiver.OnSetMessageText(string msg)
    {
        if (string.IsNullOrEmpty(msg))
        {
            _rootUI.gameObject.SetActive(false);
        }
        else
        {
            _text.text = msg;
            _rootUI.gameObject.SetActive(true);
        }
    }

    private void OnCallChangeFace(string str)
    {
    }
}

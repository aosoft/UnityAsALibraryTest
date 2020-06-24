namespace UnityLibrary.Server.Hubs
{
    public enum AnimeType
    {
        Unspecified,
        Standing,
        Walking,
        Running,
        Jump
    }

    public interface IUnityChanControllerReceiver
    {
        void OnSetAnimation(AnimeType animeType);
        void OnSetMessageText(string msg);
    }
}

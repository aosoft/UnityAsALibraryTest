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
        void SetAnimation(AnimeType animeType);
        void SetMessageText(string msg);
    }
}

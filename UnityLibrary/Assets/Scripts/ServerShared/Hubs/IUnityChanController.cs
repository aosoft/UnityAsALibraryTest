using System.Threading.Tasks;
using MagicOnion;

namespace UnityLibrary.Server.Hubs
{
    public interface IUnityChanController : IStreamingHub<IUnityChanController, IUnityChanControllerReceiver>
    {
        Task RegisterAsync();
        Task UnregisterAsync();

        Task SetAnimationAsync(AnimeType animeType);
        Task SetMessageTextAsync(string msg);
    }
}
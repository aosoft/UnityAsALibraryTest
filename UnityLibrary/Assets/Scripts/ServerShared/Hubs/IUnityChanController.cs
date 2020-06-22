using System.Threading.Tasks;
using MagicOnion;

namespace UnityLibrary.Server.Hubs
{
    public interface IUnityChanController : IStreamingHub<IUnityChanController, IUnityChanControllerReceiver>
    {
        Task Register();
        Task Unregister();
    }
}
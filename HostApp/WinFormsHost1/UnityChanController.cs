using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MagicOnion.Server.Hubs;
using UnityLibrary.Server.Hubs;

namespace WinFormsHost1
{
    class UnityChanController : StreamingHubBase<IUnityChanController, IUnityChanControllerReceiver>, IUnityChanController
    {
        private IGroup _group;

        async Task IUnityChanController.RegisterAsync()
        {
            _group = await Group.AddAsync("UnityLibrary");
        }

        Task IUnityChanController.UnregisterAsync()
        {
            return _group?.RemoveAsync(Context).AsTask();
        }

        Task IUnityChanController.SetAnimationAsync(AnimeType animeType)
        {
            Broadcast(_group).OnSetAnimation(animeType);
            return Task.CompletedTask;
        }

        Task IUnityChanController.SetMessageTextAsync(string msg)
        {
            Broadcast(_group).OnSetMessageText(msg);
            return Task.CompletedTask;
        }
    }
}

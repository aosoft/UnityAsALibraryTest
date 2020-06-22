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

        public void SetAnimation(AnimeType animeType)
        {
            Broadcast(_group).SetAnimation(animeType);
        }

        public void SetMessageText(string msg)
        {
            Broadcast(_group).SetMessageText(msg);
        }

        async Task IUnityChanController.Register()
        {
            _group = await Group.AddAsync("UnityLibrary");
            Current = this;
        }

        Task IUnityChanController.Unregister()
        {
            Current = null;
            return _group?.RemoveAsync(Context).AsTask();
        }

        public static UnityChanController Current { get; private set; }
    }
}

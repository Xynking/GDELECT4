using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CDR.ActionSystem
{
    public interface ICooldownAction : IAction
    {
        float cooldownDuration { get; }
        float currentCooldown { get; }
        bool isCoolingDown { get; }

        event Action<ICooldownAction> onStartCoolDown;

        event Action<ICooldownAction> onCoolDown;

        event Action<ICooldownAction> onEndCoolDown;

        void EndCoolDown();
    }
}


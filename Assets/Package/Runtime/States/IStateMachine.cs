using System;
using System.Collections.Generic;

namespace BroWar.Common.States
{
    public interface IStateMachine
    {
        event Action<BaseState> OnStateChanged;

        void Start();
        void Reset();
        bool ChangeState(Type nextStateType);
        bool IsStateActive(BaseState state);

        BaseState CurrentState { get; }
        IReadOnlyCollection<BaseState> States { get; }
    }
}
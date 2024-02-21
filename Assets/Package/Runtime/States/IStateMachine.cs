using System;
using System.Collections.Generic;

namespace BroWar.Common.States
{
    public interface IStateMachine
    {
        event Action<IState> OnStateChanged;

        void Start();
        void Reset();
        bool ChangeState(Type nextStateType);
        bool IsStateActive(IState state);

        IState OngoingState { get; }
        IState CurrentState { get; }
        IReadOnlyCollection<IState> States { get; }
    }
}
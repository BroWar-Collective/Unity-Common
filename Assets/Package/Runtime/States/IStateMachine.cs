using System;
using System.Collections.Generic;

namespace BroWar.Common.States
{
    public interface IStateMachine : IDisposable
    {
        /// <summary>
        /// Fired each time <see cref="CurrentState"/> is changed.
        /// </summary>
        event Action<IState> OnStateChanged;

        /// <summary>
        /// Begins new state using the first one in the cached collection or already declared starting state.
        /// </summary>
        void Start();
        /// <summary>
        /// Closes <see cref="CurrentState"/> if available and resets State Machine.
        /// </summary>
        void Reset();
        /// <inheritdoc cref="Reset" />
        void Reset(bool closeState);
        /// <summary>
        /// Updates <see cref="CurrentState"/> and <see cref="OngoingState"/>. 
        /// Tries to change state if conditions are met.
        /// </summary>
        void Tick();
        bool ChangeState(Type nextStateType);
        bool IsStateActive(IState state);

        /// <summary>
        /// Custom <see cref="IState"/> that is active all the time.
        /// It's destinations has priority over the standard flow.
        /// </summary>
        IState OngoingState { get; }
        /// <summary>
        /// Currently active <see cref="IState"/>.
        /// </summary>
        IState CurrentState { get; }
        /// <summary>
        /// All states added to the State Machine.
        /// Doesn't include <see cref="OngoingState"/>.
        /// </summary>
        IReadOnlyCollection<IState> States { get; }
        /// <summary>
        /// Indicates whether the state machine has cached states.
        /// </summary>
        public bool HasStates { get; }
        /// <summary>
        /// Indicates whether the state machine has an active state.
        /// </summary>
        public bool IsWorking { get; }
    }
}
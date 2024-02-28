using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BroWar.Common.States
{
    /// <summary>
    /// Basic state machine, contains logic for updating and switching associated states.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseStateMachine<T> : IStateMachine where T : class, IState
    {
        private readonly Type startState;
        private readonly Dictionary<Type, T> statesByType;

        public event Action<IState> OnStateChanged;

        public BaseStateMachine(IReadOnlyList<T> states) : this(states, null)
        { }

        public BaseStateMachine(IReadOnlyList<T> states, Type startState) : this(states, startState, null)
        { }

        public BaseStateMachine(IReadOnlyList<T> states, Type startState, T ongoingState)
        {
            this.startState = startState;
            var statesCount = states != null ? states.Count : 0;
            statesByType = new Dictionary<Type, T>(statesCount);
            for (var i = 0; i < statesCount; i++)
            {
                var state = states[i];
                AppendState(state);
            }

            OngoingState = ongoingState;
        }

        protected virtual bool TryProgressFromState(T sourceState)
        {
            if (!sourceState.WantsToClose())
            {
                return false;
            }

            IReadOnlyList<Type> destinations = sourceState.GetDestinations();
            for (int i = 0; i < destinations.Count; i++)
            {
                Type destination = destinations[i];
                if (destination == null)
                {
                    Reset();
                    return true;
                }

                if (TryGetState(destination, out T state))
                {
                    if (state == CurrentState)
                    {
                        continue;
                    }

                    if (state.WantsToBegin())
                    {
                        ChangeState(state);
                        return true;
                    }
                }
                else
                {
                    LogHandler.Log($"[States] Cannot find state of type: {destination.Name}.", LogType.Warning);
                }
            }

            return false;
        }

        protected virtual T GetStartState()
        {
            if (startState != null && statesByType.TryGetValue(startState, out T state))
            {
                return state;
            }

            return statesByType.FirstOrDefault(state => state.Value.WantsToBegin()).Value;
        }

        /// <inheritdoc />
        public void Start()
        {
            if (!HasStates)
            {
                LogHandler.Log($"[States] Cannot start {nameof(IStateMachine)}. States are not available.", LogType.Warning);
                return;
            }

            var state = GetStartState();
            if (state == null)
            {
                LogHandler.Log($"[States] Cannot start {nameof(IStateMachine)}. Cannot find valid state.", LogType.Warning);
                return;
            }

            ChangeState(state);
        }

        /// <inheritdoc />
        public void Tick()
        {
            if (OngoingState != null)
            {
                OngoingState.Tick();
                if (TryProgressFromState(OngoingState))
                {
                    return;
                }
            }

            if (CurrentState != null)
            {
                CurrentState.Tick();
                TryProgressFromState(CurrentState);
            }
        }

        /// <inheritdoc />
        public void Reset()
        {
            CurrentState?.CloseState();
            CurrentState = null;
        }

        public virtual bool AppendState(T state, bool setAsCurrent = false)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            var stateType = state.Type;
            if (statesByType.ContainsKey(stateType))
            {
                LogHandler.Log($"[States] {stateType} is already registered.", LogType.Warning);
                return false;
            }

            statesByType.Add(stateType, state);
            if (setAsCurrent)
            {
                ChangeState(stateType);
            }

            return true;
        }

        public virtual bool RemoveState(T state)
        {
            var stateExisted = statesByType.Remove(state.Type);
            if (stateExisted && CurrentState == state)
            {
                Reset();
            }

            return stateExisted;
        }

        public virtual bool ChangeState(T state)
        {
            return ChangeState(state.Type);
        }

        public virtual bool ChangeState(Type nextStateType)
        {
            if (!statesByType.TryGetValue(nextStateType, out var nextState))
            {
                return false;
            }

            CurrentState?.CloseState();
            CurrentState = nextState;
            CurrentState?.BeginState();
            OnStateChanged?.Invoke(CurrentState);
            return true;
        }

        public bool ContainsState(Type stateType)
        {
            return statesByType.ContainsKey(stateType);
        }

        public bool TryGetState<TState>(Type stateType, out TState state) where TState : class, T
        {
            state = statesByType.TryGetValue(stateType, out T s) ? s as TState : null;
            return state != null;
        }

        public bool IsStateActive(IState state)
        {
            return CurrentState == state;
        }

        /// <summary>
        /// Indicates whether the state machine has cached states.
        /// </summary>
        public bool HasStates => statesByType.Count > 0;
        /// <summary>
        /// Indicates whether the state machine has an active state.
        /// </summary>
        public bool IsWorking => CurrentState != null;

        /// <inheritdoc cref="IStateMachine.OngoingState"/>
        public T OngoingState { get; set; }
        /// <inheritdoc cref="IStateMachine.CurrentState"/>
        public T CurrentState { get; private set; }
        /// <inheritdoc cref="IStateMachine.States"/>
        public IReadOnlyCollection<T> States => statesByType.Values;

        IState IStateMachine.OngoingState => OngoingState;
        IState IStateMachine.CurrentState => CurrentState;
        IReadOnlyCollection<IState> IStateMachine.States => States.ToList<IState>();
    }
}
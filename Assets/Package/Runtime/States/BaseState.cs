using System;
using System.Collections.Generic;

namespace BroWar.Common.States
{
    public abstract class BaseState : IState
    {
        private readonly List<Type> destinationStates = new List<Type>();

        /// <inheritdoc />
        public void AppendDestination<T>() where T : IState
        {
            AppendDestination(typeof(T));
        }

        /// <inheritdoc />
        public void AppendDestination(Type stateType)
        {
            destinationStates.Insert(0, stateType);
        }

        /// <inheritdoc />
        public void RemoveDestination<T>() where T : IState
        {
            RemoveDestination(typeof(T));
        }

        /// <inheritdoc />
        public void RemoveDestination(Type stateType)
        {
            destinationStates.Remove(stateType);
        }

        /// <inheritdoc />
        public void ClearDestinations()
        {
            destinationStates.Clear();
        }

        /// <inheritdoc />
        public IReadOnlyList<Type> GetDestinations()
        {
            return destinationStates;
        }

        /// <inheritdoc />
        public virtual void BeginState()
        { }

        /// <inheritdoc />
        public virtual void CloseState()
        { }

        /// <inheritdoc />
        public virtual void Tick()
        { }

        /// <inheritdoc />
        public virtual bool WantsToBegin()
        {
            return true;
        }

        /// <inheritdoc />
        public virtual bool WantsToClose()
        {
            return true;
        }

        /// <inheritdoc />
        public virtual bool Represents(Type type)
        {
            return Type == type;
        }

        public override string ToString()
        {
            return Type.Name;
        }

        /// <inheritdoc />
        public virtual Type Type
        {
            get => GetType();
        }

        public static implicit operator Type(BaseState s) => s.Type;
    }
}
using System;
using System.Collections.Generic;

namespace BroWar.Common.States
{
    public abstract class BaseState
    {
        private readonly List<Type> destinationStates = new List<Type>();

        /// <summary>
        /// Appends new destination <see cref="BaseState"/> to the collection of possible destinations.
        /// </summary>
        public void AppendDestination<T>() where T : BaseState
        {
            AppendDestination(typeof(T));
        }

        /// <summary>
        /// Appends new destination to the collection of possible destinations.
        /// If the given <see cref="Type"/> equals to <see langword="null"/> this will be treated as a "reset" option.
        /// </summary>
        public void AppendDestination(Type stateType)
        {
            destinationStates.Insert(0, stateType);
        }

        /// <summary>
        /// Removes destination <see cref="BaseState"/> from the collection of possible destinations.
        /// </summary>
        public void RemoveDestination<T>() where T : BaseState
        {
            RemoveDestination(typeof(T));
        }

        /// <summary>
        /// Removes destination from the collection of possible destinations.
        /// </summary>
        public void RemoveDestination(Type stateType)
        {
            destinationStates.Remove(stateType);
        }

        /// <summary>
        /// Completely clears possible destination <see cref="BaseState"/>s.
        /// </summary>
        public void ClearDestinations()
        {
            destinationStates.Clear();
        }

        /// <summary>
        /// Returns all possible destinations from this <see cref="BaseState"/>.
        /// </summary>
        public IReadOnlyList<Type> GetDestinations()
        {
            return destinationStates;
        }

        public virtual void BeginState()
        { }

        public virtual void CloseState()
        { }

        /// <summary>
        /// Updates <see cref="BaseState"/>.
        /// </summary>
        public virtual void Tick()
        { }

        /// <summary>
        /// Indicates if this <see cref="BaseState"/> is ready to be safely activated and working.
        /// </summary>
        public virtual bool WantsToBegin()
        {
            return true;
        }

        /// <summary>
        /// Indicates if this <see cref="BaseState"/> is ready (current work is done) to be inactive.
        /// </summary>
        public virtual bool WantsToClose()
        {
            return true;
        }

        public virtual bool Represents(Type type)
        {
            return Type == type;
        }

        public override string ToString()
        {
            return Type.Name;
        }

        public virtual Type Type
        {
            get => GetType();
        }

        public static implicit operator Type(BaseState s) => s.Type;
    }
}
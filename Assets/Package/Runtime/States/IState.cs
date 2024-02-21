using System;
using System.Collections.Generic;

namespace BroWar.Common.States
{
    public interface IState
    {
        /// <summary>
        /// Appends new destination <see cref="IState"/> to the collection of possible destinations.
        /// </summary>
        void AppendDestination<T>() where T : IState;

        /// <summary>
        /// Appends new destination to the collection of possible destinations.
        /// If the given <see cref="Type"/> equals to <see langword="null"/> this will be treated as a "reset" option.
        /// </summary>
        void AppendDestination(Type stateType);

        /// <summary>
        /// Removes destination <see cref="IState"/> from the collection of possible destinations.
        /// </summary>
        void RemoveDestination<T>() where T : IState;

        /// <summary>
        /// Removes destination from the collection of possible destinations.
        /// </summary>
        void RemoveDestination(Type stateType);

        /// <summary>
        /// Completely clears possible destination <see cref="IState"/>s.
        /// </summary>
        void ClearDestinations();

        /// <summary>
        /// Returns all possible destinations from this <see cref="IState"/>.
        /// </summary>
        IReadOnlyList<Type> GetDestinations();

        void BeginState();
        void CloseState();
        void Tick();

        /// <summary>
        /// Indicates whether this <see cref="IState"/> is ready to be safely activated and working.
        /// </summary>
        bool WantsToBegin();

        /// <summary>
        /// Indicates whether this <see cref="IState"/> is ready (current work is done) to be inactive.
        /// </summary>
        bool WantsToClose();

        bool Represents(Type type);

        Type Type { get; }
    }
}
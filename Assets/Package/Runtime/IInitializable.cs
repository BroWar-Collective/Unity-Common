using System;

namespace BroWar.Common
{
    public interface IInitializable
    {
        event Action OnInitialized;

        void Initialize();

        bool IsInitialized { get; }
    }
}
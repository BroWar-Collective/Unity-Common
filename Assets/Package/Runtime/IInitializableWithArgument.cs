using System;

namespace BroWar.Common
{
    public interface IInitializableWithArgument<T>
    {
        event Action OnInitialized;

        void Initialize(T argument);

        bool IsInitialized { get; }
    }
}
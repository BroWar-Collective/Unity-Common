using System;

namespace BroWar.Common
{
    public interface IDeinitializable
    {
        event Action OnDeinitialized;

        void Deinitialize();
    }
}
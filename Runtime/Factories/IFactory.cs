using System;

namespace BroWar.Common.Factories
{
    public interface IFactory<T>
    {
        T Create<TRef>() where TRef : T;
        T Create(Type type);
        void Dispose(T target);
    }
}
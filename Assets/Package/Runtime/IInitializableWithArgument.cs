namespace BroWar.Common
{
    public interface IInitializableWithArgument<T> : IInitializable
    {
        void Initialize(T argument);
    }
}
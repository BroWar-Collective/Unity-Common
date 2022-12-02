namespace BroWar.Common
{
    public interface IInitializable
    {
        void Initialize();

        bool IsInitialized { get; }
    }
}
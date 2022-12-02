namespace BroWar.Common
{
    public interface ITickableWithTime
    {
        void Tick(float time, float deltaTime);
    }
}
namespace BroWar.Common
{
    public interface ITickableManager : ITickable
    {
        void BeginTicking();
        void CloseTicking();

        bool IsActive { get; set; }
    }
}
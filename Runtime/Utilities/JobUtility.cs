using Unity.Jobs;

namespace BroWar.Common.Utilities
{
    public static class JobUtility
    {
        public static void StartAndComplete<T>(in T job, int length, int batchCount = 16) where T : struct, IJobParallelFor
        {
            job.Schedule(length, batchCount).Complete();
        }
    }
}
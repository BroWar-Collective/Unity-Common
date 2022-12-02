using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace BroWar.Common.Utilities
{
    public static class Timer
    {
        private static readonly Dictionary<string, Stopwatch> stopwatches = new Dictionary<string, Stopwatch>();
        private static string lastLabel;

        [Conditional("UNITY_EDITOR")]
        public static void Start(string label)
        {
            lastLabel = label;
            if (stopwatches.TryGetValue(label, out var stopwatch))
            {
                stopwatch.Restart();
                return;
            }

            stopwatches[label] = Stopwatch.StartNew();
        }

        [Conditional("UNITY_EDITOR")]
        public static void Stop()
        {
            Stop(lastLabel);
        }

        [Conditional("UNITY_EDITOR")]
        public static void Stop(string label)
        {
            if (!stopwatches.TryGetValue(label, out var stopwatch))
            {
                Debug.LogWarning($"[Timer] Cannot find Stopwatch associated to the label:{label}.");
                return;
            }

            stopwatch.Stop();
            Debug.Log($"[Timer] {label}:{stopwatch.ElapsedTicks}t");
        }
    }
}
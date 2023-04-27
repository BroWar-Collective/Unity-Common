using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

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
                LogHandler.Log($"[Timer] Cannot find Stopwatch associated to the label:{label}.", LogType.Warning);
                return;
            }

            stopwatch.Stop();
            LogHandler.Log($"[Timer] {label}:{stopwatch.ElapsedTicks}t");
        }
    }
}
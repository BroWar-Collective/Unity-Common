using System.Collections.Generic;
using System.Reflection;
using Unity.Collections;
using UnityEngine;

namespace BroWar.Common.Utilities
{
    public static class ListExtensions
    {
        /// <summary>
        /// Copies elements from <see cref="NativeArray{T}"/> instance to the internal array of <see cref="List{T}"/>.
        /// </summary>
        /// <remarks>If the list capacity is too small it gets doubled unless it equals or exceeds the native array size.</remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">Source list to get array copied to.</param>
        /// <param name="nativeArray">Native Array to get array copied from.</param>
        public static void CopyFrom<T>(this List<T> list, NativeArray<T> nativeArray) where T : unmanaged
        {
            var startCapacity = list.Capacity;
            var destinedCapacity = startCapacity == 0 ? nativeArray.Length - 1 : startCapacity;
            while (destinedCapacity < nativeArray.Length)
            {
                destinedCapacity *= 2;
            }

            if (destinedCapacity > startCapacity)
            {
                list.Capacity = destinedCapacity;
            }

            var listArray = (T[])typeof(List<T>).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(list);
            typeof(List<T>).GetField("_size", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(list, nativeArray.Length);
            NativeArray<T>.Copy(nativeArray, listArray, nativeArray.Length);
        }

        /// <summary>
        /// Returns random list element based on the <see cref="Random"/> API.
        /// </summary>
        public static T GetRandom<T>(this List<T> list)
        {
            var count = list.Count;
            if (count == 0)
            {
                return default;
            }

            return list[Random.Range(0, count)];
        }

        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }
    }
}
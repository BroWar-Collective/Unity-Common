using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BroWar.Common.Utilities
{
    public static class IEnumeratorExtensions
    {
        public static bool RunCoroutineWithoutYields(this IEnumerator enumerator, int maxYields = 1000)
        {
            var enumStack = new Stack<IEnumerator>();
            enumStack.Push(enumerator);

            var step = 0;
            while (enumStack.Count > 0)
            {
                var activeEnum = enumStack.Pop();
                while (activeEnum.MoveNext())
                {
                    switch (activeEnum.Current)
                    {
                        case IEnumerator current:
                            enumStack.Push(activeEnum);
                            activeEnum = current;
                            break;
                        case Coroutine _:
                            throw new NotSupportedException($"{nameof(RunCoroutineWithoutYields)} can not be used with an IEnumerator that calls StartCoroutine inside itself.");
                    }

                    step++;
                    if (step >= maxYields)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
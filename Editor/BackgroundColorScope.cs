using System;
using UnityEngine;

namespace BroWar.Common.Editor
{
    public class BackgroundColorScope : IDisposable
    {
        private readonly Color oldColor;

        public BackgroundColorScope(Color newColor)
        {
            oldColor = GUI.backgroundColor;
            GUI.backgroundColor = newColor;
        }

        public void Dispose()
        {
            GUI.backgroundColor = oldColor;
        }
    }
}
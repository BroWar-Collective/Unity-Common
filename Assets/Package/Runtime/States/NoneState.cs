using System;

namespace BroWar.Common.States
{
    /// <summary>
    /// Helper state used to represent a "none" direction.
    /// Using it as destination will automatically stop associated state machine.
    /// </summary>
    public class NoneState : BaseState
    {
        private NoneState()
        { }

        public static NoneState Instance { get; } = new NoneState();
        public override Type Type => null;
    }
}
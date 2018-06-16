using System;

namespace BlocksLibrary
{
    [Serializable]
    public class SignalEventArgs : EventArgs
    {
        public SignalEventArgs()
        {
        }


        public SignalEventArgs(Signal signal)
        {
            Signal = signal;
        }


        public Signal Signal { get; protected set; }
    }
}
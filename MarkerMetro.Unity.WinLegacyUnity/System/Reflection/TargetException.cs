#if NETFX_CORE

namespace MarkerMetro.Unity.WinLegacy.Reflection
{
    class TargetException : System.Exception
    {
        public TargetException() : base() { }
        public TargetException(string message) : base(message) { }
    }
}
#endif
#if UNITY_IPHONE 
using System.Runtime.InteropServices;
#endif


namespace AppCentralCore
{

    public static class IsDarkMode_Plugin
    {
#if UNITY_IPHONE
        [DllImport("__Internal")]
        private static extern bool _IsDakModeSet();
#endif

        public static bool IsDakModeSet()
        {
#if UNITY_IPHONE
            return _IsDakModeSet();
#endif
        }
    }

}
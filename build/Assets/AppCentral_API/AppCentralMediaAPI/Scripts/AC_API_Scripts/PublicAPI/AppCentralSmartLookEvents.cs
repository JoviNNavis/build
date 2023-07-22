using UnityEngine;
using AppCentralCore;

namespace AppCentralAPI
{
    public class AppCentralSmartLookEvents : MonoBehaviour
    {
        #region SmartLookEvents

        public static void SmartLookTrackLevelStart(int levelID)
        {
#if AC_SMARTLOOK

            ACLogger.UserDebug("SmartLookTrackLevelStart=" + "Level" + levelID);
            SmartlookUnity.Smartlook.TrackCustomEvent("Level" + levelID);

#endif
        }
      

        #endregion
    }
}

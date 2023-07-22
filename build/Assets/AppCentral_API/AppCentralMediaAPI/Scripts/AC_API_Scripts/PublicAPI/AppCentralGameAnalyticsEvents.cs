using UnityEngine;
using AppCentralCore;

#if AC_GAMEANALYTICS
using GameAnalyticsSDK;
#endif

namespace AppCentralAPI
{
    public class AppCentralGameAnalyticsEvents : MonoBehaviour
    {
#if AC_GAMEANALYTICS

#region GameAnalyticsProgressionEvents

        public static void SendLevelProgressionEvent(
            GAProgressionStatus progressionStatus,
            Progresion01 progresion01,
            Progresion02 progresion02,
            int modeID
        )
        {
            int currentLevelID = AppCentral.CurrentLevelID;

            int totalLevelCompleted = AppCentral.CurrentLevelID;

            if (
                progressionStatus == GAProgressionStatus.Start
                || progressionStatus == GAProgressionStatus.Fail
            )
            {
                totalLevelCompleted = AppCentral.CurrentLevelID - 1;
            }

            string p01 = progresion01.ToString() + currentLevelID;
            string p02 = progresion02.ToString();

            if (progresion02 != Progresion02.@default)
            {
                p02 += modeID.ToString();
            }

            string p03 = "total" + totalLevelCompleted.ToString();

            ACLogger.UserDebug("progresion01=" + progresion01.ToString());
            ACLogger.UserDebug("currentLevelID=" + currentLevelID);
            ACLogger.UserDebug("p01=" + p01);

            ACLogger.UserDebug(
                "SendLevelProgressionEvent=" + progressionStatus + "," + p01 + "," + p02 + "," + p03
            );

            GameAnalytics.NewProgressionEvent(progressionStatus, p01, p02, p03);

        }

#endregion


#region GameAnalyticsDesignEvents
        public static void SendLevelSkippedEvent(string eventName, float eventValue)
        {
            ACLogger.UserDebug("SendLevelSkippedEvent=" + eventName + eventValue);

            GameAnalytics.NewDesignEvent(eventName, eventValue);
        }

#endregion

#endif
    }
}

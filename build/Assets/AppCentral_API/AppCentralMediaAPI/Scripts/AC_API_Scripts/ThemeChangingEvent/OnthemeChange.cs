using UnityEngine;
using AppCentralCore;

namespace AppCentralAPI
{
    public class OnthemeChange : OnThemeChangeBase
    {
        /// <summary>
        ///
        /// Write your theme change logic inside the method below.
        /// this method wil be called automatically as soon as we receive the
        ///appcentral responce from server.

        public override void ThemeChangeEventReceiver()
        {
            // "theme2" will always refer as DEFAULT theme so, please makesure to set the DEFAULT theme when "currentThemeName" is "theme2".

            string currentThemeName = AppCentralThemeSwitchController.getCurrentThemeName();

            ACLogger.UserDebug("currentThemeName=" + currentThemeName);

            // please set your theme to **currentThemeName** .

            switch (currentThemeName)
            {
                case "theme1":
                    AppCentralPixelController.Instance.SaveAppCentralPixel("theme_pixel", new string[] { "action" }, new string[] { currentThemeName });
                    
                    ACLogger.UserDebug("theme is set to  theme1");



                    break;
                case "theme2":
                    AppCentralPixelController.Instance.SaveAppCentralPixel("theme_pixel", new string[] { "action" }, new string[] { currentThemeName });
 
                    ACLogger.UserDebug("theme is set to theme2 which is a default theme.");



                    break;
                default:
                    ACLogger.UserDebug("theme is set to theme2 which is a default theme.");
                    break;
            }
        }
    }
}

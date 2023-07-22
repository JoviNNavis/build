using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Brdsdk;

namespace Brdsdk.Demo
{
    /// <summary>
    /// Sample Settings Behaviour
    /// </summary>
    public class SettingsBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Flag indicating the state of "Settings" button
        /// </summary>
        private bool settingsShown = false;

        /// "Settings" button
        public GameObject settingsButton;
        /// "Enable" button
        public GameObject enableButton;
        /// "Disable" button
        public GameObject disableButton;

        void Start()
        {
            UpdateButtons();
        }

        void Update()
        {
            UpdateButtons();
        }

        /// <summary>
        /// Updates the state of the buttons
        /// </summary>
        private void UpdateButtons()
        {
            settingsButton.SetActive(!settingsShown);

            if (settingsShown)
            {
                if (BrdsdkBridge.get_choice() == BrdsdkBridge.CHOICE_NONE)
                {
                    enableButton.SetActive(false);
                    disableButton.SetActive(false);
                }
                else if (BrdsdkBridge.get_choice() == BrdsdkBridge.CHOICE_AGREED)
                {
                    enableButton.SetActive(false);
                    disableButton.SetActive(true);
                }
                else if (BrdsdkBridge.get_choice() == BrdsdkBridge.CHOICE_DISAGREED)
                {
                    enableButton.SetActive(true);
                    disableButton.SetActive(false);
                }
            }
            else
            {
                enableButton.SetActive(false);
                disableButton.SetActive(false);
            }
        }

        /// "Show Settings" button action handler
        public void onSettingsClicked()
        {
            settingsShown = true;
            UpdateButtons();
        }

        /// "Enable" button action handler
        public void onEnableSdkClicked()
        {
            Debug.Log("Enable BrightData SDK button clicked");
            string language = "es";
            BrdsdkBridge.show_consent(null, null, null, language);
            settingsShown = false;
        }

        /// "Disable" button action handler
        public void onDisableSdkClicked()
        {
            Debug.Log("Disable BrightData SDK button clicked");
            BrdsdkBridge.opt_out();
            settingsShown = false;
        }
    }
}

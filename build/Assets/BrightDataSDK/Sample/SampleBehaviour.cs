using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Brdsdk;

namespace Brdsdk.Demo
{
    /// <summary>
    /// Sample showing a possible SDK integration.
    /// </summary>
    public class SampleBehaviour : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            /// Initialize the SDK
			string language = null;
            bool skip_consent = false;
            BrdsdkBridge.init("To support this app", "I Agree", "I disagree", choiceChanged, skip_consent, language);
            Debug.Log("SDK Status: " + BrdsdkBridge.get_choice());
        }

        /// <summary>
        /// The method is called when user's choice is changed.
        /// </summary>
        /// <param name="choice">Value representing the user's choice:
        /// - BrdsdkBridge.CHOICE_NONE - the consent screen is not yet shown;
        /// - BrdsdkBridge.CHOICE_AGREED - user accepted the consent screen;
        /// - BrdsdkBridge.CHOICE_DISAGREED - user declined the consent screen.
        /// </param>
        void choiceChanged(int choice)
        {
            Debug.Log("SDK Status: " + BrdsdkBridge.get_choice());
            if (choice == BrdsdkBridge.CHOICE_AGREED)
            {
                Debug.Log("Skip ads (SDK is enabled)");
            }
            else
            {
                Debug.Log("TODO Initialize ads (SDK is disabled)");
            }
        }
    }
}

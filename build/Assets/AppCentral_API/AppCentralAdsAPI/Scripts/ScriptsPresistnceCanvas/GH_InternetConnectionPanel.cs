using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using AppCentralCore;

namespace PresistanceCanvas
{
    public class GH_InternetConnectionPanel : MonoBehaviour
    {
        public static GH_InternetConnectionPanel Instance;

        private void Awake()
        {
            if (!Instance)
                Instance = this;
        }

        [SerializeField]
        private GameObject InternetWarningpanel_WithOk_button;

        [SerializeField]
        private GameObject InternetWarningDelay;

        [SerializeField]
        private Button CheckInternetBtn;

        bool OneTime = false;
        bool isShowing = false;

        private void Start()
        {
            InternetWarningpanel_WithOk_button.gameObject.SetActive(false);
            InternetWarningDelay.gameObject.SetActive(false);
            CheckInternetBtn.onClick.AddListener(
                OK_Button_recheckingInternetConnectivirty_ButtonPressed
            );
        }

        IEnumerator checkInternetConnectionContiniously()
        {
            while (true)
            {
                if (!isShowing)
                {
                    IsInternetAvalible();
                }

                ACLogger.UserDebug("checkInternetConnectionContiniously");
                yield return new WaitForSeconds(10);
            }
        }

        [ContextMenu("CheckInternet")]
        public bool IsInternetAvalible()
        {
            newSetting settings = AppCentralSettings.LoadSetting();

            ACLogger.UserDebug(": UseFlightModePrompt requested");

            if (!settings.UseFlightModePrompt)
            {
                ACLogger.UserDebug(
                    ": UseFlightModePrompt option is disable in the Settings, so returning true always"
                );
                return true;
            }

            bool avalible = true;
            InternetWarningDelay.SetActive(false);

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                avalible = false;
                InternetWarningpanel_WithOk_button.SetActive(true);
                isShowing = true;
            }
            else
            {
                InternetWarningpanel_WithOk_button.SetActive(false);

                if (OneTime)
                {
                    if (GH_WarningManager.Instance != null)
                    {
                        GH_WarningManager.Instance.Prompt("Connected To Internet");
                    }

                    OneTime = false;
                }

                isShowing = false;
            }

            ACLogger.UserDebug(
                ": internetReachability=" + Application.internetReachability.ToString()
            );

            return avalible;
        }

        private void OK_Button_recheckingInternetConnectivirty_ButtonPressed()
        {
            InternetWarningpanel_WithOk_button.SetActive(false);
            InternetWarningDelay.SetActive(true);

            OneTime = true;

            Invoke("IsInternetAvalible", 1.5f);
        }

        bool oneTime2 = true;

        private void Update()
        {
            if (isShowing)
            {
                if (Application.internetReachability != NetworkReachability.NotReachable)
                {
                    IsInternetAvalible();
                }
            }
        }
    }
}

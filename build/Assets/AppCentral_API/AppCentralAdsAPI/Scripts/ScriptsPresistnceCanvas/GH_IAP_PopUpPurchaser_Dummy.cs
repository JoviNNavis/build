using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.Purchasing;

namespace PresistanceCanvas
{
    public class GH_IAP_PopUpPurchaser_Dummy : MonoBehaviour
    {
        public static GH_IAP_PopUpPurchaser_Dummy Instance;

        private void Awake()
        {
            if (!Instance)
                Instance = this;
        }

        public Button OkBtn,
            CancleBtn;

        public GameObject WarningpanelWithOkbtn;
        public Text WarningMessageTxt;

        Action onSuccessCAllback;
        Action onFailCallback;

        PurchaseEventArgs _purchaseEventArgs;

        private void Start()
        {
            WarningpanelWithOkbtn.SetActive(false);

            OkBtn.onClick.AddListener(Ok_button);
            CancleBtn.onClick.AddListener(Cancle_button);
        }

        public void ShowPooUpPurchaser(string Msg)
        {
            ACLogger.UserDebug("Would you like to subscribe to this IAP");
            WarningMessageTxt.text = Msg;
            WarningpanelWithOkbtn.SetActive(true);

            // _purchaseEventArgs = purchaseEventArgs;
            // onFailCallback = OnFailCallback;
            // onSuccessCAllback = OnSuccessCAllback;
        }

        private void Ok_button()
        {
            AppCentralInAppPurchaser.Instance.OnPurchaseSuccess(_purchaseEventArgs);
            WarningpanelWithOkbtn.SetActive(false);
        }

        private void Cancle_button()
        {
            AppCentralInAppPurchaser.Instance.OnPurchaseCancleOrFail(_purchaseEventArgs);
            WarningpanelWithOkbtn.SetActive(false);
        }
    }
}

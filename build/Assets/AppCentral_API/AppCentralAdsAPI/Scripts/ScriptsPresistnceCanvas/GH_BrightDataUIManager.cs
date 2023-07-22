using UnityEngine.UI;
using UnityEngine;
using AppCentralCore;

namespace PresistanceCanvas
{
    public class GH_BrightDataUIManager : MonoBehaviour
    {
        public static GH_BrightDataUIManager Instance;

        private void Awake()
        {
            if (!Instance)
                Instance = this;
        }

        public Button OkBtn,
            CancleButton;
        public GameObject WarningpanelWithOkbtn;
        public Text WarningMessageTxt;

        private void Start()
        {
            WarningpanelWithOkbtn.SetActive(false);

            OkBtn.onClick.AddListener(OkBtn_Clicked);
            CancleButton.onClick.AddListener(CancleButton_Clicked);
        }

        public void ShowWarningWithOkButton(string Msg)
        {
            ACLogger.UserDebug("ShowWarning_WithOk_Button");
            WarningMessageTxt.text = Msg;
            WarningpanelWithOkbtn.SetActive(true);
        }

        private void OkBtn_Clicked()
        {
            // AppCentalBrightDataController.set
        }

        private void CancleButton_Clicked()
        {
            // WarningpanelWithOkbtn.SetActive(false);
        }
    }
}

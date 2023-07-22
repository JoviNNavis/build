using UnityEngine.UI;
using UnityEngine;

namespace PresistanceCanvas
{
    public class GH_WarningManager : MonoBehaviour
    {
        public static GH_WarningManager Instance;

        private void Awake()
        {
            if (!Instance)
                Instance = this;
        }

        public Button OkBtn;

        public GameObject WarningpanelWithOkbtn;
        public Text WarningMessageTxt;

        public GameObject PromptMsg;
        public Text PromptMsgTxt;

        private void Start()
        {
            WarningpanelWithOkbtn.SetActive(false);
            PromptMsg.SetActive(false);

            OkBtn.onClick.AddListener(Ok_button);
        }

        public void ShowWarningWithOkButton(string Msg)
        {
            ACLogger.UserDebug("ShowWarning_WithOk_Button");
            WarningMessageTxt.text = Msg;
            WarningpanelWithOkbtn.SetActive(true);
        }

        public void Prompt(string Msg)
        {
            PromptMsgTxt.text = Msg;
            PromptMsg.SetActive(false);
            PromptMsg.SetActive(true);
        }

        private void Ok_button()
        {
            WarningpanelWithOkbtn.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

namespace PresistanceCanvas
{
    public class GH_RewardedAdPanel : MonoBehaviour
    {
        public static GH_RewardedAdPanel Instance;

        private void Awake()
        {
            if (!Instance)
                Instance = this;
        }

        public Button YesBtn,
            NoBtn;

        public GameObject DummyRewardedPanel;
        public GH_DummyRewardedUIAdPanel _UI_DummyRewardedAdPanel;

        private Action<bool> AdComp_Status;

        private void Start()
        {
            DummyRewardedPanel.SetActive(false);
            _UI_DummyRewardedAdPanel.gameObject.SetActive(false);

            YesBtn.onClick.AddListener(WatchedCompleteAd);
            NoBtn.onClick.AddListener(CancleAd);
        }

        public void ShowDummy_AdPanel(Action<bool> adStatus)
        {
            //AnalyticsMediator.Instance.SendDesignEvent("Reward Ad Open");
            AdComp_Status = adStatus;

#if UNITY_EDITOR

            DummyRewardedPanel.SetActive(true);

#endif

#if GAMESHIVES_ADS_API

            GH.GamesHiveAdsAPI.ShowRewardedAd(GH.AdsNetwork.AppLovin, AdComp_Status);

#endif
        }

        private void WatchedCompleteAd()
        {
            DummyRewardedPanel.SetActive(false);
            AdComp_Status(true);
            //AnalyticsMediator.Instance.SendDesignEvent("Reward Received");
        }

        private void CancleAd()
        {
            DummyRewardedPanel.SetActive(false);
            AdComp_Status(false);
            //AnalyticsMediator.Instance.SendDesignEvent("Reward Cancled");
        }
    }
}

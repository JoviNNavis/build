using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace PresistanceCanvas
{
    public class GH_DummyRewardedUIAdPanel : MonoBehaviour
    {

        public Text CountDownText;

        UnityAction<bool> R_ad_CallBack;

        public void ShowR_Ad(UnityAction<bool> callback)
        {

            gameObject.SetActive(true);

            R_ad_CallBack = callback;
            StartCoroutine(StartCountDown());
        }

        IEnumerator StartCountDown()
        {
            float T = 10;

            while (T > 0)
            {

                CountDownText.text = T.ToString();
                T--;

#if !UNITY_EDITOR
            yield return new WaitForSeconds(1);
#else

                yield return new WaitForSeconds(0.05f);
                //yield return null;
#endif

            }


            gameObject.SetActive(false);
            R_ad_CallBack(true);


        }


        public void CloseAd()
        {
            gameObject.SetActive(false);
            R_ad_CallBack(false);

        }

    }
}
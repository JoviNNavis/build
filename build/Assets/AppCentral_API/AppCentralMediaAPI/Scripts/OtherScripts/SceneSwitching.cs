using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.iOS;
using AppCentralCore;

namespace AppCentralAPI
{
    public class SceneSwitching : MonoBehaviour
    {
        private int NextSceneIndex = 1;
        private int currentSceneIndex;

        public Image LoadingBar;

        private bool WaitForAppCentral_API = true;
        private bool KeepWaiting = true;

        public float MaxFillLimit,
            LoadingFinishTime;

        private AsyncOperation async;

        private void OnEnable()
        {
            AppCentralUnityApi_Internal.OnLoadEvent += AppCentralApiInitializationCompleted;
        }

        private void Start()
        {
            StartSceneLoading();
        }

        private void StartSceneLoading()
        {
            KeepWaiting = true;
            StartCoroutine(loadingSceneAsync());
        }

        private void AppCentralApiInitializationCompleted()
        {
            WaitForAppCentral_API = false;
            //KeepWaiting = false;
        }

        IEnumerator loadingSceneAsync()
        {
            ACLogger.UserDebug("SceneSwitching Start");

            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            ACLogger.UserDebug("SceneSwitching WaitForAppCentral_API=" + WaitForAppCentral_API);

            float T = 0;
            LoadingBar.fillAmount = T;

            while (WaitForAppCentral_API)
            {
                if (T <= MaxFillLimit / 2)
                {
                    T += Time.deltaTime / LoadingFinishTime;
                    LoadingBar.fillAmount = T;
                }

                yield return null;
            }

            async = SceneManager.LoadSceneAsync(NextSceneIndex);
            async.allowSceneActivation = false;
            KeepWaiting = true;

            while (KeepWaiting)
            {
                if (T <= MaxFillLimit)
                {
                    T += Time.deltaTime / LoadingFinishTime;
                    LoadingBar.fillAmount = T;
                }
                else
                {
                    KeepWaiting = false;
                }

                yield return null;
            }

            LoadingBar.fillAmount = 1;

            yield return new WaitForSeconds(0.5f);

            async.allowSceneActivation = true;
        }

        private void OnDisable()
        {
            AppCentralUnityApi_Internal.OnLoadEvent -= AppCentralApiInitializationCompleted;
        }
    }
}

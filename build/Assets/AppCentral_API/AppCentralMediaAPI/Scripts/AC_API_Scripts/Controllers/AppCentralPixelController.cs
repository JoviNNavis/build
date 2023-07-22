using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.iOS;
using UnityEngine;
using System;

namespace AppCentralCore
{
    public class AppCentralPixelController : MonoBehaviour
    {
        private static AppCentralPixelController instance;

        public static AppCentralPixelController Instance
        {
            get => instance;
            private set => instance = value;
        }

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public void CallAdLogPixel(string adPixelType, string adType, string retries)
        {
            if (!String.IsNullOrEmpty(retries))
                SaveAppCentralPixel(
                    adPixelType,
                    new string[] { "adType", "retries" },
                    new string[] { adType, retries }
                );
            else
                SaveAppCentralPixel(
                    adPixelType,
                    new string[] { "adType" },
                    new string[] { adType }
                );
        }

        public void SaveAppCentralPixel(string pixel_name, string[] attributes, string[] values)
        {
            AppCentral_JSON_Controller.API_Variables aPI_Variables = AppCentralUnityApi_Internal
                .Instance
                .jsonController
                .aPI_Variables;

            StartCoroutine(
                SaveAppCentralPixelCoroutine(pixel_name, attributes, values, aPI_Variables)
            );
        }

        public void WaitandSendAdToEnd(string AdPixelType)
        {
            StartCoroutine(WaitForAdToEnd(AdPixelType));
        }

        private IEnumerator WaitForAdToEnd(string AdPixelType)
        {
            yield return new WaitForSeconds(10);
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                "10SecAfter",
                null
            );
            yield return new WaitForSeconds(10);
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                "20SecAfter",
                null
            );
        }

        private IEnumerator SaveAppCentralPixelCoroutine(
            string pixel_name,
            string[] attributes,
            string[] values,
            AppCentral_JSON_Controller.API_Variables aPI_Variables
        )
        {
            // Build apiUrl with variables
            String AppCentralPixelUrl =
                "https://7ogim9mqkh.execute-api.us-east-1.amazonaws.com/default/app_central_pixels_save";

            WWWForm form = new WWWForm();

            // Get idfa
            //idfa = Device.advertisingIdentifier.ToString();
            // Adding user info
            form.AddField("idfv", aPI_Variables.idfv);
            form.AddField("version", aPI_Variables.appVersion);
            form.AddField("package", aPI_Variables.package);
            form.AddField("idfa", aPI_Variables.idfa);
            form.AddField("device_model", aPI_Variables.deviceModel);
            form.AddField("generation", aPI_Variables.generation);

            // Add pixel name
            form.AddField("pixel_name", pixel_name);
            // Add pixel variables
            for (int i = 0; i < attributes.Length; i++)
            {
                form.AddField(attributes[i], values[i]);
            }
            // Send pixel to server
            using (UnityWebRequest uwr = UnityWebRequest.Post(AppCentralPixelUrl, form))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    ACLogger.UserDebug(": ContactAppCentralServer: " + uwr.error);
                    uwr.Dispose();
                }
                else
                {
                    ACLogger.UserDebug(": ContactAppCentralServer: Form upload complete!");
                    uwr.Dispose();
                }
            }
        }
    }
}

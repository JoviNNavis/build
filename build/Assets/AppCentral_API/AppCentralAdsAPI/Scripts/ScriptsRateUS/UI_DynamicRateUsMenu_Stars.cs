using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GH
{
    public class UI_DynamicRateUsMenu_Stars : MonoBehaviour
    {

        public List<GameObject> Stars_Gold;


        public void Initialize()
        {
            foreach (var item in Stars_Gold)
            {
                item.SetActive(false);
            }

        }

        public void Initialize(int startValue)
        {
            Initialize();

            for (int i = 0; i < startValue; i++)
            {
                Stars_Gold[i].SetActive(true);
            }
        }


        public void ChoseStars(int p)
        {

            Initialize();
            StartCoroutine(DelayStars(p));
        }


        IEnumerator DelayStars(int p)
        {

            UI_DynamicRateUsMenu.UserRating = p;

            for (int i = 0; i < p; i++)
            {
                Stars_Gold[i].SetActive(true);
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.25f);

            RateUsController.Instance.CurrentRateUsTheme.ReveivedPlayerRating();
        }


    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PresistanceCanvas
{
    public class GH_ButtonClickAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        //public bool UseAnimationOnInteraction = false;

        private Animator animator;

        public bool playButtonSound = false;

        private void OnEnable()
        {
            ChekcAnim();

            {

                if (animator != null)
                {
                    animator.enabled = true;
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ChekcAnim();
            {
                if (animator != null)
                {
                    animator.enabled = false;
                }

                transform.localScale = Vector3.one * 0.9f;
            }
        }

        public void OnClickDown()
        {
            ChekcAnim();

            if (animator != null)
            {
                animator.Play("OnClickDown");
            }

        }

        public void OnClickUp()
        {
            ChekcAnim();

            if (animator != null)
            {
                animator.Play("OnClickUp");
            }

            if (playButtonSound)
            {
                //MusicManager.Instance.PlayOneShot(MusicManager.SoundType.ButtonClicked);
            }

        }


        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            ChekcAnim();

            //if (UseAnimationOnInteraction)
            //{
            //    OnClickUp();
            //}
            //else
            {

                if (animator != null)
                {
                    animator.enabled = true;
                }

                transform.localScale = Vector3.one;

            }
        }

        public void ChekcAnim()
        {
            if (animator == null)
            {
                animator = transform.GetComponent<Animator>();
            }
        }
    }
}
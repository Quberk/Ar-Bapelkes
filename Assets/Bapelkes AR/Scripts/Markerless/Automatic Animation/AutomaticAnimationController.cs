using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Bapelkes.Markerless.TransparentObject;

namespace Bapelkes.Markerless.AutomaticAnimation
{
    public class AutomaticAnimationController : MonoBehaviour
    {
        private TMP_Text titleTxt;

        [SerializeField] private AnimationSequence[] animations;
        private int animationIndex = 0;

        private TransparentController transparentController;

        private bool justStarted = true;

        private void OnEnable()
        {
            titleTxt = GameObject.FindGameObjectWithTag("Title").GetComponent<TMP_Text>();
            transparentController = FindObjectOfType<TransparentController>();

            //Jangan memanggil OnEnable() saat pertama kali aktif
            if (justStarted)
            {
                justStarted = false;
                return;
            }

            DeactivateAllAnimation();

            animationIndex = 0;

            titleTxt.text = animations[animationIndex].titleTxt;
            animations[animationIndex].animationObject.SetActive(true);
            Animator chatAnimator = animations[animationIndex].animation;

            chatAnimator.SetTrigger("TriggerAnimation");

            //Transparent Controller
            transparentController.objectTarget = GameObject.Find("Center Point").transform;
            transparentController.StartTheController();
        }

        public void NextAnimation()
        {
            DeactivateAllAnimation();

            animationIndex++;

            titleTxt.text = animations[animationIndex].titleTxt;
            GameObject myObject = animations[animationIndex].animationObject;
            myObject.SetActive(true);
            Animator chatAnimator = animations[animationIndex].animation;

            chatAnimator.SetTrigger("TriggerAnimation");

            //Transparent Controller
            transparentController.objectTarget = GameObject.Find("Center Point").transform;
            transparentController.StartTheController();
        }

        private void DeactivateAllAnimation()
        {
            foreach(AnimationSequence animation in animations)
            {
                animation.animationObject.SetActive(false);
            }
        }
    }

    [System.Serializable]
    public class AnimationSequence {
        public string titleTxt;
        public GameObject animationObject;
        public Animator animation;
    }

}


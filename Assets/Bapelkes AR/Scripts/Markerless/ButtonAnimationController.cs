using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Bapelkes.Markerless.TransparentObject;

namespace Bapelkes
{
    public class ButtonAnimationController : MonoBehaviour
    {
        [SerializeField] private AnimationAndButton[] animationAndButtons;

        [SerializeField] private TMP_Text titleText;

        [Header("Buttons Sprite")]
        [SerializeField] private Sprite selectedSprite;
        [SerializeField] private Sprite unselectedSprite;

        private TransparentController transparentController;

        private void Start()
        {
            foreach (AnimationAndButton animationAndButton in animationAndButtons)//Menonaktifkan semua Button di awal
            {
                animationAndButton.objectButton.SetActive(false);
            }

            transparentController = FindObjectOfType<TransparentController>();
        }

        public void PlaneDetected()
        {
            foreach (AnimationAndButton animationAndButton in animationAndButtons)
            {
                //animationAndButton.objectButton.SetActive(true);
                animationAndButton.pickingReference();
            }

            //Menonaktifkan semua Gameobject kecuali Default Gameobject
            foreach (AnimationAndButton animationAndButton in animationAndButtons) 
            {
                animationAndButton.gameObject.SetActive(false);
                animationAndButton.objectButton.GetComponent<Image>().sprite = unselectedSprite;
            }

            //Transparent Controller
            transparentController.objectTarget = GameObject.Find("Center Point").transform;
            transparentController.StartTheController();
        }

        public void AnimationButtonPressed(GameObject button)
        {
            //Menonaktifkan Default Gameobject
            GameObject defaultGameobject = GameObject.FindGameObjectWithTag("DefaultGameobject");
            if (defaultGameobject != null)
                defaultGameobject.SetActive(false);

            foreach(AnimationAndButton animationAndButton in animationAndButtons)
            {
                animationAndButton.gameObject.SetActive(false);
                animationAndButton.objectButton.GetComponent<Image>().sprite = unselectedSprite;

                //Jika button tersebut yang ditekan
                if (animationAndButton.objectButton.name == button.name)
                {
                    //Inserting Title
                    titleText.text = animationAndButton.titleName;

                    animationAndButton.gameObject.SetActive(true);
                    animationAndButton.objectButton.GetComponent<Image>().sprite = selectedSprite;
                }
            }

            //Transparent Controller
            transparentController.objectTarget = GameObject.Find("Center Point").transform;
            transparentController.StartTheController();
        }

        public void ActivateAllChatButtons()
        {
            foreach (AnimationAndButton animationAndButton in animationAndButtons)
            {
                animationAndButton.objectButton.SetActive(true); //Activating all the chat Buttons
            }

            //Resetting the Title Text
            titleText.text = "";
        }
    }



    [System.Serializable]
    public class AnimationAndButton
    {
        public GameObject objectButton;
        public string objectName;
        public string titleName;
        [HideInInspector] public GameObject gameObject;

        public void pickingReference()
        {
            gameObject = GameObject.Find(objectName);
        }
    }
}


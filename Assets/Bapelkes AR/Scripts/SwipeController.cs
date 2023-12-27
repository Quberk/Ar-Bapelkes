using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bapelkes
{
    public class SwipeController : MonoBehaviour
    {
        [SerializeField] private Scrollbar scrollbar;
        float scrollPos = 0;
        float[] pos;
        float distance;

        [SerializeField] private Vector2 contentSelectedSize;
        [SerializeField] private Vector2 contentUnselectedSize;

        [Header("Swipe Buttons")]
        [SerializeField] private GameObject[] swipeButtons;
        [SerializeField] private Sprite selectedSwipeBtnSprite;
        [SerializeField] private Sprite unselectedSwipeBtnSprite;

        private bool swipeButtonTouched = false;
        private int swipeBtnNum;

        private bool swipeButtonClicked = false;

        // Start is called before the first frame update
        void Start()
        {
            AddListenerToSwipeButtons(); //Adding Listeners to all Swipe buttons

            pos = new float[9];
            distance = 1f / (pos.Length - 1);

            for (int i = 0; i < pos.Length; i++)
            {
                pos[i] = distance * i;
            }

            for (int i = 0; i < pos.Length; i++)
            {
                if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
                {
                    transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, contentSelectedSize, 0.1f);

                    for (int a = 0; a < pos.Length; a++)
                    {
                        if (a != i)
                            transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, contentUnselectedSize, 0.1f);
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            //If there is a touch then Cancel all
            if (Input.touchCount > 0 && !swipeButtonClicked)
            {
                scrollPos = scrollbar.value;
                swipeButtonTouched = false;
            }

            else if (swipeButtonTouched) //If User Touched the Swipe Btn then it goes to the intended Menu Num
            {
                scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[swipeBtnNum], 0.1f);
                scrollPos = scrollbar.value;
            }
            
            else
            {
                for (int i = 0; i < pos.Length; i++)
                {
                    if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
                    {
                        scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                    }
                }
            }


            for (int i = 0; i < pos.Length; i++)
            {
                if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
                {
                    transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, contentSelectedSize, 0.1f);

                    SwipeButtonsUpdate(i); //Update the Swipe Buttons Active Status

                    for (int a = 0; a < pos.Length; a++)
                    {
                        if (a != i)
                            transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, contentUnselectedSize, 0.1f);
                    }
                }
            }

            swipeButtonClicked = false; //Reset The Swipe Button Clicked Status
        }

        void SwipeButtonsUpdate(int selectedNum)
        {
            foreach(GameObject swipeButton in swipeButtons)
            {
                swipeButton.GetComponent<Image>().sprite = unselectedSwipeBtnSprite;
            }

            swipeButtons[selectedNum].GetComponent<Image>().sprite = selectedSwipeBtnSprite;
        }

        public void SwipeButtonTouched(int selectedNum)
        {
            swipeButtonTouched = true;
            swipeBtnNum = selectedNum;
        }

        private void AddListenerToSwipeButtons()
        {
            foreach(GameObject swippeBtn in swipeButtons)
            {
                swippeBtn.GetComponent<Button>().onClick.AddListener(SwipeButtonListener);
            }
        }

        private void SwipeButtonListener()
        {
            swipeButtonClicked = true;
        }
    }

}

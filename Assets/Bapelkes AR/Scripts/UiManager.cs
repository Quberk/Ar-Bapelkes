using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Bapelkes
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private string beforeScene;

        [Header("Put it if it's exist")]
        [SerializeField] private FixingLeanTouch fixingLeanTouch;
        [SerializeField] private Animator targetAnimator;

        private bool isLoading = false;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape) && isLoading == false)
                BackScene();
        }

        private void Start()
        {
            loadingScreen.SetActive(false);
        }

        public void MoveToScene(string sceneName)
        {
            //Coba - coba
            if (FindObjectOfType<PlaceOnPlane>() != null)
            {
                PlaceOnPlane placeOnPlane = FindObjectOfType<PlaceOnPlane>();
                placeOnPlane.placementUpdate.RemoveListener(placeOnPlane.DiableVisual);
            }

            if (fixingLeanTouch != null)
                fixingLeanTouch.FixTheLeanTouch();

            StartCoroutine(LoadSceneAsync(sceneName));
        }

        IEnumerator LoadSceneAsync(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

            loadingScreen.SetActive(true);

            while (!operation.isDone)
            {
                yield return null;

            }
        }

        public void ActivateGameobject(GameObject target)
        {
            target.SetActive(true);
        }

        public void DeactivateGameobject(GameObject target)
        {
            target.SetActive(false);
        }

        public void InvokeAnimation(string animationTrigger)
        {
            targetAnimator.SetTrigger(animationTrigger);
        }

        void BackScene()
        {
            isLoading = true;

            if (beforeScene == "")
                return;

            MoveToScene(beforeScene);
        }
    }
}


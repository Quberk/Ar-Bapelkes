using UnityEngine;
using UnityEngine.UI;

namespace Bapelkes
{
    public class BoardingController : MonoBehaviour
    {
        private int step = 1;

        [SerializeField] private int maxStep;

        [SerializeField] private GameObject container;

        [Header("Moving Panel Animation")]
        [SerializeField] private GameObject nextButtons;
        [SerializeField] private float movingSpeed;
        private Vector3 movingTarget;

        [Header("Step Panels")]
        [SerializeField] private Vector3 panelDistance;

        int lastStep;

        private void Update()
        {
            RectTransform containerRect = container.GetComponent<RectTransform>();
            containerRect.localPosition = Vector3.MoveTowards(containerRect.localPosition, movingTarget, movingSpeed * Time.deltaTime);


            if (step >= 3)
            {
                nextButtons.SetActive(false);
                return;
            }

            nextButtons.SetActive(true);
        }

        public void GoToStep(int step)
        {
            this.step = step;

            movingTarget = (step - 1) * panelDistance;
        }

        public void NextStep()
        {
            if ((step + 1) > maxStep)
                return;

            step++;

            movingTarget = (step - 1) * panelDistance;
        }
    }

}

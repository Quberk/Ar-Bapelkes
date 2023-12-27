using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bapelkes.Markerless.AutomaticAnimation
{
    public class AutomaticAnimationTrigger : MonoBehaviour
    {
        [SerializeField] private AutomaticAnimationController automaticAnimationController;

        public void NextAnimation()
        {
            automaticAnimationController.NextAnimation();
        }
    }
}


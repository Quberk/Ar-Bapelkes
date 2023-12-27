using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bapelkes.Markerless.TransparentObject
{

    public class BehindCameraObject : MonoBehaviour
    {
        [SerializeField] private GameObject transparentBody;
        [SerializeField] private GameObject solidBody;

        public void ShowTransparent()
        {
            transparentBody.SetActive(true);
            solidBody.SetActive(false);
        }

        public void ShowSolid()
        {
            transparentBody.SetActive(false);
            solidBody.SetActive(true);
        }

    }
}

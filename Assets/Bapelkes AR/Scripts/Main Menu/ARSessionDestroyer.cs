using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Bapelkes.PersiapanRuangDanAlat;
using UnityEngine.EventSystems;
using Lean.Touch;

namespace Bapelkes.MainMenu
{
    public class ARSessionDestroyer : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
            if (FindObjectOfType<ARSessionOrigin>() != null)
            {
                GameObject aRSessionOrigin = FindObjectOfType<ARSessionOrigin>().gameObject;
                Destroy(aRSessionOrigin);
            }

            if (FindObjectOfType<ARSession>() != null)
            {
                GameObject aRSession = FindObjectOfType<ARSession>().gameObject;
                Destroy(aRSession);
            }

            if (FindObjectOfType<MarkerlessObjectDefine>() != null)
            {
                GameObject markerlessObject = FindObjectOfType<MarkerlessObjectDefine>().gameObject;
                Destroy(markerlessObject);
            }

            if (FindObjectOfType<ContentParameterCatcher>() != null)
            {
                GameObject contentParameterCatcher = FindObjectOfType<ContentParameterCatcher>().gameObject;
                Destroy(contentParameterCatcher);
            }

            if (FindObjectOfType<LeanTouch>() != null)
            {
                GameObject leantouch = FindObjectOfType<LeanTouch>().gameObject;
                Destroy(leantouch);
            }

            StopAllCoroutines();

            

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}

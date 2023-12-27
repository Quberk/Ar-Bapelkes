using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Bapelkes
{
    namespace PersiapanRuangDanAlat
    {
        public class ContentParameterCatcher : MonoBehaviour
        {
            MarkerlessObjectDefine markerlessObjectDefine;

            [SerializeField] private PlaceOnPlane placeOnPlane;

            [Header("Populate Parameter")]
            [SerializeField] private GameObject parentObjectToDetect;
            private GameObject objectToDetect;

            [SerializeField] private TMP_Text objectNameText;
            [SerializeField] private TMP_Text infoTittleText;
            [SerializeField] private TMP_Text infoShadowText;
            [SerializeField] private TMP_Text infoContentText;

            // Start is called before the first frame update
            void Start()
            {
                


                //Find the MarkerlessObjectDefine
                markerlessObjectDefine = GameObject.FindObjectOfType<MarkerlessObjectDefine>();

                placeOnPlane.placedPrefab = markerlessObjectDefine.objectToDetectPrefab;

                /*
                //Instantiate Object to Scene
                GameObject myObject = Instantiate(objectToDetect, transform.position, Quaternion.identity);
                myObject.transform.SetParent(parentObjectToDetect.transform);
                myObject.transform.localPosition = Vector3.zero;
                myObject.transform.localScale = new Vector3(0.0100759f, 0.0100759f, 0.0100759f);
                */

                //Populate other Info
                objectNameText.text = markerlessObjectDefine.objectName;
                
                infoTittleText.text = markerlessObjectDefine.objectName;
                infoShadowText.text = markerlessObjectDefine.objectName;
                infoContentText.text = markerlessObjectDefine.infoContent;

                Destroy(markerlessObjectDefine.gameObject);
                Destroy(gameObject);
            }
        }
    }
}

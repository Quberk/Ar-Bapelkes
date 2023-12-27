using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Bapelkes
{
    namespace PersiapanRuangDanAlat {
        public class ContentDefenition : MonoBehaviour
        {
            private MarkerlessObjectDefine markerlessObjectDefine;

            [SerializeField] private UiManager uiManager;

            [SerializeField] private GameObject objectToDetectPrefab;
            [SerializeField] private string objectName;
            [SerializeField] private string infoContent;

            [SerializeField] private string targetScene;

            // Start is called before the first frame update
            void Start()
            {
                markerlessObjectDefine = FindObjectOfType<MarkerlessObjectDefine>();
            }

            public void TransferParameter()
            {
                uiManager.MoveToScene(targetScene);

                ///-------------------POPULATE THE MARKERLESS OBJECT DEFINE-------------------
                ///
                markerlessObjectDefine.objectToDetectPrefab = objectToDetectPrefab;
                markerlessObjectDefine.objectName = objectName;
                markerlessObjectDefine.infoContent = infoContent;
            }

        }
    }


}

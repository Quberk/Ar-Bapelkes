using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bapelkes
{
    namespace PersiapanRuangDanAlat
    {
        public class MarkerlessObjectDefine : MonoBehaviour
        {
            [Header("Populate Parameter")]
            [HideInInspector] public GameObject objectToDetectPrefab;
            [HideInInspector] public string objectName;
            [HideInInspector] public string infoContent;

            // Start is called before the first frame update
            void Start()
            {
                if (SceneManager.GetActiveScene().name == "MainMenuScene")
                    Destroy(gameObject);

                else
                    DontDestroyOnLoad(gameObject);
            }
        }
    }
}

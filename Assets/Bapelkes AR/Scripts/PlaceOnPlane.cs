using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Lean.Touch;
using Bapelkes.Markerless.TransparentObject;
using UnityEngine.EventSystems;


/// <summary>
/// Listens for touch events and performs an AR raycast from the screen touch point.
/// AR raycasts will only hit detected trackables like feature points and planes.
///
/// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
/// and moved to the hit position.
/// </summary>

namespace Bapelkes
{
    [RequireComponent(typeof(ARRaycastManager))]

    public class PlaceOnPlane : MonoBehaviour
    {
        [Header("AR Plane Component")]
        [SerializeField] private ARSessionOrigin arSessionOrigin;
        [SerializeField] private ARPlaneManager arPlaneManager;
        [SerializeField] private ARPointCloudManager arPointCloudManager;
        [SerializeField] private ARPointCloud arPointCloud;
        [SerializeField] private ARRaycastManager arRaycastManager;
        [SerializeField] private ARAnchorManager aRAnchorManager;

        [Header("UI")]
        [SerializeField] private GameObject lockPositionBtn;
        [SerializeField] private GameObject infoBtn;
        [SerializeField] private GameObject infoPanel;
        [SerializeField] private GameObject objectNameTxt;

        [SerializeField]
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]
        GameObject m_PlacedPrefab;

        [HideInInspector] public UnityEvent placementUpdate;

        [Header("Markerless")]

        [SerializeField]
        GameObject visualObject;

        [SerializeField] private Vector3 objectSize;

        [SerializeField] private ButtonAnimationController buttonAnimationController;

        private bool positionUpdate = true;

        [SerializeField] private TransparentController transparentController;

        /// <summary>
        /// The prefab to instantiate on touch.
        /// </summary>
        public GameObject placedPrefab
        {
            get { return m_PlacedPrefab; }
            set { m_PlacedPrefab = value; }
        }

        /// <summary>
        /// The object instantiated as a result of a successful raycast intersection with a plane.
        /// </summary>
        public GameObject spawnedObject { get; private set; }

        void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();

            if (placementUpdate == null)
                placementUpdate = new UnityEvent();

            placementUpdate.AddListener(DiableVisual);


            //Deactivate UIs
            if (infoBtn != null)
                infoBtn.SetActive(false);
            if (infoPanel != null)
                infoPanel.SetActive(false);
            if (objectNameTxt != null)
                objectNameTxt.SetActive(false);
            lockPositionBtn.SetActive(false);
        }

        bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }

            touchPosition = default;
            return false;
        }

        void Update()
        {
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))// && !EventSystem.current.IsPointerOverGameObject())
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;

                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                    spawnedObject.transform.localScale = objectSize;

                    //Activate UIs
                    if (infoBtn != null)
                        infoBtn.SetActive(true);
                    if (infoPanel != null)
                        infoPanel.SetActive(true);
                    if (objectNameTxt != null)
                        objectNameTxt.SetActive(true);
                    lockPositionBtn.SetActive(true);

                    //Transparent Controller
                    if (transparentController != null)
                    {
                        transparentController.objectTarget = GameObject.Find("Center Point").transform;
                        transparentController.StartTheController();

                    }

                    PositionUpdate();

                    if (buttonAnimationController != null)
                    {
                        buttonAnimationController.PlaneDetected();
                    }
                }
                else if (positionUpdate == true)
                {
                    //repositioning of the object 
                    spawnedObject.transform.position = hitPose.position;
                }


                placementUpdate.Invoke();
            }
        }

        public void DiableVisual()
        {
            visualObject.SetActive(false);
        }

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;

        public void PositionUpdate()
        {
            positionUpdate = true;

            //Deactivating Lean Scale and Lean Rotate
            LeanPinchScale leanPinchScale = FindObjectOfType<LeanPinchScale>();
            LeanTwistRotateAxis leanTwistRotateAxis = FindObjectOfType<LeanTwistRotateAxis>();

            leanPinchScale.enabled = false;
            leanTwistRotateAxis.enabled = false;

            //Activating AR Plane Component
            arSessionOrigin.enabled = true;
            arPlaneManager.enabled = true;
            arPointCloudManager.enabled = true;
            arPointCloud.enabled = true;
            arRaycastManager.enabled = true;
            aRAnchorManager.enabled = true;

        }

        public void LockPosition()
        {
            positionUpdate = false;

            //Activating Lean Scale and Lean Rotate
            LeanPinchScale leanPinchScale = FindObjectOfType<LeanPinchScale>();
            LeanTwistRotateAxis leanTwistRotateAxis = FindObjectOfType<LeanTwistRotateAxis>();

            leanPinchScale.enabled = true;
            leanTwistRotateAxis.enabled = true;

            //Activating AR Plane Component
            arSessionOrigin.enabled = false;
            arPlaneManager.enabled = false;
            arPointCloudManager.enabled = false;
            arPointCloud.enabled = false;
            arRaycastManager.enabled = false;
            aRAnchorManager.enabled = false;

            GameObject[] arPlaneFxs = GameObject.FindGameObjectsWithTag("ARPlaneFx");

            foreach(GameObject arPlaneFx in arPlaneFxs)
            {
                Destroy(arPlaneFx);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bapelkes.Markerless.TransparentObject
{
    public class TransparentController : MonoBehaviour
    {
        [SerializeField] private List<BehindCameraObject> currentlyBehindTheCamera;
        [SerializeField] private List<BehindCameraObject> alreadyTransparent;
        public Transform objectTarget;
        private Transform camera;

        private bool startToFunction;

        private void Awake()
        {
            currentlyBehindTheCamera = new List<BehindCameraObject>();
            alreadyTransparent = new List<BehindCameraObject>();

            camera = this.gameObject.transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (!startToFunction)
                return;


            GetAllObjectsInFrontOfCamera();

            MakeObjectsSolid();
            MakeObjectsTransparent();
        }

        public void StartTheController()
        {
            startToFunction = true;
        }

        private void GetAllObjectsInFrontOfCamera()
        {
            currentlyBehindTheCamera.Clear();

            float cameraObjectDistance = Vector3.Magnitude(camera.position - objectTarget.position);

            Ray ray1_forward = new Ray(camera.position, objectTarget.position - camera.position);

            var hits1_forward = Physics.RaycastAll(ray1_forward, cameraObjectDistance);

            foreach(var hit in hits1_forward)
            {
                if (hit.collider.gameObject.TryGetComponent(out BehindCameraObject behindCameraObject))
                {
                    if (!currentlyBehindTheCamera.Contains(behindCameraObject))
                    {
                        currentlyBehindTheCamera.Add(behindCameraObject);
                    }
                }
            }

        }

        private void MakeObjectsTransparent()
        {
            for (int i = 0; i < currentlyBehindTheCamera.Count; i++)
            {
                BehindCameraObject behindCameraObject = currentlyBehindTheCamera[i];

                if (!alreadyTransparent.Contains(behindCameraObject))
                {
                    behindCameraObject.ShowTransparent();
                    alreadyTransparent.Add(behindCameraObject);
                }
            }
        }

        private void MakeObjectsSolid()
        {
            for(int i = alreadyTransparent.Count - 1; i >= 0; i--)
            {
                BehindCameraObject wasBehindCameraObject = alreadyTransparent[i];

                if (!currentlyBehindTheCamera.Contains(wasBehindCameraObject))
                {
                    wasBehindCameraObject.ShowSolid();
                    alreadyTransparent.Remove(wasBehindCameraObject);
                }
            }
        }
    }
}

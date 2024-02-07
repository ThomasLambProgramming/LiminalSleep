using UnityEngine;
using CoreFunctions;
using Cinemachine;

namespace Player
{
    public class CameraZoom : MonoBehaviour
    {
        //--------------------- Zoom Variables ---------------------------------//
        [SerializeField] private CinemachineVirtualCamera _playerCamera = null;
        [SerializeField] private float _zoomInSpeed = 2f;
        [SerializeField] private float _zoomOutSpeed = 5f;
        [SerializeField] private float _zoomAmount = 20f;
        private float _zoomInitial = 0;
        private float _zoomTimer = 0;
        private bool _zoomIn = false;
        private bool _zoomFinished = true;
        //----------------------------------------------------------------------//

        void Start()
        {
            _zoomInitial = _playerCamera.m_Lens.FieldOfView;
        }

        //We use a bool here to determine if the zoom is done for the player manager
        public void Tick()
        {
            FunctionLibrary.PerformLerp(ref _zoomTimer, out _zoomFinished, _zoomIn, _zoomInSpeed, _zoomOutSpeed);

            //So far just changing the field of view seems to do a good enough job with zooming, Possibly adding additional actions when zoomed in might be worth it.
            _playerCamera.m_Lens.FieldOfView = Mathf.Lerp(_zoomInitial, _zoomAmount, _zoomTimer);
        }

        public bool IsZoomed()
        {
            if (_zoomFinished == false || _zoomIn)
                return true;

            return false;
        }

        //Public Turning on and off of zoom (Separating scripts)
        public void PerformZoomIn()
        {
            _zoomIn = true;
            _zoomFinished = false;
        }
        public void PerformZoomOut()
        {
            _zoomIn = false;
            _zoomFinished = false;
        }
    }
}

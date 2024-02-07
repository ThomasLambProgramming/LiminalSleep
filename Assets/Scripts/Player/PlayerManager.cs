using CoreFunctions;
using UnityEngine;
using UnityEngine.InputSystem;
using Interactables;
using EasyCharacterMovement;

namespace Player
{
    //Safety to have all required scripts for this component
    [RequireComponent(typeof(InteractManage), typeof(CameraZoom), typeof(CharacterLook))]
    public class PlayerManager : MonoBehaviour
    {
        private InteractManage _interactManage = null;
        private CameraZoom _cameraZoom = null;

        private void Start()
        {
            _cameraZoom = GetComponent<CameraZoom>();
            _interactManage = GetComponent<InteractManage>();
            
            EnableInput();
        }


        private void Update()
        {
            if (_cameraZoom.IsZoomed())
                _cameraZoom.Tick();
            else
            {
                //We dont want to be able to interact with objects while zooming as it might cause issues
                _interactManage.Tick();
            }
        }

        #region Input


        private void EnableInput()
        {
            GlobalInput.InputInstance._interact.performed += StartInteractKey;
            GlobalInput.InputInstance._interact.canceled += EndInteractKey;
            
            GlobalInput.InputInstance._leftMouse.performed += StartLeftClick;
            GlobalInput.InputInstance._leftMouse.canceled += EndLeftClick;
            
            GlobalInput.InputInstance._rightMouse.performed += StartRightClick;
            GlobalInput.InputInstance._rightMouse.canceled += EndRightClick;
        }
        private void DisableInput()
        {
            GlobalInput.InputInstance._interact.performed -= StartInteractKey;
            GlobalInput.InputInstance._interact.canceled -= EndInteractKey;
            
            GlobalInput.InputInstance._leftMouse.performed -= StartLeftClick;
            GlobalInput.InputInstance._leftMouse.canceled -= EndLeftClick;
            
            GlobalInput.InputInstance._rightMouse.performed -= StartRightClick;
            GlobalInput.InputInstance._rightMouse.canceled -= EndRightClick;
        }

        private void StartRightClick(InputAction.CallbackContext a_context)
        {
            if (_interactManage.HoldingObject())
            {
                _interactManage.StartRightClickInput();
            }
            else
                _cameraZoom.PerformZoomIn();
        }
        private void EndRightClick(InputAction.CallbackContext a_context)
        {
            if (_cameraZoom.IsZoomed())
                _cameraZoom.PerformZoomOut();
            
            else if (_interactManage.HoldingObject())
            {
                _interactManage.EndRightClickInput();
            }
        }
        private void StartLeftClick(InputAction.CallbackContext a_context)
        {
            _interactManage.StartLeftClick();
        }
        private void EndLeftClick(InputAction.CallbackContext a_context)
        {
            _interactManage.EndLeftClick();
        }
        private void StartInteractKey(InputAction.CallbackContext a_context)
        {
            if (!_cameraZoom.IsZoomed())
                _interactManage.StartInteractInput();
        }
        private void EndInteractKey(InputAction.CallbackContext a_context)
        {
            if (!_cameraZoom.IsZoomed())
                _interactManage.EndInteractInput();
        }

        #endregion
    }
}
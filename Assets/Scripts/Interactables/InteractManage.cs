using UnityEngine;
using CoreFunctions;

namespace Interactables
{
    public class InteractManage : MonoBehaviour
    {
        //-------------------- Interactable Variables --------------------------//
        public float _throwForce = 50f;
        public float _nonCarryThrowForce = 10000f;
        public float _interactRange = 500f;
        public Transform _interactHoldLocation = null;
        private bool _holdingInteractable = false;
        private IInteractable _currentInteractable = null;
        private Transform _currentInteractableTransform = null;
        private Outline _currentInteractableOutline = null;
        //----------------------------------------------------------------------//

        
        //--------------------- Inspect Variables ------------------------------//
        public Transform _inspectHoldLocation = null;
        public float _rotateSpeed = 4f;
        public float _inspectSpeedIn = 3f;
        public float _inspectSpeedOut = 3f;
        private float _inspectTimer = 0;
        private bool _inspectIn = true;
        private bool _moving = false;
        //Function to only return if we are holding an interactable for the player manager.
        public bool HoldingObject() => _holdingInteractable;
        public bool _inspecting = false;
        
        //----------------------------------------------------------------------//


        /// <summary>
        /// ONLY USED BY CUSTOM EDITOR DO NOT USE OTHERWISE!!!!!!!!!!!
        /// </summary>
        public void SetInteractableVariables(
            float a_throwForce, float a_nonCarryForce, float a_interactRange, Transform a_interactLocation)
        {
            _throwForce = a_throwForce;
            _nonCarryThrowForce = a_nonCarryForce;
            _interactRange = a_interactRange;
            _interactHoldLocation = a_interactLocation;
        }
        /// <summary>
        /// ONLY USED BY CUSTOM EDITOR DO NOT USE OTHERWISE!!!!!!!!!!!
        /// </summary>
        public void SetInspectVariables(
            float a_rotateSpeed, float a_inspectSpeedIn, float a_inspectSpeedOut, Transform a_inspectLocation)
        {
            _rotateSpeed = a_rotateSpeed;
            _inspectSpeedIn = a_inspectSpeedIn;
            _inspectSpeedOut = a_inspectSpeedOut;
            _interactHoldLocation = a_inspectLocation;
        }

        //This is to give control to player manager
        public void Init()
        {
                            
        }

        //This is to give update loop control to the player manager
        public void Tick()
        {
            if (_inspecting)
            {
                if (_moving)
                {
                    if (MoveToInspectLocation())
                    {
                        _moving = false;
                        //If we have lifted right click then we want to not inspect anymore.
                        if (!_inspectIn)
                            _inspecting = false;
                    }
                }
                else
                {
                    //Movement Input for rotating the inspected object.
                    Vector2 mouseInput = GlobalInput.InputInstance._mouse.ReadValue<Vector2>();
                    _currentInteractableTransform.eulerAngles += new Vector3(-mouseInput.y * _rotateSpeed, -mouseInput.x * _rotateSpeed);
                }
            }
            else if (!_holdingInteractable)
                ScanForInteractable();
        }

        private void ScanForInteractable()
        {
            //Get raycast information and direction from player camera position
            RaycastHit hit;
            Transform tempTransform = Camera.main.transform;
            Ray ray = new Ray(tempTransform.position, tempTransform.forward);
            
            bool newInteractable = false;
            //If the raycast hits an interactable tag then we know that a new interactable will be enabled
            //not using layermasks otherwise it will search through walls (would break game)
            if (Physics.Raycast(ray, out hit, _interactRange, ~0, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.CompareTag("Interactable"))
                {
                    newInteractable = true;
                }
            }

            if (newInteractable)
            {
                if (_currentInteractable != null)
                    RemoveInteractable();
                
                IInteractable interactable = hit.transform.GetComponent<IInteractable>();
                _currentInteractable = interactable;
                
                _currentInteractableTransform = hit.transform;
                _currentInteractableOutline = _currentInteractableTransform.GetComponent<Outline>();
                _currentInteractableOutline.FadeIn();
            }
            else if (_currentInteractable != null)
                RemoveInteractable();
            
            //I hate the amount of null checks here but they dont run often every update with the scan so its not the worst.
            //Tried 4 different versions but this seems to be the simplest with the least null checks that i could think of, 
            //ps. Alot of these comments are just for me to laugh about when the project is done
        }

        
        //This is a catch all function for if the player needs to get a object removed during a cutscene
        //and also to make sure no variable is left. Technically this is unperformant when swapping variables
        //but I believe having this here makes the code cleaner and will have less bugs.
        public void RemoveInteractable()
        {
            _currentInteractable = null;
            _currentInteractableOutline.FadeOut();
            _currentInteractableOutline = null;

            if (_currentInteractableTransform.parent == _inspectHoldLocation)
                _currentInteractableTransform.parent = null;
            
            _currentInteractableTransform = null;
        }

        private bool MoveToInspectLocation()
        {
            FunctionLibrary.PerformLerp(ref _inspectTimer, out bool finished, _inspectIn, _inspectSpeedIn, _inspectSpeedOut);
            _currentInteractableTransform.position = Vector3.Lerp(
                _interactHoldLocation.position, 
                _inspectHoldLocation.position, 
                _inspectTimer);
            _currentInteractableTransform.rotation = Quaternion.Lerp(_currentInteractableTransform.rotation, _inspectHoldLocation.rotation, _inspectTimer);
            
            return finished;
        }

        private void PickupInteractable()
        {
            _holdingInteractable = true;
           
            _currentInteractableTransform.position = _interactHoldLocation.position;
            _currentInteractableTransform.rotation = _interactHoldLocation.rotation;
            _currentInteractableTransform.parent = _interactHoldLocation;

            _currentInteractableTransform.GetComponent<Collider>().enabled = false;
            _currentInteractableTransform.GetComponent<Rigidbody>().isKinematic = true;
        }
        private void ReleaseInteractable(float a_force)
        {
            _currentInteractableTransform.parent = null;
            
            Collider interactCollider = _currentInteractableTransform.GetComponent<Collider>();
            interactCollider.enabled = true;
            
            Rigidbody interactableRb = _currentInteractableTransform.GetComponent<Rigidbody>();
            interactableRb.isKinematic = false;
            
            //Using a_force here to allow for the drop that has no force, and also for a charged throw and etc with only one function
            interactableRb.AddForce(_interactHoldLocation.forward * a_force);
            _holdingInteractable = false;

            RemoveInteractable();
        }
        private void PushInteractable()
        {
            _currentInteractableTransform.GetComponent<Rigidbody>().AddForce(_interactHoldLocation.forward * _nonCarryThrowForce);
        }
        //Input managed by player manager but naming it what the keys actually are so its clear
        public void StartRightClickInput()
        {
            if (_holdingInteractable)
            {
                _moving = true;
                _inspecting = true;
                _inspectIn = true;
            }
        }
        public void EndRightClickInput()
        {
            if (_inspecting)
            {
                _moving = true;
                _inspectIn = false;
            }
        }
        public void StartInteractInput()
        {
            if (_holdingInteractable)
            {
                //Release the current interactable without throwing it.
                ReleaseInteractable(0);
                if (_inspecting)
                {
                    _inspectIn = true;
                    _inspectTimer = 0;
                    _inspecting = false;
                }
            }
            else if (_currentInteractable != null)
            {
                switch (_currentInteractable.InteractType())
                {
                    case InteractableType.Pickup:
                        PickupInteractable();
                        break;
                    case InteractableType.Door:
                        //Perform door action and give player direction to determine what way the door should open
                        _currentInteractable.PerformAction(transform.forward);
                        break;
                    case InteractableType.Button:
                        _currentInteractable.PerformAction();
                        break;
                }
                
            }
        }
        public void EndInteractInput()
        {
            
        }

        public void StartLeftClick()
        {
            if (_currentInteractable == null)
                return;

            switch (_currentInteractable.InteractType())
            {
                case InteractableType.Pickup:
                    if (_holdingInteractable)
                    {
                        if (_inspecting)
                        {
                            _inspectIn = true;
                            _inspectTimer = 0;
                            _inspecting = false;
                        }
                        ReleaseInteractable(_throwForce);
                    }
                    else if (_currentInteractable != null)
                        PushInteractable();
                    break;
                case InteractableType.Door:
                    _currentInteractable.PerformAction(transform.forward);
                    break;
                case InteractableType.Button:
                    break;
            }
            
        }

        public void EndLeftClick()
        {
            
        }

#if UNITY_EDITOR
        public int _editorSelection = 0;
#endif
    }
}

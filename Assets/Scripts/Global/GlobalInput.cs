using UnityEngine;
using UnityEngine.InputSystem;


namespace CoreFunctions
{
    public class GlobalInput : MonoBehaviour
    {
        //Singleton to have a global input option rather than all scripts needing access or reference to input action
        public static GlobalInput InputInstance = null;
        public InputActionAsset _inputAction = null;

        //Hide in inspector as we dont want to add anything to them that the inspector allows
        [HideInInspector]
        public InputAction _leftMouse = null;
        [HideInInspector]
        public InputAction _rightMouse = null;
        [HideInInspector]
        public InputAction _interact = null;
        [HideInInspector]
        public InputAction _mouse = null;


        void Awake()
        {
            //Avoid double ups
            if (InputInstance != null)
                Debug.LogError("More than one global input. Remove the second one or there will be input errors");

            //get input actions and enable them all for use
            InputInstance = this;
            _interact = _inputAction.FindAction("Interact");
            _leftMouse = _inputAction.FindAction("Left Mouse");
            _rightMouse = _inputAction.FindAction("Right Mouse");
            _mouse = _inputAction.FindAction("Mouse Look");

            EnableInput();
        }

        //Mass enable and disable to be used for cinematics or etc
        //(DO NOT USE THIS FOR SIMPLE THINGS USE THE SCRIPT THAT USES THAT INPUT)
        public static void EnableInput()
        {
            InputInstance._leftMouse.Enable();
            InputInstance._rightMouse.Enable();
            InputInstance._interact.Enable();
            InputInstance._mouse.Enable();
        }
        public static void DisableInput()
        {
            InputInstance._leftMouse.Disable();
            InputInstance._rightMouse.Disable();
            InputInstance._interact.Disable();
            InputInstance._mouse.Disable();
        }
    }
}

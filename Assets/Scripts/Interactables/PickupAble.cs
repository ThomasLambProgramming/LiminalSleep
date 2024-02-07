using UnityEngine;

namespace Interactables
{
    public class PickupAble : MonoBehaviour, IInteractable
    {
        [SerializeField] InteractableType _interactableType = InteractableType.Pickup;
        public InteractableType InteractType() => _interactableType;
        public void PerformAction()
        {

        }
        //Void function to fulfill Interface
        public void PerformAction(Vector3 a_direction) { }
    }
}

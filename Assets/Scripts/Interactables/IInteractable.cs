using UnityEngine;

namespace Interactables
{
    public enum InteractableType
    {
        Pickup = 1,
        Door = 2,
        Button = 3,
    } 

    
    public interface IInteractable
    {
        public InteractableType InteractType();
        public void PerformAction();
        public void PerformAction(Vector3 a_direction);
    }
}

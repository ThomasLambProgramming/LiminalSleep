using UnityEngine;
using UnityEngine.Events;

namespace Interactables
{
    public class Button : MonoBehaviour, IInteractable
    {
        [SerializeField] private UnityEvent _interactEvent = null;
        [SerializeField] private InteractableType _interactableType = InteractableType.Button;
        public InteractableType InteractType() => _interactableType;


        public void PerformAction()
        {
            _interactEvent?.Invoke();
        }
        //Void function To fill requirement for interface
        public void PerformAction(Vector3 a_direction) { }      
    }
}

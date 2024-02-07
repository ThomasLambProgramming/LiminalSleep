using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(Outline), typeof(Rigidbody), typeof(HingeJoint))]
    public class Door : MonoBehaviour, IInteractable
    {
        private InteractableType _interactableType = InteractableType.Door;
        private Rigidbody _rb = null;
        [SerializeField] private float _openForce;
        [SerializeField] private float _minAngle = -90f;
        [SerializeField] private float _maxAngle = 90f;
        public InteractableType InteractType() => _interactableType;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();

            JointLimits limit = GetComponent<HingeJoint>().limits;
            limit.min = _minAngle;
            limit.max = _maxAngle;
            GetComponent<HingeJoint>().limits = limit; 
        }
        public void Update()
        {
            Vector3 currentEular = transform.rotation.eulerAngles;
            
                Debug.Log(currentEular.y);
            if (currentEular.y > _maxAngle || currentEular.y < _minAngle)
            {
                currentEular.y = Mathf.Clamp(currentEular.y, _minAngle, _maxAngle);
                transform.rotation = Quaternion.Euler(currentEular);
                _rb.angularVelocity = Vector3.zero;
            }
        }
        public void PerformAction()
        {
            Debug.Log("Open Door Filler");
        }
        //Void function to fulfill Interface
        public void PerformAction(Vector3 a_direction) 
        { 
            _rb.AddForce(a_direction * _openForce);
        }
    }
}

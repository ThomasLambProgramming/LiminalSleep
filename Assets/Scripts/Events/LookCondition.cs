using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using EasyCharacterMovement;

namespace ObjectEvents
{
    [AddComponentMenu("ItsNotReal/Conditions/LookCondition")]
    public class LookCondition : MonoBehaviour, IEventCondition
    {
        private static Transform _playerTransform = null;

        private Vector3 _lookDirection = Vector3.zero;
        [SerializeField] private List<Vector3> _lookDirections = new List<Vector3>();

        [Range(0, 180)]
        [SerializeField] private float _angleAllowed = 40f;
        [SerializeField] private bool _requiresLookAway = false;
        private bool _waitForLookAway = false;

        [SerializeField] private bool _debugEnabled = true;
        [Header("Index = what direction you want to view for debugging (so it doesnt clutter the object with lines)")]
        [SerializeField] private int _indexOfDirection = 0;

        void Start()
        {
            _playerTransform = FindObjectOfType<CharacterMovement>().transform;

            //Normalise all look directions
            for (int i = 0; i < _lookDirections.Count; i++)
            {
                _lookDirections[i] = _lookDirections[i].normalized;
            }

            _lookDirection = _lookDirections[0];
        }

        public void ChangeDirection(int index)
        {
            _lookDirection = _lookDirections[index];
            _indexOfDirection = index;
        }
        public bool CheckCondition()
        {
            bool isTrue = Mathf.Acos(Vector3.Dot(_playerTransform.forward, _lookDirection)) < Mathf.Deg2Rad * _angleAllowed;

            if (!_requiresLookAway)
                return isTrue;

            if (isTrue && _requiresLookAway && !_waitForLookAway)
            {
                _waitForLookAway = true;
                return true;
            }

            //is the player looking in the opposite direction to the look direction
            if (_waitForLookAway)
            {
                if (Mathf.Acos(Vector3.Dot(_playerTransform.forward, -_lookDirection)) < Mathf.Deg2Rad * _angleAllowed)
                {
                    _waitForLookAway = false;
                }
            }

            return false;
        }

        public void ChangeAngle(float a_angle) => _angleAllowed = a_angle;
        private void OnDrawGizmos()
        {
            if (!_debugEnabled)
                return;

            Gizmos.DrawLine(transform.position, transform.position + _lookDirections[_indexOfDirection].normalized * 2);

            //Cone of vision from the looking direction
            Vector3 direction1 = Quaternion.Euler(0, _angleAllowed, 0) * _lookDirections[_indexOfDirection].normalized;
            Vector3 direction2 = Quaternion.Euler(0, -_angleAllowed, 0) * _lookDirections[_indexOfDirection].normalized;

            Gizmos.DrawLine(transform.position, transform.position + direction1 * 5);
            Gizmos.DrawLine(transform.position, transform.position + direction2 * 5);
        }
    }
}

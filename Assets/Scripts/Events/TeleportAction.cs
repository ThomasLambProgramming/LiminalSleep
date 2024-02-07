using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectEvents
{
    [AddComponentMenu("ItsNotReal/Actions/Teleport Action")]
    public class TeleportAction : MonoBehaviour, IEventAction
    {
        [SerializeField] private List<Vector3> _newLocation = new List<Vector3>();
        [SerializeField] private bool _debugEnabled = true;

        [SerializeField] private int index = 0;
        public void PerformAction()
        {
            transform.position = _newLocation[index];
        }
        public void SetIndex(int a_index) => index = a_index;
        private void OnDrawGizmos()
        {
            if (_debugEnabled)
                Gizmos.DrawCube(_newLocation[index], Vector3.one);
        }
    }
}

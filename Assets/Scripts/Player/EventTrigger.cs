using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class EventTrigger : MonoBehaviour
    {
        public UnityEvent m_EventToTrigger;
        private void OnTriggerEnter(Collider other)
        {
            m_EventToTrigger?.Invoke();
        }
    }
}
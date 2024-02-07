using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace ObjectEvents
{
    [AddComponentMenu("ItsNotReal/Object/EventObject")]
    public class EventObject : MonoBehaviour
    {
        [SerializeField] private EventInfo _event;


        private IEventCondition _eventCondition;

        private void Start()
        {
            _eventCondition = GetComponent<IEventCondition>();
            _event._eventCondition = _eventCondition;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                Activate();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                Deactivate();
        }

        public void Activate()
        {
            _event._active = true;
            ActionManager.Manager.AddToEvents(_event);
        }
        public void Deactivate()
        {
            _event._active = false;
            ActionManager.Manager.RemoveFromEvents(_event);
        }
    }
}

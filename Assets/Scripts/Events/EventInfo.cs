using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectEvents
{
    [Serializable]
    public class EventInfo
    {
        public EventActionOption _actionOption;
        public List<UnityEvent> _events;
        public IEventCondition _eventCondition;

        [HideInInspector]
        public int _index;
        [HideInInspector]
        public bool _active;
        [HideInInspector]
        public bool _needToRemove;

        public void Increment()
        {
            if (_index < _events.Count - 1)
                _index++;
            else if (_actionOption == EventActionOption.FireOnce)
                _needToRemove = true;
            else if (_actionOption == EventActionOption.Loop)
                _index = 0;
        }

        public bool NeedToRemove() => _needToRemove;
    }

    public enum EventActionOption
    {
        FireOnce = 0,
        Loop = 1,
        Random,
        RandomMultiple
    }
}
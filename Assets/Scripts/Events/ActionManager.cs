using System.Collections.Generic;
using EasyCharacterMovement;
using UnityEngine;

namespace ObjectEvents
{
    [AddComponentMenu("ItsNotReal/Manager/ActionManager")]
    public class ActionManager : MonoBehaviour
    {
        //Singleton manager to make this easy.
        public static ActionManager Manager = null;

        [SerializeField] private List<EventInfo> _events;
        private CharacterMovement _player;

        public void Start()
        {
            _player = FindObjectOfType<CharacterMovement>();
            Manager = this;
        }
        public void Update()
        {
            CheckEvents();
        }

        private void CheckEvents()
        {
            List<EventInfo> eventsToRemove = new List<EventInfo>();


            foreach (var eventInfo in Manager._events)
            {
                if (eventInfo._eventCondition.CheckCondition() && eventInfo._active)
                {
                    switch (eventInfo._actionOption)
                    {
                        case EventActionOption.FireOnce:
                            eventInfo._events[eventInfo._index]?.Invoke();
                            eventInfo.Increment();
                            break;

                        case EventActionOption.Loop:
                            eventInfo._events[eventInfo._index]?.Invoke();
                            eventInfo.Increment();
                            break;

                        case EventActionOption.Random:
                            eventInfo._events[Random.Range(0, eventInfo._events.Count)]?.Invoke();
                            break;

                        case EventActionOption.RandomMultiple:
                            int amountOfEvents = Random.Range(1, eventInfo._events.Count);

                            List<int> alreadyRun = new List<int>();

                            for (int i = 0; i < amountOfEvents + 1; i++)
                            {
                                int indexToAffect = Random.Range(0, eventInfo._events.Count);
                                if (alreadyRun.Contains(indexToAffect))
                                    i--;
                                else
                                {
                                    alreadyRun.Add(indexToAffect);
                                    eventInfo._events[indexToAffect]?.Invoke();
                                }
                            }
                            break;
                    }

                    if (eventInfo.NeedToRemove())
                        eventsToRemove.Add(eventInfo);
                }
            }



            foreach (var lookEvent in eventsToRemove)
            {
                Manager._events.Remove(lookEvent);
            }
        }

        public void AddToEvents(EventInfo _eventToAdd)
        {
            Manager._events.Add(_eventToAdd);
        }
        public void RemoveFromEvents(EventInfo _eventToRemove)
        {
            Manager._events.Remove(_eventToRemove);
        }

    }
}

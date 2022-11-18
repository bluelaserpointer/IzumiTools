using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyEvent : MonoBehaviour
{
    [Serializable]
    public struct KeyAndEvent
    {
        public KeyCode keyCode;
        public UnityEvent keyEvent;
    }
    public List<KeyAndEvent> events;
    void Update()
    {
        foreach(KeyAndEvent keyAndEvent in events)
        {
            if(Input.GetKeyDown(keyAndEvent.keyCode))
            {
                keyAndEvent.keyEvent.Invoke();
            }
        }
    }
}

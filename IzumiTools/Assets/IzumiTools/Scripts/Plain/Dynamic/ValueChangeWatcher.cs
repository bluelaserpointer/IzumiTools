using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IzumiTools
{
    [System.Serializable]
    public class ValueChangeWatcher<T>
    {
        public ValueChangeWatcher()
        {
        }
        public ValueChangeWatcher(UnityAction<T> valueChangeAction)
        {
            onValueChange.AddListener(valueChangeAction);
        }
        [SerializeField]
        T value;
        public UnityEvent<T> onValueChange = new UnityEvent<T>();
        public T Value
        {
            get => value;
            set
            {
                if (!this.value.Equals(value))
                {
                    onValueChange.Invoke(this.value = value);
                }
            }
        }
        /// <summary>
        /// Set value without invoking onValueChange event.
        /// </summary>
        /// <param name="value"></param>
        public void Initialize(T value)
        {
            this.value = value;
        }
    }
}
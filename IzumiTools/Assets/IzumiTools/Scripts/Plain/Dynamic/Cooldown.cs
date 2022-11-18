using UnityEngine;

namespace IzumiTools
{
    /// <summary>
    /// Manual progressable cooldown manager.
    /// </summary>
    [System.Serializable]
    public class Cooldown : CappedValue
    {
        public Cooldown(float maxValue)
        {
            Max = maxValue;
        }
        public Cooldown()
        {
            Max = 1;
        }
        public bool IsReady => Value == Max;
        public void Reset()
        {
            Value = 0;
        }
        public bool Eat()
        {
            if (IsReady)
            {
                Reset();
                return true;
            }
            return false;
        }
        public void Add(float value)
        {
            Value += value;
        }
        public void AddDeltaTime()
        {
            Add(Time.deltaTime);
        }
        public void AddFixedDeltaTime()
        {
            Add(Time.fixedDeltaTime);
        }
        public bool AddAndEat(float value)
        {
            Add(value);
            return Eat();
        }
        public bool AddDeltaTimeAndEat()
        {
            AddDeltaTime();
            return Eat();
        }
        public bool AddFixedDeltaTimeAndEat()
        {
            AddFixedDeltaTime();
            return Eat();
        }
    }
}
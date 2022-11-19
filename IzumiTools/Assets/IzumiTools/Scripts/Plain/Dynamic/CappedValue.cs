using UnityEngine;

namespace IzumiTools
{
    /// <summary>
    /// Toplimited positive float value for representing things like health point, magazine, and <see cref="Cooldown"/>, etc.
    /// </summary>
    [System.Serializable]
    public class CappedValue
    {
        [SerializeField]
        [Min(0f)]
        private float _max;

        public CappedValue(float maxValue)
        {
            Max = maxValue;
        }
        public CappedValue()
        {
            _max = 1;
        }

        private float _value;
        public float Value
        {
            get => _value;
            set => _value = Mathf.Clamp(value, 0, _max);
        }
        public float Max
        {
            get => _max;
            set
            {
                _max = Mathf.Max(0, value);
                if (_value > _max)
                    _value = _max;
            }
        }
        /// <summary>
        /// eauals Max - Value
        /// </summary>
        public float Space  => _max - _value;
        public float Ratio
        {
            get => _value / _max;
            set => _value = _max * Mathf.Clamp01(value);
        }
        public void Maximize()
        {
            Value = Max;
        }
        public float AddAndGetOverflow(float value)
        {
            float overflow;
            _value = ExtendedMath.ClampAndGetOverflow(_value + value, 0, _max, out overflow);
            return overflow;
        }
        public float AddAndGetDelta(float value)
        {
            float oldValue = Value;
            Value += value;
            return Value - oldValue;
        }
        /// <summary>
        /// Transfer value to the other as lot as possible, while not exceeding the amount of the source remaining, target space, and specified toplimit;
        /// </summary>
        /// <param name="target"></param>
        /// <param name="maxValue"></param>
        /// <returns>Actual transfered amount</returns>
        public float TransferTo(CappedValue target, float maxValue = int.MaxValue)
        {
            float amount = Mathf.Min(maxValue, Value, target.Space);
            Value -= amount;
            target.Value += amount;
            return amount;
        }
    }
}
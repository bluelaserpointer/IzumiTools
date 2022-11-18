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
        public float AddAndReturnOverflow(float delta)
        {
            float overflow;
            _value = ExtendedMath.ClampAndGetOverflow(_value + delta, 0, _max, out overflow);
            return overflow;
        }
        /// <summary>
        /// Transfer value to the other, ensures not exceeding source amount, target space, and specified toplimit;
        /// </summary>
        /// <param name="target"></param>
        /// <param name="maxAmount"></param>
        /// <returns>Actual transfered amount</returns>
        public float Transfer(CappedValue target, int maxAmount = int.MaxValue)
        {
            float amount = Mathf.Min(maxAmount, Value, target.Space);
            Value -= amount;
            target.Value += amount;
            return amount;
        }
    }

}
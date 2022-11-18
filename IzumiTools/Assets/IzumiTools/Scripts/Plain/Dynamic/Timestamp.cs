using UnityEngine;

namespace IzumiTools
{
    /// <summary>
    /// Rewindable time counter for ui, events, etc. For manual progressable cooldown, see <see cref="Cooldown"/>.
    /// </summary>
    [System.Serializable]
    public class Timestamp
    {
        public float lastStampTime;
        public float PassedTime => Time.timeSinceLevelLoad - lastStampTime;
        public void Stamp()
        {
            lastStampTime = Time.timeSinceLevelLoad;
        }
        public void Rewind(float value)
        {
            lastStampTime = Time.timeSinceLevelLoad - value;
        }
    }
}
using UnityEngine;

public class TimeExtension
{
    public static bool TimeBlink(float trueDuration, float falseDuration)
    {
        return Time.timeSinceLevelLoad % (trueDuration + falseDuration) > trueDuration;
    }
    public static bool TimeBlink(float halfDuration)
    {
        return Time.timeSinceLevelLoad % (halfDuration * 2) > halfDuration;
    }
}

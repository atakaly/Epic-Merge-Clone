using UnityEngine;

public static class Extensions
{
    public static float Remap(this float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        value = Mathf.Clamp(value, fromMin, fromMax);
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }
}

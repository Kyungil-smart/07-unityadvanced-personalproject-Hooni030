using UnityEngine;

public static class DebugUtil
{
    public static void DebugingColor(string text, string color = "#222222")
    {
        Debug.Log($"<color=#{color}>{text}</color>");
    }

}

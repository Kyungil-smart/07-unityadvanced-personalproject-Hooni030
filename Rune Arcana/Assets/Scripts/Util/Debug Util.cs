using UnityEngine;

public static class DebugUtil
{
    public static void DebugingColor(string text, string color = "ffffff")
    {
        Debug.Log($"<color=#{color}>{text}</color>");
    }

}

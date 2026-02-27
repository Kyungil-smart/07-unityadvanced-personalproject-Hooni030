using System.Collections.Generic;
using UnityEngine;

public static class YieldContainer
{
    private static readonly Dictionary<float, WaitForSeconds> _wait 
        = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds Wait(float seconds)
    {
        if (!_wait.ContainsKey(seconds))
        {
            _wait.Add(seconds, new WaitForSeconds(seconds));
        }
        
        return _wait[seconds];
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public static class Localization
{
    public static event Action OnDataSet;

    private static Dictionary<string, string> _data = new Dictionary<string, string>();
    
    public static void SetData(Dictionary<string, string> data)
    {
        _data = data;

        OnDataSet.InvokeIfNotNull();
    }
    
    public static string Get(string key)
    {
        if(_data.ContainsKey(key))
        {
            return _data[key];
        }

        return "undefined";
    }
}

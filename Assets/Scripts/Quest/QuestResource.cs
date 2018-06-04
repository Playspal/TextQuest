using System;

using UnityEngine;

public class QuestResource
{
    public Action<int, int> OnChange;
    public QuestResourceType Type;
    
    public int Value { get; private set;  }

    public QuestResource(QuestResourceType type, int value)
    {
        Type = type;
        Value = value;
    }
    
    /// <summary>
    /// Update value
    /// </summary>
    /// <returns>Returns true if updated. Returns false if trying to decrease minimum value</returns>
    public bool Update(int value)
    {
        if(value <= 0 && Value < Mathf.Abs(value))
        {
            return false;
        }
    
        Value += value;
        
        OnChange.InvokeIfNotNull(Value, value);

        return true;
    }
}

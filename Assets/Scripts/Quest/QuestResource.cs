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
    
    public void Update(int value)
    {
        Value += value;
        
        if(Value < 0)
        {
            Value = 0;
        }
        
        OnChange.InvokeIfNotNull(Value, value);
    }
}

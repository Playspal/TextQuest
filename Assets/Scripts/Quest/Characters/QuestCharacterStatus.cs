using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCharacterStatus
{
    public int Value
    {
        get
        {
            return _value;
        }
        
        protected set
        {
            _value = Mathf.Clamp(value, 0, 100);
        }
    }
    
    public bool IsCritical
    {
        get
        {
            return Value <= 0;
        }
    }

    private int _value;
    
    public QuestCharacterStatus()
    {
        Value = 0;
    }
    
    public QuestCharacterStatus(int value)
    {
        Value = value;
    }
    
    public virtual void Update(int value)
    {
        Value += value;
    }
    
    public virtual void Process()
    {
    }
    
    public virtual void RefillUsingResource()
    {
    }
}

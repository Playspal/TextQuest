using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCharacterStatus
{
    public QuestCharacterStatusType StatusType { get; protected set; }

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
    
    public float ValueNormalized
    {
        get
        {
            return (float)Value / 100f;
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
    protected bool _refillEnabled = true;
    
    public QuestCharacterStatus()
    {
        Value = 0;
    }
    
    public QuestCharacterStatus(int value)
    {
        Value = value;
    }
    
    public virtual void SetRefillEnabled(bool value)
    {
        _refillEnabled = value;
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

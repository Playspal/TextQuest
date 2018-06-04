using System;

public class QuestBoolean
{
    public event Action<bool> OnChange;
    
    public bool Value { get; private set; }
    
    public QuestBoolean(bool value)
    {
        Value = value;
    }
    
    public bool IsFalse
    {
        get
        {
            return Value == false;
        }
    }
    
    public bool IsTrue
    {
        get
        {
            return Value;
        }
    }
    
    public void Update(bool value)
    {
        if(Value != value)
        {
            Value = value;
            OnChange.InvokeIfNotNull(Value);
        }
    }
}
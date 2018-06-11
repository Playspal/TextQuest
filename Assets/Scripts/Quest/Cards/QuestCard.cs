using System;
using System.Collections.Generic;

using UnityEngine;

public class QuestCard : QuestEvent
{
    public Action<QuestCard> OnNextCardChoosen;
    
    public Action<string, Action> OnAnswer;
    public Action<QuestResource[]> OnAnswerResources;

    protected bool _isInUse = false;
    protected int _timeOfLastUse = -1;
    protected List<QuestResource> _updatedResources = new List<QuestResource>();

    public virtual QuestCardType GetCardType()
    {
        return QuestCardType.Undefined;
    }

    public virtual string GetQuestion()
    {
        return null;
    }
    
    public virtual QuestAction GetAction1()
    {
        return null;
    }
    
    public virtual QuestAction GetAction2()
    {
        return null;
    }
    
    public virtual float GetCooldown()
    {
        return float.MaxValue;
    }
    
    public virtual int GetPriority()
    {
        return 5;
    }
    
    /// <summary>
    /// Is the card ready to use.
    /// </summary>
    public virtual bool IsReadyToUse()
    {
        return false;
    }
    
    public virtual bool IsCool()
    {
        if(_isInUse)
        {
            return false;
        }

        return _timeOfLastUse <= -1 || Quest.Instance.Status.Date.Day - _timeOfLastUse >= GetCooldown();
    }
    
    protected virtual void UpdateTime(int value)
    {
        Quest.Instance.Status.Date.AddHours(value);
    }
    
    protected virtual bool UpdateResource(QuestResourceType type, int value)
    {
        if(Quest.Instance.Status.Resources.Update(type, value))
        {
            _updatedResources.Add(new QuestResource(type, value));
            return true;
        }

        return false;
    }

    public virtual void Begin()
    {
        _isInUse = true;
    }

    protected virtual void End(string message, QuestCard nextCard = null, Action callback = null)
    {
        _timeOfLastUse = Quest.Instance.Status.Date.Day;
    
        OnAnswer.InvokeIfNotNull
        (
            message,
            () =>
            {
                callback.InvokeIfNotNull();

                _updatedResources.Clear();
                _isInUse = false;
            }
        );
        
        OnAnswerResources(_updatedResources.ToArray());
        //OnNextCardChoosen.InvokeIfNotNull(nextCard);

        if (nextCard != null)
        {
            Quest.Instance.AddCard(nextCard);
        }
    }
}
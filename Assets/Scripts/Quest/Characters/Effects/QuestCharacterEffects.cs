using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCharacterEffects
{
    private List<QuestCharacterEffect> _effects = new List<QuestCharacterEffect>();
    
    /// <summary>
    /// Adds or removes specified effect
    /// </summary>
    /// <returns>Returns true if effect added or removed</returns>
    public bool Set(QuestCharacterEffectType effectType, bool value)
    {
        if(value && Add(effectType))
        {
            return true;
        }
        
        if(!value && Remove(effectType))
        {
            return true;
        }

        return false;
    }
    
    /// <summary>
    /// Adds new effect
    /// </summary>
    /// <returns>Returns true if create was added</returns>
    public bool Add(QuestCharacterEffectType effectType)
    {
        if(Contains(effectType))
        {
            return false;
        }

        _effects.Add(new QuestCharacterEffect(effectType));

        return true;
    }
    
    /// <summary>
    /// Removes effects
    /// </summary>
    /// <returns>Return true if effect was removed</returns>
    public bool Remove(QuestCharacterEffectType effectType)
    {
        QuestCharacterEffect effect = Get(effectType);
        
        if(effect != null)
        {
            _effects.Remove(effect);
            return true;
        }

        return false;
    }
    
    /// <summary>
    /// Is the effects list contains effect with specified type
    /// </summary>
    public bool Contains(QuestCharacterEffectType effectType)
    {
        return Get(effectType) != null;
    }
    
    /// <summary>
    /// Gets the effect by type
    /// </summary>
    public QuestCharacterEffect Get(QuestCharacterEffectType effectType)
    {
        return _effects.Find(x => x.EffectType == effectType);
    }
    
    /// <summary>
    /// Gets all active effects
    /// </summary>
    public List<QuestCharacterEffect> GetAll()
    {
        return _effects;
    }
}
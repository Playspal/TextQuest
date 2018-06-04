using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCharacterEffect
{
    public QuestCharacterEffectType EffectType { get; protected set; }
    
    public QuestCharacterEffect(QuestCharacterEffectType effectType)
    {
        EffectType = effectType;
    }
}

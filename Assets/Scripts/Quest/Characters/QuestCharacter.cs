using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestCharacter
{
    public event Action<QuestCharacter, QuestCharacterEffectType> OnEffectAdded;
    public event Action<QuestCharacter, QuestCharacterEffectType> OnEffectRemoved;
    public event Action<QuestCharacter> OnDeath;

    public string Name = "Unnamed";

    public bool IsInShelter = false;
    public bool IsDead = false;
    public bool IsInAdventure { get; private set; }

    public QuestCharacterDeathReason DeathReason = QuestCharacterDeathReason.Unknown;
    public QuestCharacterBurialType BurialType = QuestCharacterBurialType.None;

    public QuestCharacterEffects Effects = new QuestCharacterEffects();
    public QuestCharacterStatuses Statuses = new QuestCharacterStatuses();
    
    public void SetIsInAdventure(bool value)
    {
        IsInAdventure = value;

        Statuses.Water.SetRefillEnabled(!value);
        Statuses.Food.SetRefillEnabled(!value);
    }
    
    public void SetBurialType(QuestCharacterBurialType value)
    {
        BurialType = value;
    }
    
    public string GetJobText()
    {
        QuestBuilding building = Quest.Instance.Status.Buildings.GetBuildingByWorker(this);
        return building != null ? building.DescriptionJob : "Ничем не занят";
    }
    
    public int GetInventorySize()
    {
        return 1 + Mathf.CeilToInt(4f * Statuses.Stamina.ValueNormalized);
    }
    
    public void SetEffect(QuestCharacterEffectType effectType, bool value)
    {
        if (value && Effects.Add(effectType))
        {
            OnEffectAdded.InvokeIfNotNull(this, effectType);
        }
        
        if (!value && Effects.Remove(effectType))
        {
            OnEffectRemoved.InvokeIfNotNull(this, effectType);
        }
    }
    
    public virtual void ProcessHours(int hours)
    {
        for (int i = 0; i < hours; i++)
        {
            ProcessOneHour();
        }
    }
    
    public virtual void ProcessOneHour()
    {
        if(!IsInShelter)
        {
            return;
        }
        
        if(IsDead)
        {
            return;
        }

        Statuses.Process(IsInAdventure);
        
        SetEffect(QuestCharacterEffectType.Thirst, Statuses.Water.IsCritical);
        SetEffect(QuestCharacterEffectType.Starvation, Statuses.Food.IsCritical);
        SetEffect(QuestCharacterEffectType.Debilitation, Statuses.Stamina.IsCritical);

        if(Statuses.Health.IsCritical)
        {
            IsDead = true;
            
            if(Effects.Contains(QuestCharacterEffectType.Thirst))
            {
                DeathReason = QuestCharacterDeathReason.Thirst;
            }
            else if(Effects.Contains(QuestCharacterEffectType.Starvation))
            {
                DeathReason = QuestCharacterDeathReason.Starvation;
            }
            else if(Effects.Contains(QuestCharacterEffectType.Debilitation))
            {
                DeathReason = QuestCharacterDeathReason.Debilitation;
            }

            OnDeath.InvokeIfNotNull(this);
        }
    }
}

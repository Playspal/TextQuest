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

    public QuestCharacterStatusHealth StatusHealth = new QuestCharacterStatusHealth(100);
    public QuestCharacterStatusWater StatusWater = new QuestCharacterStatusWater(100);
    public QuestCharacterStatusFood StatusFood = new QuestCharacterStatusFood(100);
    public QuestCharacterStatusStamina StatusStamina = new QuestCharacterStatusStamina(100);
    
    public void SetIsInAdventure(bool value)
    {
        IsInAdventure = value;
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

        StatusWater.Process();
        StatusFood.Process();
        
        SetEffect(QuestCharacterEffectType.Thirst, StatusWater.IsCritical);
        SetEffect(QuestCharacterEffectType.Starvation, StatusFood.IsCritical);

        if(StatusWater.IsCritical)
        {
            StatusHealth.Update(-2);
        }
        
        if(StatusFood.IsCritical)
        {
            StatusHealth.Update(-1);
        }

        if(StatusHealth.IsCritical)
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

            OnDeath.InvokeIfNotNull(this);
        }
    }
}

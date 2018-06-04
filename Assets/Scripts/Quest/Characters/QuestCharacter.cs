using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestCharacter
{
    public event Action<QuestCharacter> OnDeath;

    public string Name = "Unnamed";

    public bool IsInShelter = false;
    public bool IsDead = false;

    public bool IsSufferingByHunger = false;
    public bool IsSufferingByThirst = false;

    public QuestCharacterDeathReason DeathReason = QuestCharacterDeathReason.Unknown;
    public QuestCharacterBurialType BurialType = QuestCharacterBurialType.None;
    
    public int Health = 100;
    public int Stamina = 100;
    public int Thirst = 0;
    public int Hunger = 0;
    
    public void SetBurialType(QuestCharacterBurialType value)
    {
        BurialType = value;
    }
    
    public string GetJobText()
    {
        QuestBuilding building = Quest.Instance.Status.Buildings.GetBuildingByWorker(this);
        return building != null ? building.DescriptionJob : "Ничем не занят";
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

        IsSufferingByHunger = Hunger >= 100;
        IsSufferingByThirst = Thirst >= 100;

        UpdateThirst();
        
        if(Hunger > 50)
        {
            if(Quest.Instance.Status.Resources.Food.Value > 0)
            {
                Quest.Instance.Status.Resources.Food.Update(-1);
                Hunger -= 50;
            }
        }

        if(Hunger < 100 && Hunger + 3 >= 100)
        {
            Quest.Instance.AddStory(Name + " страдает от голода.", null);
        }
        
        Hunger += 3;
        
        if(Hunger >= 100)
        {
            Health -= 1;
        }
        
        if(Health <= 0)
        {
            IsDead = true;
            
            if(Hunger >= 100)
            {
                DeathReason = QuestCharacterDeathReason.Starvation;
            }
            else if(Thirst >= 100)
            {
                DeathReason = QuestCharacterDeathReason.Thirst;
            }

            OnDeath.InvokeIfNotNull(this);
        }
    }
    
    private void UpdateThirst()
    {
        if(Quest.Instance.Status.Weather.Weather == QuestWeather.Type.Rain)
        {
            Thirst -= 5;
        }
        else
        {
            if(Thirst < 100 && Thirst + 6 >= 100)
            {
                Quest.Instance.AddStory(Name + " страдает от жажды.", null);
            }
            
            Thirst += 6;
        
            if(Thirst > 25)
            {
                if(Quest.Instance.Status.Resources.Water.Value > 0)
                {
                    Quest.Instance.Status.Resources.Water.Update(-1);
                    Thirst -= 25;
                }
            }
            
            if(Thirst >= 100)
            {
                Health -= 2;
            }
        }
        
        Thirst = Mathf.Clamp(Thirst, 0, 100);
    }
}

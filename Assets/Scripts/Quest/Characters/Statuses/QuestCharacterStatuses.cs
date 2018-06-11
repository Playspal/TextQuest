using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCharacterStatuses
{
    public QuestCharacterStatusHealth Health = new QuestCharacterStatusHealth(100);
    public QuestCharacterStatusWater Water = new QuestCharacterStatusWater(100);
    public QuestCharacterStatusFood Food = new QuestCharacterStatusFood(100);
    public QuestCharacterStatusStamina Stamina = new QuestCharacterStatusStamina(100);
    public QuestCharacterStatusMadness Madness = new QuestCharacterStatusMadness(100);
    
    public void Process(bool isInAdventure)
    {
        Water.Process();
        Food.Process();

        if (isInAdventure)
        {
            Stamina.Process();
            Stamina.Process();
            Stamina.Process();
        }
        else
        {
            Stamina.Process();
        }
        
        if(Water.IsCritical)
        {
            Health.Update(-2);
        }
        
        if(Food.IsCritical)
        {
            Health.Update(-1);
        }
        
        if(Health.Value < 50)
        {
            Madness.Update(-1);
        }
    }

    public void UpdateStatus(QuestCharacterStatusType type, int value)
    {
        QuestCharacterStatus status = GetStatusByType(type);

        if (status != null)
        {
            status.Update(value);
        }
    }

    public QuestCharacterStatus GetStatusByType(QuestCharacterStatusType type)
    {
        switch(type)
        {
            case QuestCharacterStatusType.Food: return Food;
            case QuestCharacterStatusType.Health: return Health;
            case QuestCharacterStatusType.Madness: return Madness;
            case QuestCharacterStatusType.Stamina: return Stamina;
            case QuestCharacterStatusType.Water: return Water;
        }

        return null;
    }
}

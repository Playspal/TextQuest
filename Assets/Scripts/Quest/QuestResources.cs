using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestResources
{
    // Currencies
    public QuestResource Gold = new QuestResource(QuestResourceType.Gold, 0);
    public QuestResource Diamonds = new QuestResource(QuestResourceType.Diamonds, 0);

    // General resources
    public QuestResource Food = new QuestResource(QuestResourceType.Food, 5);
    public QuestResource Water = new QuestResource(QuestResourceType.Water, 5);
    public QuestResource Weapon = new QuestResource(QuestResourceType.Weapon, 1);
    public QuestResource Beds = new QuestResource(QuestResourceType.Beds, 0);

    // Craft resourcs
    public QuestResource Wood = new QuestResource(QuestResourceType.Wood, 3);
    public QuestResource Gas = new QuestResource(QuestResourceType.Wood, 1);

    // Story modifiers
    public QuestResource Morale = new QuestResource(QuestResourceType.Morale, 0);
    public QuestResource Karma = new QuestResource(QuestResourceType.Karma, 0);
    
    /// <summary>
    /// Check than player have enough amount of provided resources
    /// </summary>
    public bool IsEnough(List<QuestResource> resources)
    {
        for (int i = 0; i < resources.Count; i++)
        {
            if(GetResourceByType(resources[i].Type).Value < resources[i].Value)
            {
                return false;
            }
        }

        return true;
    }
    
    /// <summary>
    /// Change amount of resource with specified type
    /// </summary>
    public void Update(QuestResourceType type, int value)
    {
        QuestResource questResource = GetResourceByType(type);

        if (questResource != null)
        {
            questResource.Update(value);
        }
    }
    
    /// <summary>
    /// Gets the instance of resource by specified type
    /// </summary>
    public QuestResource GetResourceByType(QuestResourceType type)
    {
        switch(type)
        {
            case QuestResourceType.Gold: return Gold;
            case QuestResourceType.Diamonds: return Diamonds;
            
            case QuestResourceType.Food: return Food;
            case QuestResourceType.Water: return Water;
            case QuestResourceType.Weapon: return Weapon;
            case QuestResourceType.Beds: return Beds;
            
            case QuestResourceType.Wood: return Wood;
            case QuestResourceType.Gas: return Gas;
            
            case QuestResourceType.Morale: return Morale;
            case QuestResourceType.Karma: return Karma;
        }

        return null;
    }
}
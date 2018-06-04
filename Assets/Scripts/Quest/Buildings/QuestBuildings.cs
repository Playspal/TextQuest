using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBuildings
{
    public List<QuestBuilding> Buildings = new List<QuestBuilding>()
    {
        new QuestBuildingSignalFire(),
        new QuestBuildingSignalFire(),
        new QuestBuildingSignalFire(),
        new QuestBuildingSignalFire(),
        new QuestBuildingSignalFire(),
        new QuestBuildingSignalFire(),
        new QuestBuildingSignalFire(),
        new QuestBuildingSignalFire(),
        new QuestBuildingSignalFire()
    };
    
    public bool IsBuilded(QuestBuildingType type)
    {
        return GetBuilding(type).IsBuilded;
    }
    
    public QuestBuilding GetBuildingByWorker(QuestCharacter character)
    {
        return Buildings.Find(x => x.Worker == character);
    }
    
    public QuestBuilding GetBuilding(QuestBuildingType type)
    {
        return Buildings.Find(x => x.BuildingType == type);
    }
    
    public void ProcessMinutes(int value)
    {
        for (int i = 0; i < Buildings.Count; i++)
        {
            Buildings[i].ProcessMinutes(value);
        }
    }
}

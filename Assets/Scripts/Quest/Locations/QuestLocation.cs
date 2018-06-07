using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLocation
{
    public QuestLocationType LocationType { get; protected set; }
    
    public string Name { get; protected set; }
    public Vector2 Coordinates { get; protected set; }
    
    public bool IsDiscovered { get; private set; }
    
    public QuestResourceType[] Resources { get; protected set;  }
    
    public QuestLocation(QuestLocationType locationType)
    {
        LocationType = locationType;
        IsDiscovered = locationType == QuestLocationType.Home;
        
        switch(locationType)
        {
            case QuestLocationType.Home:
                Resources = new QuestResourceType[0];
                break;
                
            case QuestLocationType.Church:
                Resources = new QuestResourceType[]
                {
                    QuestResourceType.Food,
                    QuestResourceType.Water,
                    QuestResourceType.Wood
                };
                break;
        }
    }
    
    public void SetCoordinates(Vector2 value)
    {
        Coordinates = value;
    }
    
    public void SetName(string value)
    {
        Name = value;
    }
}

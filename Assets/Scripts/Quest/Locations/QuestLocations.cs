using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLocations
{
    public List<QuestLocation> Locations { get; private set; }

    public QuestLocations()
    {
        Locations = new List<QuestLocation>();

        Locations.Add(Create(QuestLocationType.Home, Vector2.zero));

        for (int i = 0; i < 10; i++)
        {
            Locations.Add(CreateRandom());    
        }
    }
    
    private QuestLocation CreateRandom()
    {
        Array types = QuestLocationType.GetValues(typeof(QuestLocationType));
        QuestLocationType type = QuestLocationType.Home;
        
        while(type == QuestLocationType.Home)
        {
            type = (QuestLocationType)types.GetValue(UnityEngine.Random.Range(0, types.Length));
        }

        return Create(type, GenerateRandomPosition());
    }
    
    private QuestLocation Create(QuestLocationType type, Vector2 position)
    {
        QuestLocation location = new QuestLocation(type);

        location.SetName(GenerateLocationName(type));
        location.SetCoordinates(position);

        return location;
    }
    
    private string GenerateLocationName(QuestLocationType type)
    {
        switch(type)
        {
            case QuestLocationType.Home: return "Убежище";
            case QuestLocationType.Church: return "Церковь";
        }

        return "Локация";
    }
    
    private Vector2 GenerateRandomPosition()
    {
        Vector2 output = Vector2.zero;

        int timeout = 200;
        float size = 300;

        while (timeout > 0)
        {
            bool isValid = true;
        
            output = new Vector2
            (
                UnityEngine.Random.Range(-size, size),
                UnityEngine.Random.Range(-size, size)
            );

            for (int i = 0; i < Locations.Count; i++)
            {
                float deltax = output.x - Locations[i].Coordinates.x;
                float deltay = output.y - Locations[i].Coordinates.y;
                float distance = Mathf.Sqrt(deltax * deltax + deltay * deltay);
                
                if(distance < 100)
                {
                    isValid = false;
                    break;
                }
            }
            
            if(isValid)
            {
                break;
            }

            timeout--;
        }

        return output;
    }
}

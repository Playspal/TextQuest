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
    
    public void SetAsDiscovered()
    {
        if (!IsDiscovered)
        {
            IsDiscovered = true;

            switch(LocationType)
            {
                case QuestLocationType.Church:
                    Quest.Instance.AddStory("Мы дошли до точки назначения. Это оказалась старая церковь", null);
                    break;
            }

            Ui.ScreenMap.Refresh();
            Ui.ShowScreenCards();
        }        
    }
    
    public void ProcessStory()
    {
        GetLoot();
        //Quest.Instance.GenerateCard(QuestCardType.IsInLocation, true);
    }
    
    public void GetLoot()
    {
        Quest.Instance.Status.Resources.Update(QuestResourceType.Food, +5);
        Quest.Instance.AddStory("Обыскав всю локацию нам удалось найти немного ресурсов", null);
        Ui.ShowScreenCards();
    }
    
    public void SetCoordinates(Vector2 value)
    {
        Coordinates = value;
    }
    
    public void SetName(string value)
    {
        Name = value;
    }
    
    /// <summary>
    /// Return distance from this location to shelter
    /// </summary>
    public int GetDistance(bool round)
    {
        return Distance(Quest.Instance.Status.Locations.Home, this, round);
    }
    
    public int GetRequiredWater(int people)
    {
        return Mathf.RoundToInt(GetDistance(true) / 10) * people;
    }
    
    public int GetRequiredMinutes()
    {
        return Duration(Quest.Instance.Status.Locations.Home, this);
    }
    
    /// <summary>
    /// Gets distance betwen two locations
    /// </summary>
    public static int Distance(QuestLocation a, QuestLocation b, bool round)
    {
        float distance = Vector2.Distance(a.Coordinates, b.Coordinates) / 10f;
        
        if(round)
        {
            return Mathf.RoundToInt(distance / 10) * 10;
        }

        return Mathf.RoundToInt(distance);
    }
    
    /// <summary>
    /// Gets required minutes to move from A to B
    /// </summary>
    public static int Duration(QuestLocation a, QuestLocation b)
    {
        return Mathf.RoundToInt(Distance(a, b, false) / 4 * 60);
    }
}

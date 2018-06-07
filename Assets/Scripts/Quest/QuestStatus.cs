using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStatus
{
    public QuestDate Date = new QuestDate();
    public QuestWeather Weather = new QuestWeather();
    public QuestResources Resources = new QuestResources();
    public QuestLocations Locations = new QuestLocations();
    public QuestBuildings Buildings = new QuestBuildings();
    public QuestCharacters Characters = new QuestCharacters();

    public QuestLocation CurrentLocation { get; private set; }
    public QuestLocationType CurrentLocationType
    {
        get
        {
            return CurrentLocation != null ? CurrentLocation.LocationType : QuestLocationType.Wasteland;
        }
    }
    
    public QuestStatus()
    {
        SetCurrentLocation(Locations.Home);
    
        Date.OnMinutesPass += ProcessMinutes;
        Date.OnHoursPass += ProcessHours;
        Date.OnDaysPass += ProcessDays;

        Weather.OnWeatherChange += OnWeatherChange;
    }
    
    public void SetCurrentLocation(QuestLocation location)
    {
        CurrentLocation = location;
    }
    
    private void ProcessMinutes(int value)
    {
        Buildings.ProcessMinutes(value);
    }
    
    private void ProcessHours(int value)
    {
        Weather.ProcessHours(value);
        Characters.ProcessHours(value);
    }
    
    private void ProcessDays(int value)
    {
    }
    
    private void OnWeatherChange(QuestWeather.Type weather)
    {    
        switch(weather)
        {
            case QuestWeather.Type.Normal:
                break;
            
            case QuestWeather.Type.Rain:
                Quest.Instance.AddStory("Начался сильный дождь.", null);
                break;
                
            case QuestWeather.Type.Snow:
                break;
        }
    }
    
    private void OnDeath(QuestCharacter questCharacter)
    {
    }
}
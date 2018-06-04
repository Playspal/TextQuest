using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestWeather
{
    public enum Type
    {
        Normal,
        Rain,
        Snow
    }

    public Action<Type> OnWeatherChange;
    
    public Type Weather = Type.Normal;

    private int _delay = 12;
    
    public void SetWeather(Type weather, int duration)
    {
        _delay = duration;
    
        Weather = weather;
        
        OnWeatherChange.InvokeIfNotNull(Weather);
    }
    
    public void ProcessHours(int value)
    {
        _delay -= value;
        
        if(_delay <= 0)
        {
            switch(Weather)
            {
                case Type.Normal:
                    if(UnityEngine.Random.Range(0f, 1f) < 0.25f)
                    {
                        SetWeather(Type.Rain, UnityEngine.Random.Range(6, 48));
                    }
                    break;
                    
                case Type.Rain:
                    SetWeather(Type.Normal, UnityEngine.Random.Range(12, 48));
                    break;
            }
        }
    }
}

using System;

public class QuestDate
{
    public Action<int> OnMinutesPass;
    public Action<int> OnHoursPass;
    public Action<int> OnDaysPass;

    public int HoursSinceGameStart { get; private set; }
    
    public int Minutes { get; private set; }
    public int Hours { get; private set; }
    public int Day { get; private set; }
    
    public bool IsNight { get { return Hours >= 0 && Hours < 6; } }
    public bool IsMorning { get { return Hours >= 6 && Hours < 12; } }
    public bool IsDay { get { return Hours >= 12 && Hours < 18; } }
    public bool IsEvening { get { return Hours >= 18 && Hours < 24; } }

    public QuestDate()
    {
        Day = 0;
        Hours = 6;
        Minutes = 45;
    }
    
    public void AddMinutes(int value)
    {
        Minutes += value;

        OnMinutesPass.InvokeIfNotNull(value);
        
        while(Minutes >= 60)
        {
            Minutes -= 60;
            AddHours(1);
        }
    }

    public void AddHours(int value)
    {
        Hours += value;
        HoursSinceGameStart += value;

        OnHoursPass.InvokeIfNotNull(value);
        
        while (Hours >= 24)
        {
            Hours -= 24;
            AddDay(1);
        }
    }
    
    public void AddDay(int value)
    {
        Day += value;
        
        OnDaysPass.InvokeIfNotNull(value);
    }
}
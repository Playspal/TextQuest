using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestAdventure
{
    public Action OnAdventureFinished;

    public readonly QuestLocation From;
    public readonly QuestLocation To;

    /// <summary>
    /// Minutes left to arrive to destination
    /// </summary>
    public int Timeleft { get; private set; }

    public QuestAdventure(QuestLocation from, QuestLocation to)
    {
        From = from;
        To = to;

        Timeleft = 60 * 60;
    }
    
    public void ProcessMinutes(int value)
    {
        Timeleft -= value;
        
        if(Timeleft <= 0)
        {
            OnAdventureFinished.InvokeIfNotNull();
        }
    }
}

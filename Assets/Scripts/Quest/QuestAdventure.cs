using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestAdventure
{
    public event Action OnNearToShelter;
    public event Action OnRandomEvent;
    public event Action OnTargetReached;
    public event Action OnAdventureStarted;
    public event Action OnAdventureFinished;

    public QuestLocation From { get; private set; }
    public QuestLocation To { get; private set; }
    public readonly QuestCharactersGroup Characters;

    /// <summary>
    /// Minutes left to arrive to destination
    /// </summary>
    public int Timeleft { get; private set; }
    public int TimeleftDefined { get; private set; }
    public float TimeleftNormalized { get; private set; }

    private QuestLocation _locationA;
    private QuestLocation _locationB;

    private bool _isRandomEventDispathed = false;
    private bool _isNearToShelterDispathed = false;

    public QuestAdventure(QuestLocation a, QuestLocation b, QuestCharactersGroup characters)
    {        
        Characters = characters;
        SetCharactersStatus(true);
        
        _locationA = a;
        _locationB = b;

        GotoB();
    }

    private void SetCharactersStatus(bool isInAdventure)
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            Characters.Get(i).SetIsInAdventure(isInAdventure);
        }
    }

    private void GotoA()
    {
        Goto(_locationB, _locationA);
    }
    
    private void GotoB()
    {
        Goto(_locationA, _locationB);
    }
    
    private void Goto(QuestLocation from, QuestLocation to)
    {
        From = from;
        To = to;

        Timeleft = QuestLocation.Duration(From, To);
        TimeleftDefined = Timeleft;
        TimeleftNormalized = 1;
    }

    public void ProcessMinutes(int value)
    {
        Timeleft -= value;
        TimeleftNormalized = (float)Timeleft / (float)TimeleftDefined;
        
        if(Timeleft <= 0)
        {
            TimeleftNormalized = 0;
            
            if(To == _locationB)
            {
                OnTargetReached.InvokeIfNotNull();
                GotoA();
            }
            else if(To == _locationA)
            {
                SetCharactersStatus(false);
                OnAdventureFinished.InvokeIfNotNull();
            }
        }
        else
        {
            bool dispatch = UnityEngine.Random.Range(0f, 100f) < 1;
        
            if(dispatch)
            {
                if (TimeleftNormalized > 0.2f && TimeleftNormalized < 0.8f)
                {
                    if(!_isRandomEventDispathed)
                    {
                        _isRandomEventDispathed = true;
                        OnRandomEvent.InvokeIfNotNull();
                    }
                }
                
                if(To == _locationB && TimeleftNormalized > 0.9f)
                {
                    if(!_isNearToShelterDispathed)
                    {
                        _isNearToShelterDispathed = true;
                        OnNearToShelter.InvokeIfNotNull();
                    }
                }
            }
        }
    }
}

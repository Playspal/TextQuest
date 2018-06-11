using System;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public static Quest Instance;

    public bool IsPause = false;
    
    public QuestStatus Status { get; private set; }
    public QuestAdventure Adventure { get; private set; }

    public List<QuestEvent> Queue = new List<QuestEvent>();

    private float _timer = 0;

    public Quest()
    {
        Instance = this;
        
        Status = new QuestStatus();
        Status.Date.OnMinutesPass += (int minutes) =>
        {
            if(Adventure != null)
            {
                Adventure.ProcessMinutes(minutes);
            }
        };
        
        Status.Date.OnHoursPass += (int hours) =>
        {
            if(Status.Weather.Weather == QuestWeather.Type.Rain)
            {
                Status.Resources.Update(QuestResourceType.Water, +1);
            }
            
            switch(Status.CurrentLocationType)
            {
                case QuestLocationType.Wasteland:
                    break;
                    
                case QuestLocationType.Home:
                    GenerateCard(QuestCardType.IsInShelter, false);
                    break;
            };
        };
        //Process(null);
    }
    
    public void SetPause(bool value)
    {
        IsPause = value;
    }
    
    public void StartAdventure(QuestLocation location, QuestCharactersGroup characters)
    {
        Adventure = new QuestAdventure(Status.Locations.Home, location, characters);
        Adventure.OnAdventureFinished += StopAdventure;
        Adventure.OnTargetReached += () =>
        {
            Status.SetCurrentLocation(Adventure.To);
            
            Adventure.To.SetAsDiscovered();
            Adventure.To.ProcessStory();
            
            Status.SetCurrentLocation(null);
        };

        Adventure.OnNearToShelter += () =>
        {
            AddStory("Случайное событие рядом с лагерем", null);
            Ui.ShowScreenCards();
        };
        Adventure.OnRandomEvent += () => GenerateCard(QuestCardType.IsInWasteland, true);
        
        Status.SetCurrentLocation(null);
    }
    
    public void StopAdventure()
    {
        Adventure = null;
        Status.SetCurrentLocation(Status.Locations.Home);
        Ui.ScreenMap.Refresh();
    }
    
    public bool GenerateCard(QuestCardType type, bool showCardsScreen)
    {
        QuestCard card = QuestCards.GetCardByType(type);
        
        if (card != null)
        {
            AddCard(card);

            if (showCardsScreen)
            {
                Ui.ShowScreenCards();
            }

            return true;
        }

        return false;
    }
    
    public void AddStory(string message, Action callback)
    {
        AddEvent
        (
            new QuestStory
            {
                Message = message,
                Callback = callback
            }
        );
    }
    
    public void AddCard(QuestCard questCard)
    {
        questCard.Begin();
        AddEvent(questCard);
    }
    
    public void AddEvent(QuestEvent questEvent)
    {
        Queue.Add(questEvent);
    }
    
    public void Process(QuestCard card)
    {
//        card = card ?? QuestCards.GetCardByType(QuestCardType.NewTurn);

        if (card != null)
        {
            card.OnNextCardChoosen = Process;
            //Ui.SetupCard(card);
        }
    }
    
    public void Update()
    {    
        if(!IsPause)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _timer += Time.deltaTime * 1000f;
            }
            else
            {
                _timer += Time.deltaTime * 100f;
            }
            
            while(_timer > 1)
            {
                _timer -= 1;

                Status.Date.AddMinutes(1);
            }
        }
    }
}

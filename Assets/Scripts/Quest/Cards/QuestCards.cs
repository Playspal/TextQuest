using System;
using System.Collections.Generic;

using UnityEngine;

public static class QuestCards
{
    public static List<QuestCard> Cards = new List<QuestCard>()
    {
        new QuestCardIsInShelterSignalFireStranger01(),
        new QuestCardCharacterDieFromStarvation()
    };
    
    public static QuestCard GetRandomCard()
    {
        return GetRandomCardInList
        (
            Cards.FindAll(x => x.IsCool() && x.IsReadyToUse())
        );
    }
    
    public static QuestCard GetCardByType(QuestCardType[] types)
    {
        int index = UnityEngine.Random.Range(0, types.Length);
        return GetCardByType(types[index]);
    }
    
    public static QuestCard GetCardByType(QuestCardType type)
    {
        List<QuestCard> cards = Cards.FindAll
        (
            x => x.IsCool() && x.IsReadyToUse() && x.GetCardType() == type
            
        );
         
        return GetRandomCardInList(cards);
    }
    
    public static QuestCard GetCard<T>() where T : QuestCard
    {
        return Cards.Find(x => x is T);
    }
    
    private static QuestCard GetRandomCardInList(List<QuestCard> cards)
    {
        List<int> temp = new List<int>();

        for (int i = 0; i < cards.Count; i++)
        {
            for (int j = 0; j < cards[i].GetPriority(); j++)
            {
                temp.Add(i);
            }
        }

        if(temp.Count <= 0)
        {
            return null;
        }

        int index = temp[UnityEngine.Random.Range(0, temp.Count)];

        return cards[index];
    }
}
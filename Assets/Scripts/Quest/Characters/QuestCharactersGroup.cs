using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestCharactersGroup
{
    public List<QuestCharacter> Characters { get; private set; }
    
    public int Count
    {
        get
        {
            return Characters.Count;
        }
    }
    
    public QuestCharactersGroup()
    {
        Characters = new List<QuestCharacter>();
    }
    
    public QuestCharactersGroup(List<QuestCharacter> characters)
    {
        Characters = new List<QuestCharacter>();
        Add(characters);
    }
    
    public QuestCharactersGroup FindAllWhoReadyForAdventure()
    {
        return FindAll
        (
            x =>
            x.Effects.Contains(QuestCharacterEffectType.Debilitation) == false
        );
    }
    
    public QuestCharactersGroup FindAll(Predicate<QuestCharacter> predicate)
    {
        return new QuestCharactersGroup(Characters.FindAll(predicate));
    }
    
    public void Add(List<QuestCharacter> characters)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            Add(characters[i]);
        }
    }
    
    public void Add(QuestCharacter character)
    {
        Characters.Add(character);
    }
    
    public QuestCharacter GetFirst()
    {
        return Count > 0 ? Characters[0] : null;
    }
    
    public QuestCharacter Get(int index)
    {
        return Count > index ? Characters[index] : null;
    }
    
    public int GetInventorySize()
    {
        int output = 0;
    
        for (int i = 0; i < Count; i++)
        {
            output += Characters[i].GetInventorySize();
        }

        return output;
    }
    
    public void UpdateCharactersStatus(QuestCharacterStatusType type, int value)
    {
        for (int i = 0; i < Count; i++)
        {
            Characters[i].Statuses.UpdateStatus(type, value);
        }
    }
}

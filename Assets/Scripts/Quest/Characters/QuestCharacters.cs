using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestCharacters : MonoBehaviour
{
    private List<string> _availableNames = new List<string>()
    {
        "Хэнк",
        "Бобби",
        "Билл",
        "Дейл",
        "Лаки"
    };

    private List<QuestCharacter> _characters = new List<QuestCharacter>();

    public QuestCharacters()
    {
        AddCharacterToShelter(GetNewCharacter());
    }
    
    public QuestCharacter FindLeader()
    {
        return _characters.Find(x => x.IsDead == false && x.IsInShelter == true);
    }

    public QuestCharactersGroup FindAlive()
    {
        return FindAll(x => x.IsDead == false && x.IsInShelter == true);
    }
    
    public QuestCharactersGroup FindAliveAdventurers()
    {
        return FindAll(x => x.IsDead == false && x.IsInAdventure == true);
    }
    
    public QuestCharacter FindDead(QuestCharacterDeathReason? deathReason, QuestCharacterBurialType? burialType)
    {
        return FindDeadAll(deathReason, burialType).GetFirst();
    }
    
    public QuestCharactersGroup FindDeadAll(QuestCharacterDeathReason? deathReason, QuestCharacterBurialType? burialType)
    {
        if(deathReason != null && burialType != null)
        {
            return FindAll
            (
                x =>
                x.IsDead == true &&
                x.IsInShelter == true &&
                x.DeathReason == deathReason &&
                x.BurialType == burialType
            );
        }
        
        if(deathReason == null && burialType != null)
        {
            return FindAll
            (
                x =>
                x.IsDead == true &&
                x.IsInShelter == true &&
                x.BurialType == burialType
            );
        }

        return new QuestCharactersGroup();
    }
    
    public void AddCharacter(QuestCharacter character)
    {
        _characters.Add(character);
    }
    
    public void AddCharacterToShelter(QuestCharacter character)
    {
        character.IsInShelter = true;
        
        AddCharacter(character);
    }

    public QuestCharacter GetNewCharacter()
    {
        QuestCharacter character = new QuestCharacter();
        character.Name = GenerateName();

        return character;
    }
    
    public void ProcessHours(int value)
    {
        for (int i = 0; i < _characters.Count; i++)
        {
            _characters[i].ProcessHours(value);
        }
    }
    
    private QuestCharactersGroup FindAll(Predicate<QuestCharacter> predicate)
    {
        return new QuestCharactersGroup(_characters.FindAll(predicate));
    }
    
    private string GenerateName()
    {
        if(_availableNames.Count <= 0)
        {
            return "Безымянный";
        }
    
        string characterName = _availableNames[UnityEngine.Random.Range(0, _availableNames.Count)];

        _availableNames.Remove(characterName);
    
        return characterName;
    }
}

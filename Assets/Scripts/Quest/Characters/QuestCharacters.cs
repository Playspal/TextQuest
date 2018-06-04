using System.Collections;
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

    public List<QuestCharacter> FindAlive()
    {
        return _characters.FindAll(x => x.IsDead == false && x.IsInShelter == true);
    }
    
    public QuestCharacter Find(QuestCharacterDeathReason? deathReason, QuestCharacterBurialType? burialType)
    {
        List<QuestCharacter> questCharacter = FindAll(deathReason, burialType);
        return questCharacter.Count > 0 ? questCharacter[0] : null;
    }
    
    public List<QuestCharacter> FindAll(QuestCharacterDeathReason? deathReason, QuestCharacterBurialType? burialType)
    {
        if(deathReason != null && burialType != null)
        {
            return _characters.FindAll
            (
                x =>
                x.DeathReason == deathReason &&
                x.BurialType == burialType
            );
        }
        
        if(deathReason == null && burialType != null)
        {
            return _characters.FindAll(x => x.BurialType == burialType);
        }

        return new List<QuestCharacter>();
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
    
    private string GenerateName()
    {
        if(_availableNames.Count <= 0)
        {
            return "Безымянный";
        }
    
        string characterName = _availableNames[Random.Range(0, _availableNames.Count)];

        _availableNames.Remove(characterName);
    
        return characterName;
    }
}

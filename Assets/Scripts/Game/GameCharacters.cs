using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacters
{
    private List<GameCharacter> _characters = new List<GameCharacter>();

    public GameCharacters()
    {
        CreateCharacter();
    }

    public void CreateCharacter()
    {
        GameCharacter character = new GameCharacter();
        _characters.Add(character);
    }
    
    public void Update()
    {
        for (int i = 0; i < _characters.Count; i++)
        {
            _characters[i].Update();
        }
    }
}
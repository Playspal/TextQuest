using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    public GameCharacters GameCharacters;
    
    public Game()
    {
        GameCharacters = new GameCharacters();
    }
    
    public void Update()
    {
        GameCharacters.Update();
    }
}

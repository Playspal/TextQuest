using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityExpansion.UI;

public class UiCharacterStatus : UiObject
{    
    [SerializeField]
    private Text _health;

    [SerializeField]
    private Text _hunger;
    
    [SerializeField]
    private Text _thirst;

    public void SetData(QuestCharacter character)
    {
        _health.text = character.Health + "%";
        _hunger.text = (100 - character.Hunger) + "%";
        _thirst.text = (100 - character.Thirst) + "%";
    }
}

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
        _health.text = character.StatusHealth.Value + "%";
        _hunger.text = character.StatusFood.Value + "%";
        _thirst.text = character.StatusWater.Value + "%";
    }
}

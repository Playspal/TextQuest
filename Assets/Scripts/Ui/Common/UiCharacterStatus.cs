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

    [SerializeField]
    private Text _stamina;

    [SerializeField]
    private Text _madness;

    public void SetData(QuestCharacter character)
    {
        _health.text = character.Statuses.Health.Value + "%";
        _hunger.text = character.Statuses.Food.Value + "%";
        _thirst.text = character.Statuses.Water.Value + "%";
        _stamina.text = character.Statuses.Stamina.Value + "%";
        _madness.text = character.Statuses.Madness.Value + "%";
    }
}

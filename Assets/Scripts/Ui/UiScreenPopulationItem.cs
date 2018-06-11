using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityExpansion.UI;

public class UiScreenPopulationItem : UiVerticalListItem
{
    [SerializeField]
    private Text _name;
    
    [SerializeField]
    private UiCharacterStatus _status;

    [SerializeField]
    private Text _effects;
    
    public void SetData(QuestCharacter questCharacter)
    {
        _status.SetData(questCharacter);
        _name.text = questCharacter.Name;
        
        List<string> effects = new List<string>();

        if(questCharacter.Effects.Contains(QuestCharacterEffectType.Starvation))
        {
            effects.Add("Голод");
        }
        
        if(questCharacter.Effects.Contains(QuestCharacterEffectType.Thirst))
        {
            effects.Add("Жажда");
        }
        
        if(questCharacter.Effects.Contains(QuestCharacterEffectType.Debilitation))
        {
            effects.Add("Изнеможение");
        }

        string effectsString = effects.Count == 0 ? "Нет активных эффектов" : string.Empty;

        for (int i = 0; i < effects.Count; i++)
        {
            effectsString += string.IsNullOrEmpty(effectsString) ? "" : "\n";
            effectsString += effects[i];
        }

        _effects.text = questCharacter.GetJobText() + "\n" + effectsString;
    }
    
    public override float GetPreferredHeight()
    {
        return Mathf.Abs(_effects.GetComponent<RectTransform>().anchoredPosition.y - _effects.preferredHeight);
    }
}

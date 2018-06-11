using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityExpansion.UI;

public class UiResorce : UiObject
{
    [SerializeField]
    private Image _icon;

    [SerializeField]
    private Text _caption;
    
    public void Setup(QuestResourceType type, int amount)
    {
        _icon.sprite = Resources.Load<Sprite>(GetAssetName(type));
        _caption.text = amount.ToLeadingZerosString(2);
    }
    
    private string GetAssetName(QuestResourceType type)
    {
        return GetSpritePathByType(type);
    }
    
    public static Sprite GetSpriteByType(QuestResourceType type)
    {
        string path = GetSpritePathByType(type);
        
        if(string.IsNullOrEmpty(path))
        {
            return null;
        }

        return Resources.Load<Sprite>(path);
    }
    
    public static string GetSpritePathByType(QuestResourceType type)
    {
        string root = "Sprites/Ui/Icons/";
        
        switch(type)
        {
            case QuestResourceType.Population:
                return root + "ui-icon-population";
                
            case QuestResourceType.Food:
                return root + "ui-icon-food";
                
            case QuestResourceType.Water:
                return root + "ui-icon-water";
                
            case QuestResourceType.Wood:
                return root + "ui-icon-wood";
                
            case QuestResourceType.Gas:
                return root + "ui-icon-gas";
        }

        return null;
    }
}

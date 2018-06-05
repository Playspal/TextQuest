using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityExpansion.UI;

public class UiScreenMapItem : UiObject
{
    [SerializeField]
    private Image _icon;

    [SerializeField]
    private Text _name;

    [SerializeField]
    private List<Image> _resources;
    
    public void SetData(QuestLocation questLocation)
    {
        X = questLocation.Coordinates.x;
        Y = questLocation.Coordinates.y;

        _name.text = questLocation.Name.ToUpper();

        for (int i = 0; i < _resources.Count; i++)
        {
            if(i < questLocation.Resources.Length)
            {
                _resources[i].gameObject.SetActive(true);
                _resources[i].sprite = UiResorce.GetSpriteByType(questLocation.Resources[i]);

                Debug.LogError("!!");
            }
            else
            {
                _resources[i].gameObject.SetActive(false);
            }
        }
    }
}

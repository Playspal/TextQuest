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
    
    public QuestLocation Location { get; private set; }

	protected override void Awake()
	{
		base.Awake();

        UiEvents.AddClickListener(gameObject, OnClick);
	}

	public void SetData(QuestLocation questLocation)
    {
        Location = questLocation;
        
        X = Location.Coordinates.x;
        Y = Location.Coordinates.y;

        if (Location.IsDiscovered)
        {
            _name.text = Location.Name.ToUpper();

            for (int i = 0; i < _resources.Count; i++)
            {
                if (i < Location.Resources.Length)
                {
                    _resources[i].gameObject.SetActive(true);
                    _resources[i].sprite = UiResorce.GetSpriteByType(Location.Resources[i]);
                }
                else
                {
                    _resources[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            _name.text = "Неизвестно";

            for (int i = 0; i < _resources.Count; i++)
            {
                _resources[i].gameObject.SetActive(false);
            }
        }
    }
    
    private void OnClick()
    {
        if(Location.IsDiscovered)
        {
        }
        else
        {
            UiPopupConfirm popup = Ui.ShowPopupConfirm();

            popup.OnOk += () => Quest.Instance.StartAdventure(Location);
            popup.SetData
            (
                "РАЗВЕДКА",
                "Эта локация еще не разведана. Отправиться на разведку?",
                "ДА",
                "НЕТ"
            );
        }
    }
}

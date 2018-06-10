using System;
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

    public Action<UiScreenMapItem> OnClick;
    public QuestLocation Location { get; private set; }

	protected override void Awake()
	{
		base.Awake();

        UiEvents.AddClickListener(gameObject, OnIconClick);
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
    
    private void OnIconClick()
    {
        OnClick.InvokeIfNotNull(this);
    
        if(Location.LocationType == QuestLocationType.Home)
        {
            // TODO: show info about home
            return;
        }

        QuestCardSystemStartAdventure card = QuestCards.GetCard<QuestCardSystemStartAdventure>() as QuestCardSystemStartAdventure;

        card.SetLocation(Location);

        Quest.Instance.AddCard(card);
        Ui.ShowScreenCards();
    }
}

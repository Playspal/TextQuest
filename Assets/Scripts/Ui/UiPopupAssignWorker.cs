using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityExpansion.UI;

public class UiPopupAssignWorker : UiLayoutElementPopup
{
    [SerializeField]
    private UiVerticalList _verticalList;

    [SerializeField]
    private Text _title;

    [SerializeField]
    private UiButton _buttonClose;

    private QuestBuilding _questBuilding;

	protected override void Awake()
	{
		base.Awake();

        _buttonClose.OnClick += Hide;
        _buttonClose.SetCaption("ЗАКРЫТЬ");

        _title.text = "НАЗНАЧИТЬ РАБОТНИКА";
	}

    public void SetBuilding(QuestBuilding building)
    {
        _questBuilding = building;
        
        Refresh();
        RefreshSelection();
    }

	protected override void ShowBegin()
    {
        base.ShowBegin();
    }

    private void Refresh()
    {
        List<QuestCharacter> characters = Quest.Instance.Status.Characters.FindAlive();
    
        _verticalList.Clear();

        for (int i = 0; i < characters.Count; i++)
        {
            UiPopupAssignWorkerItem item = _verticalList.CreateItem<UiPopupAssignWorkerItem>();
            item.SetData(characters[i]);
            item.OnClick += OnItemClick;
        }
    }
    
    private void RefreshSelection()
    {
        List<UiVerticalListItem> items = _verticalList.GetItems();

        for (int i = 0; i < items.Count; i++)
        {
            UiPopupAssignWorkerItem item = items[i] as UiPopupAssignWorkerItem;
        
            item.SetSelected(_questBuilding.Worker == item.QuestCharacter);
            item.Refresh();
        }
    }
    
    private void OnItemClick(UiPopupAssignWorkerItem item)
    {
        if (_questBuilding.Worker == item.QuestCharacter)
        {
            _questBuilding.SetWorker(null);
        }
        else
        {
            _questBuilding.SetWorker(item.QuestCharacter);
        }
        
        RefreshSelection();
    }
}

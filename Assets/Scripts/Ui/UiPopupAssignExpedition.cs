using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityExpansion.UI;

public class UiPopupAssignExpedition : UiLayoutElementPopup
{
    [SerializeField]
    private UiVerticalList _verticalList;

    [SerializeField]
    private Text _title;

    [SerializeField]
    private UiButton _buttonClose;

    [SerializeField]
    private UiButton _buttonConfirm;

    private List<QuestCharacter> _characters = new List<QuestCharacter>();
    private QuestLocation _location;

    protected override void Awake()
    {
        base.Awake();

        _buttonClose.OnClick += Hide;
        _buttonClose.SetCaption("ЗАКРЫТЬ");

        _title.text = "НАЧАТЬ ЭКСПЕДИЦИЮ";
    }
    
    public void SetLocation(QuestLocation location)
    {
        _location = location;

        Refresh();
        RefreshSelection();
    }

    protected override void ShowBegin()
    {
        base.ShowBegin();
    }

    private void Refresh()
    {
        QuestCharactersGroup characters = Quest.Instance.Status.Characters.FindAlive();
    
        _verticalList.Clear();

        for (int i = 0; i < characters.Count; i++)
        {
            UiPopupAssignExpeditionItem item = _verticalList.CreateItem<UiPopupAssignExpeditionItem>();
            item.SetData(characters.Get(i));
            item.OnClick += OnItemClick;
        }
    }
    
    private void RefreshSelection()
    {
        List<UiVerticalListItem> items = _verticalList.GetItems();

        for (int i = 0; i < items.Count; i++)
        {
            UiPopupAssignExpeditionItem item = items[i] as UiPopupAssignExpeditionItem;
        
            item.SetSelected(_characters.Contains(item.QuestCharacter));
            item.Refresh();
        }
    }
    
    private void OnItemClick(UiPopupAssignExpeditionItem item)
    {
        QuestCharacter character = item.QuestCharacter;

        if(_characters.Contains(character))
        {
            _characters.Remove(character);
        }
        else
        {
            _characters.Add(character);
        }
        
        RefreshSelection();
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityExpansion.UI;

public class UiPopupAssignExpeditionItem : UiVerticalListItem
{
    [SerializeField]
    private Text _name;

    [SerializeField]
    private UiCharacterStatus _status;

    [SerializeField]
    private GameObject _backgroundNormal;

    [SerializeField]
    private GameObject _backgroundSelected;

    public Action<UiPopupAssignExpeditionItem> OnClick;
    public QuestCharacter QuestCharacter { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        UiEvents.AddClickListener
        (
            gameObject,
            () => OnClick.InvokeIfNotNull(this)
        );
    }
    
    public void Refresh()
    {
        _name.text = QuestCharacter.Name + " (" + QuestCharacter.GetJobText() + ")";
        _status.SetData(QuestCharacter);
    }

    public void SetData(QuestCharacter character)
    {
        QuestCharacter = character;
        Refresh();
    }
    
    public void SetSelected(bool value)
    {
        _backgroundNormal.SetActive(!value);
        _backgroundSelected.SetActive(value);
    }
}

using System;

using UnityEngine;
using UnityEngine.UI;
using UnityExpansion.UI;

public class UiPopupConfirm : UiLayoutElementPopup
{
    public Action OnOk;
    public Action OnCancel;

    [SerializeField]
    private Text _title;

    [SerializeField]
    private Text _description;

    [SerializeField]
    private UiButton _buttonOk;

    [SerializeField]
    private UiButton _buttonCancel;

    public void SetData(string title, string description, string buttonOk, string buttonCancel)
    {
        _title.text = title.ToUpper();
        _description.text = description;
        
        _buttonOk.SetCaption(buttonOk.ToUpper());
        _buttonCancel.SetCaption(buttonCancel.ToUpper());
    }

	protected override void Awake()
	{
		base.Awake();

        _buttonOk.OnClick += OnButtonOkClick;
        _buttonCancel.OnClick += OnButtonCancelClick;
	}

	private void OnButtonOkClick()
    {
        OnOk.InvokeIfNotNull();
        Hide();
    }
    
    private void OnButtonCancelClick()
    {
        OnCancel.InvokeIfNotNull();
        Hide();
    }
}

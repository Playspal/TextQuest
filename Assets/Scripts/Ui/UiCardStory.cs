using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityExpansion.UI;

public class UiCardStory : UiCard
{
    public Action OnClick;
    
    [SerializeField]
    private Text _message;
    
    public void SetMessage(string value)
    {
        _message.text = value;
    }

	protected override void Awake()
	{
        UiEvents.AddClickListener(gameObject, Drop);
    }
}

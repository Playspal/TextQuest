using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityExpansion.UI;

public class ScreenCardsCardBack : MonoBehaviour
{
    public Action OnClick;
    
    [SerializeField]
    private Text _message;

    [SerializeField]
    private ScreenCardsCardBackResources _resources;

    public void SetMessage(string value)
    {
        _message.text = value;
    }
    
    public void SetResources(QuestResource[] resources)
    {
        _resources.SetResources(resources);
    }

	private void Awake()
	{
        UiEvents.AddClickListener
        (
            gameObject,
            ()=>
            {
                OnClick.InvokeIfNotNull();
            }
        );
	}

}

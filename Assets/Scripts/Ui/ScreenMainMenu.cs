using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExpansion.UI;

public class ScreenMainMenu : UiLayoutElementScreen
{
	protected override void Awake()
	{
		base.Awake();

        UiEvents.AddClickListener(gameObject, OnClick);
        
	}
    
    private void OnClick()
    {
        Hide();
        Index.Instance.StartGame();
    }
}

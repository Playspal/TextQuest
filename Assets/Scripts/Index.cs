using System;
using System.Collections.Generic;

using UnityEngine;
using UnityExpansion.UI;

public class Index : MonoBehaviour
{
    public static Index Instance;
    
    private Quest _quest;
    private Game _game;
    
	// Use this for initialization
	void Start ()
    {
        Instance = this;

        float pixelsPerUnit = 32f;
        Camera.main.orthographicSize = Screen.height / (pixelsPerUnit * 2);
        Localization.SetData(LocalizationRU.Load());
	}
	
    public void StartGame()
    {        
        _quest = new Quest();
        _game = new Game();

        Ui.ShowScreenTown();

        Quest.Instance.AddStory
        (
            "psychopocalipsis\n\nСтарого мира больше нет, все, что было раньше не имеет значения. Важно только то, что будет потом и любой наш выбор имеет последствия...",
            null
        );
    }
    
	// Update is called once per frame
	void Update ()
    {
        if(_game != null)
        {
            _game.Update();
        }
        
        if(_quest != null)
        {
            _quest.Update();
            _quest.SetPause
            (
                (_quest.Adventure == null || Ui.ScreenCards.IsActive) &&
                (
                    UiLayout.ActiveScreen != Ui.ScreenTown ||
                    Ui.ScreenCards.IsActive
                )
            );
        }
	}
}
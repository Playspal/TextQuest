using System;
using System.Collections.Generic;
using UnityEngine;
using UnityExpansion.Services;
using UnityExpansion.UI;
using UnityExpansion.UI.Animation;

public class ScreenCards : UiLayoutElementScreen
{
    [SerializeField]
    private UiCardSimple _prefabCardA;

    [SerializeField]
    private UiCardStory _prefabCardStory;
    
    [SerializeField]
    private RectTransform _container;

    private List<UiCard> _cards = new List<UiCard>();

	protected override void ShowBegin()
	{
		base.ShowBegin();

        ProcessQueue();
	}

	protected override void Update()
	{
		base.Update();

        ProcessQueue();
	}

	private void ProcessQueue()
    {
        if(Quest.Instance == null)
        {
            return;
        }

        for (int i = 0; i < Quest.Instance.Queue.Count; i++)
        {
            QuestEvent questEvent = Quest.Instance.Queue[i];
            
            if(questEvent is QuestCard)
            {
                SetupCard(questEvent as QuestCard);
            }
            
            if(questEvent is QuestStory)
            {
                SetupStory(questEvent as QuestStory);
            }
        }

        Quest.Instance.Queue = new List<QuestEvent>();
    }

	public void SetupCard(QuestCard card)
    {
        UiCardSimple cardScreen = GameObject.Instantiate<UiCardSimple>
        (
            _prefabCardA,
            _container
        );
        
        cardScreen.Rotation = UnityEngine.Random.Range(-1f, 1f);
        cardScreen.GetComponent<RectTransform>().SetAsFirstSibling();
        cardScreen.SetupCard(card);
        cardScreen.OnDrop += () =>
        {
            if (_cards.Count <= 1)
            {
                //Ui.ShowScreenTown();
                Ui.PanelFooter.Show();
            }
        };
        
        cardScreen.OnRemoved += () =>
        {
            if (_cards.Count <= 1)
            {
                Hide();
            }

            _cards.Remove(cardScreen);
        };
        
        _cards.Add(cardScreen);
    }
    
    public void SetupStory(QuestStory questStory)
    {
        SetupStory(questStory.Message, questStory.Callback);
    }
    
    public void SetupStory(string message, Action callback)
    {
        UiCardStory cardScreen = GameObject.Instantiate<UiCardStory>(_prefabCardStory, _container);
        cardScreen.Rotation = UnityEngine.Random.Range(-1f, 1f);
        cardScreen.GetComponent<RectTransform>().SetAsFirstSibling();
        cardScreen.SetMessage(message);
        cardScreen.OnClick = callback;
        cardScreen.OnDrop += () =>
        {
            if (_cards.Count <= 1)
            {
                //Ui.ShowScreenTown();
                Ui.PanelFooter.Show();
            }
        };
        
        cardScreen.OnRemoved += () =>
        {
            if (_cards.Count <= 1)
            {
                Hide();
            }

            _cards.Remove(cardScreen);
        };

        _cards.Add(cardScreen);
    }

    private void OnCardButtonClick(int index)
    {
    }
}

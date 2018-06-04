using System;
using System.Collections.Generic;
using UnityEngine;

public class UiCardSimple : UiCard
{
    [SerializeField]
    private UiCardSimpleFront _front;
    
    [SerializeField]
    private UiCardSimpleBack _back;
    
    public void SetupCard(QuestCard card)
    {
        card.OnAnswer = SetupCardAnswer;
        card.OnAnswerResources = SetupCardAnswerResources;
    
        _front.SetMessage(card.GetQuestion());
        _front.SetButton1(card.GetAction1());
        _front.SetButton2(card.GetAction2());
        
        _back.SetResources(null);
        _back.OnClick = Drop;
        
        ShowCardFront(true);
    }
    
    private void SetupCardAnswer(string value, Action callback)
    {
        _back.SetMessage(value);

        OnRemoved += callback;
        OnFlipped += () => ShowCardFront(false);
        
        Flip();
    }
    
    private void SetupCardAnswerResources(QuestResource[] resources)
    {
        _back.SetResources(resources);
    }
    
    private void ShowCardFront(bool value)
    {
        _front.gameObject.SetActive(value);
        _back.gameObject.SetActive(!value);
    }
}

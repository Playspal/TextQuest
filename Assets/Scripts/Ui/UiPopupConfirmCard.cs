using System;
using System.Collections.Generic;
using UnityEngine;

public class UiPopupConfirmCard : UiCard
{
    [SerializeField]
    private UiCardSimpleFront _front;
    
    [SerializeField]
    private UiCardSimpleBack _back;
    
    public void SetQuestion(string message, string button1, string button2, Action callback1, Action callback2)
    {
        _front.SetMessage(message);
        
        _front.SetButton1
        (
            button1,
            ()=>
            {
                callback1.InvokeIfNotNull();
            }
        );

        _front.SetButton2
        (
            button2,
            () =>
            {
                callback2.InvokeIfNotNull();
            }
        );
        
        ShowCardFront(true);
    }
    
    public void SetAnswer(string message, QuestResource[] resources, Action callback)
    {
        _back.SetMessage(message);
        _back.SetResources(resources);
        _back.OnClick = Drop;

        OnRemoved += callback;
        OnFlipped += () => ShowCardFront(false);
        
        Flip();
    }
    
    private void ShowCardFront(bool value)
    {
        _front.gameObject.SetActive(value);
        _back.gameObject.SetActive(!value);
    }
}

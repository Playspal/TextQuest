using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityExpansion.UI;

public class PopupStorytelling : UiLayoutElementPopup
{
    [SerializeField]
    private Text _message;
    
    private string[] _messages;
    private int _messagesIndex = 0;
    
    protected override void Awake()
    {
        base.Awake();

        UiEvents.AddClickListener(gameObject, NextMessage);
    }
    
    public void SetMessages(string[] value)
    {
        _messages = value;
        _messagesIndex = 0;

        ShowMessage();
    }
    

    private void NextMessage()
    {
        _messagesIndex++;
        ShowMessage();
    }
    
    private void ShowMessage()
    {
        if(_messages == null || _messagesIndex >= _messages.Length)
        {
            Hide();
        }
        else
        {
            _message.text = _messages[_messagesIndex];
        }
    }

}

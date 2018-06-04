using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityExpansion.UI;

public class ScreenCardsStory : MonoBehaviour
{
    public Action OnClick;
    
    [SerializeField]
    private Text _message;
    
    private bool _isDrop = false;

    public void SetMessage(string value)
    {
        Reset();
        
        _message.text = value;
    }

    private void Awake()
    {
        UiEvents.AddClickListener
        (
            gameObject,
            ()=>
            {
                //OnClick.InvokeIfNotNull();
                Drop();
            }
        );
    }
    
    private void Reset()
    {
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
    
    private void Drop()
    {
        _isDrop = true;
    }
    
    private void Update()
    {
        if(_isDrop)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();

            rectTransform.anchoredPosition = new Vector2
            (
                0,
                (rectTransform.anchoredPosition.y - 10) * 1.2f
            );
            
            if(rectTransform.anchoredPosition.y < -500)
            {
                _isDrop = false;
                
                OnClick.InvokeIfNotNull();
            }
        }
    }
}

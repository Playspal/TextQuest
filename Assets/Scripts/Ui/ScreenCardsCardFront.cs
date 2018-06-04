using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityExpansion.Services;
using UnityExpansion.UI;

public class ScreenCardsCardFront : MonoBehaviour
{
    public Action<int> OnClick;

    [SerializeField]
    private Text _message;

    [SerializeField]
    private UiButton _button1;

    [SerializeField]
    private UiButton _button2;

    [SerializeField]
    private RectTransform _marker;

    private Action _buttonCallback1;
    private Action _buttonCallback2;

    public void SetMessage(string value)
    {
        _message.text = value;
    }

    public void SetButton1(QuestAction action)
    {
        _button1.SetCaption(action.Message);
        _buttonCallback1 = action.Handler;
    }

    public void SetButton2(QuestAction action)
    {
        _button2.SetCaption(action.Message);
        _buttonCallback2 = action.Handler;
    }

    public void Reset()
    {
        _marker.gameObject.SetActive(false);
    }

    private void Awake()
    {
        _button1.OnClick += OnButtonClick1;
        _button2.OnClick += OnButtonClick2;

        _marker.gameObject.SetActive(false);
    }

    private void OnButtonClick1()
    {
        DeferredAction action = new DeferredAction
        (
            () =>
            {
                _buttonCallback1.InvokeIfNotNull();
                OnClick.InvokeIfNotNull(1);
            },
            0.15f,
            DeferredType.TimeBased
        );

        DrawMarker();
    }

    private void OnButtonClick2()
    {
        DeferredAction action = new DeferredAction
        (
            () =>
            {
                _buttonCallback2.InvokeIfNotNull();
                OnClick.InvokeIfNotNull(2);
            },
            0.15f,
            DeferredType.TimeBased
        );

        DrawMarker();
    }

    private void DrawMarker()
    {
        Vector2 localpoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle
        (
           GetComponent<RectTransform>(),
           Input.mousePosition,
           GetComponentInParent<Canvas>().worldCamera,
           out localpoint
        );

        _marker.gameObject.SetActive(true);
        _marker.anchoredPosition = localpoint;
    }
}
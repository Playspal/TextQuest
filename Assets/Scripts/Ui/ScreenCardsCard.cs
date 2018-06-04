using System;
using System.Collections.Generic;
using UnityEngine;
using UnityExpansion.UI;
using UnityExpansion.Tweens;

public class ScreenCardsCard : MonoBehaviour
{
    public Action OnRemoved;

    [SerializeField]
    private ScreenCardsCardBack _cardBack;

    [SerializeField]
    private ScreenCardsCardFront _cardFront;

    private bool _isFlip = false;
    private bool _isFlipOver = false;
    private float _flipTime = 0;
    
    private bool _isDrop = false;

    public void SetupCard(QuestCard card)
    {
        card.OnAnswer = SetupCardAnswer;
        card.OnAnswerResources = SetupCardAnswerResources;
    
        _cardFront.SetMessage(card.GetQuestion());
        
        _cardFront.SetButton1(card.GetAction1());
        _cardFront.SetButton2(card.GetAction2());
        
        _cardBack.SetResources(null);
    }

    public void SetupCardAnswer(string value, Action callback)
    {
        _cardBack.SetMessage(value);

        OnRemoved += callback;
        Flip();
    }
    
    private void SetupCardAnswerResources(QuestResource[] resources)
    {
        _cardBack.SetResources(resources);
    }
 
    private void Awake()
    {
        ShowCardFront(true);

        _cardFront.OnClick += OnFrontButtonClick;
        _cardBack.OnClick += Drop;
    }
    
    private void OnFrontButtonClick(int buttonIndex)
    {
        //Flip();
    }

	public void Reset()
	{
		GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        ShowCardFront(true);

        _cardFront.Reset();
	}

	private void Drop()
    {
        if(_isFlip)
        {
                //return;
        }
    
        _isDrop = true;
    }
    
    private void Flip()
    {
        _isFlip = true;
        _isFlipOver = false;
        _flipTime = 0;
    }
    
    private void ShowCardFront(bool value)
    {
        _cardFront.gameObject.SetActive(value);
        _cardBack.gameObject.SetActive(!value);
    }
    
    private float Interpolate(float p)
    {
        return Mathf.Sin(-13 * (Mathf.PI / 2.0f) * (p + 1)) * Mathf.Pow(5, -15 * p) + 1;
    }

	private void Update()
	{
        if (_isFlip)
        {
            float rotation = transform.localRotation.eulerAngles.y;
        
            _flipTime += Time.deltaTime * 0.25f;
            _flipTime *= 1.05f;
            _isFlip = _flipTime < 1;

            if (!_isFlipOver && rotation >= 80)
            {
                _isFlipOver = true;
                ShowCardFront(false);
            }

            rotation = (_isFlipOver ? 180 : 0) + 180 * Interpolate(_flipTime);
            transform.localRotation = Quaternion.Euler
            (
                transform.localRotation.eulerAngles.x, rotation,  (_isFlipOver ? (rotation - 180) : rotation) * 0.01f
            );
        }
        
        if(_isDrop)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();

            rectTransform.anchoredPosition = new Vector2
            (
                0,
                (rectTransform.anchoredPosition.y - 500 * Time.deltaTime) * 1.2f
            );
            
            if(rectTransform.anchoredPosition.y < -500)
            {
                _isDrop = false;
                
                OnRemoved.InvokeIfNotNull();
                GameObject.Destroy(this.gameObject);
            }
        }
	}
}

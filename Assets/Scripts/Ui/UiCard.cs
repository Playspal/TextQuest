using System;
using System.Collections.Generic;
using UnityEngine;
using UnityExpansion.UI;

public class UiCard : UiObject
{
    public Action OnDrop;
    public Action OnRemoved;
    public Action OnFlipped;
    
    private bool _isFlip = false;
    private bool _isFlipOver = false;
    private float _flipTime = 0;
    
    private bool _isDrop = false;

    public void Flip()
    {
        _isFlip = true;
        _isFlipOver = false;
        _flipTime = 0;
    }
    
    public void Drop()
    {
        if (!_isDrop)
        {
            OnDrop.InvokeIfNotNull();
            
            _isDrop = true;
        }
    }

	protected override void Update()
	{
		base.Update();

        UpdateFlip();
        UpdateDrop();
    }
    
    private void UpdateFlip()
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
                
                transform.localRotation = Quaternion.Euler
                (
                    transform.localRotation.eulerAngles.x,
                    transform.localRotation.eulerAngles.y,
                    UnityEngine.Random.Range(-1f, 1f)
                );
                
                OnFlipped.InvokeIfNotNull();
            }
            
            if(!_isFlipOver)
            {
                Y -= 5 * Time.deltaTime;
            }

            float easing = Mathf.Sin(-13 * (Mathf.PI / 2.0f) * (_flipTime + 1)) * Mathf.Pow(5, -15 * _flipTime) + 1;

            rotation = (_isFlipOver ? 180 : 0) + 180 * easing;
            transform.localRotation = Quaternion.Euler
            (
                transform.localRotation.eulerAngles.x,
                rotation, 
                transform.localRotation.eulerAngles.z//(_isFlipOver ? (rotation - 180) : rotation) * 0.01f
            );
        }
    }
    
    private void UpdateDrop()
    {
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

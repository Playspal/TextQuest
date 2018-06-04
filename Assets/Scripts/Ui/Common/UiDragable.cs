using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExpansion.UI;

public class UiDragable : UiObject
{
    [SerializeField]
    private bool _dragHorizontal = false;
    
    [SerializeField]
    private bool _dragVertical = false;

    [SerializeField]
    private float _minX = -500;
    
    [SerializeField]
    private float _maxX = 500;
    
    [SerializeField]
    private float _minY = -500;
    
    [SerializeField]
    private float _maxY = 500;

    private bool _isDrag = false;
    
    private float _lastX;
    private float _lastY;

    private float _speedX = 0;
    private float _speedY = 0;
    
    protected override void Awake()
    {
        base.Awake();

        UiEvents.AddMousePressListener(gameObject, OnMousePress);
        UiEvents.AddMouseReleaseListener(gameObject, OnMouseRelease);
    }

    protected override void Update()
    {
        base.Update();
        
        if(_isDrag)
        {
            float canvasScaleFactor = GetComponentInParent<Canvas>().scaleFactor;
            
            float deltaX = (Input.mousePosition.x - _lastX) / canvasScaleFactor;
            float deltaY = (Input.mousePosition.y - _lastY) / canvasScaleFactor;

            float distance = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);
            float angle = Mathf.Atan2(deltaY, deltaX) - Rotation / 180f * Mathf.PI;
            
            _speedX = Mathf.Cos(angle) * distance;
            _speedY = Mathf.Sin(angle) * distance;
            
            _lastX = Input.mousePosition.x;
            _lastY = Input.mousePosition.y;
        }

        if (_dragHorizontal)
        {
            X += _speedX;
            X = Mathf.Clamp(X, _minX, _maxX);
        }

        if (_dragVertical)
        {
            Y += _speedY;
            Y = Mathf.Clamp(Y, _minY, _maxY);
        }
        
        _speedX *= 0.95f;
        _speedY *= 0.95f;
    }
    
    private void OnMousePress()
    {
        _isDrag = true;
        
        _lastX = Input.mousePosition.x;
        _lastY = Input.mousePosition.y;
    }
    
    private void OnMouseRelease()
    {
        _isDrag = false;
    }
}

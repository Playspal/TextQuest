using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExpansion.UI;

public class UiDragableParalax : UiObject
{
    [SerializeField]
    private UiObject _layer1;

    [SerializeField]
    private float _multiplier1 = 1;
    
    [SerializeField]
    private UiObject _layer2;
    
    [SerializeField]
    private float _multiplier2 = 1;
    
    [SerializeField]
    private UiObject _layer3;
    
    [SerializeField]
    private float _multiplier3 = 1;

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
            _layer1.X += _speedX * _multiplier1;
            _layer1.X = Mathf.Clamp(_layer1.X, _minX, _maxX);
            
            _layer2.X += _speedX * _multiplier2;
            _layer2.X = Mathf.Clamp(_layer2.X, _minX, _maxX);
            
            _layer3.X += _speedX * _multiplier3;
            _layer3.X = Mathf.Clamp(_layer3.X, _minX, _maxX);
        }

        if (_dragVertical)
        {
            _layer1.Y += _speedY * _multiplier1;
            _layer1.Y = Mathf.Clamp(_layer1.Y, _minY, _maxY);
            
            _layer2.Y += _speedY * _multiplier2;
            _layer2.Y = Mathf.Clamp(_layer2.Y, _minY, _maxY);
            
            _layer3.Y += _speedY * _multiplier3;
            _layer3.Y = Mathf.Clamp(_layer3.Y, _minY, _maxY);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExpansion.UI;

public class UiCardMap : UiCard
{
    [SerializeField]
    private UiObject _container;

    private bool _isDrag = false;
    private Vector2 _mousePosition;

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
            float deltaX = Input.mousePosition.x - _mousePosition.x;
            float deltaY = Input.mousePosition.y - _mousePosition.y;

            deltaX /= GetComponentInParent<Canvas>().scaleFactor;
            deltaY /= GetComponentInParent<Canvas>().scaleFactor;

            float distance = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);
            float angle = Mathf.Atan2(deltaY, deltaX) - (transform.localRotation.eulerAngles.z ) / 180f * Mathf.PI;
            
            
            _speedX = Mathf.Cos(angle) * distance;
            _speedY = Mathf.Sin(angle) * distance;
                        
            _mousePosition = Input.mousePosition;
        }
        
        _container.X += _speedX * 0.5f;
        _container.Y += _speedY * 0.5f;
        
        _speedX *= 0.95f;
        _speedY *= 0.95f;
	}
    
    private void OnMousePress()
    {
        _isDrag = true;
        _mousePosition = Input.mousePosition;
    }
    
    private void OnMouseRelease()
    {
        _isDrag = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExpansion.UI;

public class UiVerticalList : MonoBehaviour
{
    [SerializeField]
    private UiVerticalListItem _prefab;

    [SerializeField]
    private UiObject _mask;

    [SerializeField]
    private UiObject _container;

    [SerializeField]
    private float _spacing = 10;

    [SerializeField]
    private float _paddingTop = 50;
    
    [SerializeField]
    private float _paddingBottom = 50;
    
    private List<UiVerticalListItem> _items = new List<UiVerticalListItem>();
    
    private bool _isDrag = false;
    private float _lastY;
    private float _speedY = 0;

    private bool _needToResize = true;
    
    public List<UiVerticalListItem> GetItems()
    {
        return _items;// as List<T>;
    }
    
    public T CreateItem<T>() where T : UiVerticalListItem
    {
        T item = UiObject.Instantiate<T>(_prefab.gameObject, _container.RectTransform);

        AddItem(item);
        Resize();

        return item;
    }

    public void Clear()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].Destroy();
        }

        _items = new List<UiVerticalListItem>();
    }

	private void OnEnable()
	{
        OnMouseRelease();
	}

	private void Update()
	{        
        if(_needToResize)
        {
            _needToResize = false;
            Resize();
        }
        
        if(_container.Height < _mask.Height)
        {
            _container.Y = 0;
        }
        else
        {
            if(_isDrag)
            {
                float canvasScaleFactor = GetComponentInParent<Canvas>().scaleFactor;
                float deltaX = (Input.mousePosition.y - _lastY) / canvasScaleFactor;
    
                float distance = deltaX;
                
                _speedY = distance;
                _lastY = Input.mousePosition.y;
            }

            _container.Y += _speedY;
            _container.Y = Mathf.Clamp(_container.Y, 0, _container.Height - _mask.Height);
            
            _speedY *= 0.95f;
        }
        
        if(Input.GetMouseButtonDown(0))
        {
            OnMousePress();
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            OnMouseRelease();
        }
	}

	private void AddItem(UiVerticalListItem item)
    {
        _needToResize = true;
        _items.Add(item);
    }
    
    public void Resize()
    {
        float position = _paddingTop;
        
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].Y = -position;
            
            position += _items[i].GetPreferredHeight();
            position += _spacing;
        }

        _container.Height = position + _paddingBottom;
    }
    
    private void OnMousePress()
    {
        _isDrag = true;
        _lastY = Input.mousePosition.y;
    }
    
    private void OnMouseRelease()
    {
        _isDrag = false;
    }
}

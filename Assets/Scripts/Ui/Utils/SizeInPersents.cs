using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SizeInPersents : MonoBehaviour
{
    [SerializeField]
    private float _paddingLeft;
    
    [SerializeField]
    private float _paddingRight;
    
    [SerializeField]
    private float _paddingTop;
    
    [SerializeField]
    private float _paddingBottom;

    private RectTransform _rectTransform;
    private RectTransform _parent;

	private void Awake()
	{
        _rectTransform = GetComponent<RectTransform>();
        _parent = _rectTransform.parent.GetComponent<RectTransform>();
	}

	// Update is called once per frame
	private void Update()
    {
        float width = _parent.rect.width;
        float height = _parent.rect.height;
    
        _rectTransform.anchorMin = new Vector2(0, 0);
        _rectTransform.anchorMax = new Vector2(1, 1);
        _rectTransform.pivot = new Vector2(0.5f, 0.5f);
        _rectTransform.offsetMin = new Vector2(width * _paddingLeft, height * _paddingBottom);
        _rectTransform.offsetMax = new Vector2(-width * _paddingRight, -height * _paddingTop);
	}
}

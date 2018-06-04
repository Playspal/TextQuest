using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExpansion.UI;

public class UiScreenTown : UiLayoutElementScreen
{
    [SerializeField]
    private UiScreenTownItem _prefabItem;

    [SerializeField]
    private UiObject _container;

    private List<UiScreenTownItem> _items = new List<UiScreenTownItem>();

    protected override void Awake()
    {
        base.Awake();

        
    }

	protected override void Update()
	{
		base.Update();
	}

	protected override void ShowBegin()
	{
		base.ShowBegin();
        SetupItems();
	}

	private void SetupItems()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].Destroy();
        }

        _items = new List<UiScreenTownItem>();
    
        for (int i = 0; i < Quest.Instance.Status.Buildings.Buildings.Count; i++)
        {
            UiScreenTownItem item = SetupItem();
            item.X = (item.Width + 10) * i;
            item.SetData(Quest.Instance.Status.Buildings.Buildings[i]);

            _items.Add(item);
        }
    }
    
    private UiScreenTownItem SetupItem()
    {
        UiScreenTownItem item = UiObject.Instantiate<UiScreenTownItem>
        (
            _prefabItem.gameObject,
            _container.RectTransform
        );

        _items.Add(item);

        return item;
    }
}

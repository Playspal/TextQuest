using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExpansion.UI;

public class UiScreenMap : UiLayoutElementScreen
{
    [SerializeField]
    private UiScreenMapItem _prefab;

    [SerializeField]
    private UiObject _container;

    private List<UiScreenMapItem> _items = new List<UiScreenMapItem>();

	protected override void ShowBegin()
	{
		base.ShowBegin();

        Refresh();
	}

	private void Refresh()
    {
        Clear();

        List<QuestLocation> locations = Quest.Instance.Status.Locations.Locations;

        for (int i = 0; i < locations.Count; i++)
        {
            UiScreenMapItem item = UiObject.Instantiate<UiScreenMapItem>(_prefab, _container.RectTransform);
            item.SetData(locations[i]);

            _items.Add(item);
        }
    }
    
    private void Clear()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].Destroy();
        }

        _items = new List<UiScreenMapItem>();
    }
}

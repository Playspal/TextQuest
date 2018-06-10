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
    
    [SerializeField]
    private UiObject _containerItems;

    [SerializeField]
    private UiObject _marker;

    private List<UiScreenMapItem> _items = new List<UiScreenMapItem>();

	protected override void ShowBegin()
	{
		base.ShowBegin();

        Refresh();
	}

	public void Refresh()
    {
        Clear();

        List<QuestLocation> locations = Quest.Instance.Status.Locations.Locations;

        for (int i = 0; i < locations.Count; i++)
        {
            UiScreenMapItem item = UiObject.Instantiate<UiScreenMapItem>(_prefab, _containerItems.RectTransform);
            item.SetData(locations[i]);
            item.OnClick += OnItemClick;

            _items.Add(item);
        }
    }
    
    private void OnItemClick(UiScreenMapItem item)
    {
        MoveToHome();
    }
    
    private void Clear()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].Destroy();
        }

        _items = new List<UiScreenMapItem>();
    }
    
    private void MoveToHome()
    {
        Debug.LogError("!!! 1");
        for (int i = 0; i < _items.Count; i++)
        {
            if(_items[i].Location.LocationType == QuestLocationType.Home)
            {
                _container.X = -_items[i].X;
                _container.Y = -_items[i].Y;
            }
        }
    }

	protected override void Update()
	{
		base.Update();

        QuestAdventure adventure = Quest.Instance.Adventure;
        
        if(adventure != null)
        {
            _marker.SetActive(true);

            float deltax = adventure.To.Coordinates.x - adventure.From.Coordinates.x;
            float deltay = adventure.To.Coordinates.y - adventure.From.Coordinates.y;
            float angle = Mathf.Atan2(deltay, deltax);
            float length = Mathf.Sqrt(deltax * deltax + deltay * deltay);
            float m = length * (1 - adventure.TimeleftNormalized);

            angle = 0 * Mathf.PI / 180 + angle;
            
            _marker.X = adventure.From.Coordinates.x + Mathf.Cos(angle) * m;
            _marker.Y = adventure.From.Coordinates.y + Mathf.Sin(angle) * m;

            _container.X = -_marker.X;
            _container.Y = -_marker.Y;
        }
        else
        {
            //_marker.SetActive(false);
        }
	}
}

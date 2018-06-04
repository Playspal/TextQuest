using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCardsCardBackResources : MonoBehaviour
{
    [SerializeField]
    private List<UiResorce> _items = new List<UiResorce>();
    
    public void SetResources(QuestResource[] resources)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if(resources != null && i < resources.Length)
            {
                _items[i].Setup(resources[i].Type, resources[i].Value);
                _items[i].SetActive(true);
            }
            else
            {
                _items[i].SetActive(false);
            }
        }
    }
}
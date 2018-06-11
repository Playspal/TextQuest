using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExpansion.UI;

public class UiScreenPopulation : UiLayoutElementScreen
{
    [SerializeField]
    private UiVerticalList _verticalList;

	protected override void ShowBegin()
	{
		base.ShowBegin();
        Refresh();
	}

	private void Refresh()
    {
        QuestCharactersGroup characters = Quest.Instance.Status.Characters.FindAlive();
    
        _verticalList.Clear();

        for (int i = 0; i < characters.Count; i++)
        {
            UiScreenPopulationItem item = _verticalList.CreateItem<UiScreenPopulationItem>();
            item.SetData(characters.Get(i));
        }
    }
}

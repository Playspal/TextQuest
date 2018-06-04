using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPanelHeaderResources : MonoBehaviour
{
    [SerializeField]
    private Text _population;

    [SerializeField]
    private Text _food;

    [SerializeField]
    private Text _water;

    [SerializeField]
    private Text _wood;

    [SerializeField]
    private Text _gas;

	private void Update()
	{
        if (Quest.Instance != null)
        {
            QuestResources resources = Quest.Instance.Status.Resources;

            _population.text = Quest.Instance.Status.Characters.FindAlive().Count.ToLeadingZerosString(2);
            _food.text = resources.Food.Value.ToLeadingZerosString(2);
            _water.text = resources.Water.Value.ToLeadingZerosString(2);
            _wood.text = resources.Wood.Value.ToLeadingZerosString(2);
            _gas.text = resources.Gas.Value.ToLeadingZerosString(2);
        }
	}
}

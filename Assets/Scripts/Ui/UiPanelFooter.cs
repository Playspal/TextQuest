using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExpansion.UI;

public class UiPanelFooter : UiLayoutElementPanel
{
    [SerializeField]
    private UiButton _buttonMap;

    [SerializeField]
    private UiButton _buttonTown;

    [SerializeField]
    private UiButton _buttonPopulation;

    [SerializeField]
    private UiButton _buttonNotification;

	protected override void Awake()
	{
		base.Awake();

        _buttonMap.OnClick += Ui.ShowScreenMap;
        _buttonTown.OnClick += Ui.ShowScreenTown;
        _buttonNotification.OnClick += Ui.ShowScreenCards;
        _buttonPopulation.OnClick += Ui.ShowScreenPopulation;
	}

	protected override void Update()
	{
		base.Update();

        if (Quest.Instance != null)
        {
            bool haveNotification = Quest.Instance.Queue.Count > 0;

            _buttonMap.SetActive(!haveNotification);
            _buttonNotification.SetActive(haveNotification);
        }
	}
}

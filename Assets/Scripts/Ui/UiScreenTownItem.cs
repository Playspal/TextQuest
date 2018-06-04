using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityExpansion.UI;

public class UiScreenTownItem : UiObject
{
    [SerializeField]
    private UiResources _resourcesRequiredToBuild;

    [SerializeField]
    private GameObject _panelBuild;

    [SerializeField]
    private GameObject _panelBuildInProgress;

    [SerializeField]
    private GameObject _panelBuilded;

    [SerializeField]
    private Text _buildMessage;
    
    [SerializeField]
    private Text _buildInProgressMessage;

    [SerializeField]
    private Text _buildInProgressTimer;

    [SerializeField]
    private Text _buildedMessage;

    private QuestBuilding _building;
    
    public void SetData(QuestBuilding building)
    {
        _building = building;
        _resourcesRequiredToBuild.SetResources(building.Cost.ToArray());
    }

	protected override void Awake()
	{
		base.Awake();

        UiEvents.AddClickListener(gameObject, OnClick);
	}

	protected override void Start()
	{
		base.Start();
        
        Update();
	}

	protected override void Update()
	{
		base.Update();

        _buildMessage.text = _building.Name;
        _buildInProgressMessage.text = "Construction...";
        _buildInProgressTimer.text = _building.GetConstructionTimeleft().ToString();
        _buildedMessage.text = _building.Name;

        switch(_building.State)
        {
            case QuestBuildingState.NotBuilded:
                SetActivePanel(_panelBuild);
                break;
                
            case QuestBuildingState.ConstructionInProgress:
                SetActivePanel(_panelBuildInProgress);
                break;
                
            case QuestBuildingState.Builded:
                SetActivePanel(_panelBuilded);
                break;
        }
	}

	private void OnClick()
    {
        switch(_building.State)
        {
            case QuestBuildingState.NotBuilded:
                ConstructRequest();
                break;
                
            case QuestBuildingState.ConstructionInProgress:
                Ui.ShowPopupAssignWorker(_building);
                break;
                
            case QuestBuildingState.Builded:
                Ui.ShowPopupAssignWorker(_building);
                break;
        }
    }
    
    private void SetActivePanel(GameObject target)
    {
        _panelBuild.SetActive(target == _panelBuild);
        _panelBuildInProgress.SetActive(target == _panelBuildInProgress);
        _panelBuilded.SetActive(target == _panelBuilded);
    }
    
    private void ConstructRequest()
    {
        UiPopupConfirm popupConfirm = Ui.ShowPopupConfirm();
        popupConfirm.SetData
        (
            _building.Name,
            _building.Description,
            _building.ActionNameConstruct,
            LocalizationKeys.CommonButtonCancel.Get()
        );

        popupConfirm.OnOk += () =>
        {
            if (Quest.Instance.Status.Resources.IsEnough(_building.Cost))
            {
                Ui.ShowPopupAssignWorker(_building);
                _building.ConstructionStart();
            }
            else
            {
                Ui.ShowPopupNotEnoughResources(LocalizationKeys.BuildingCommonNotEnoughResourcesToConstruct.Get());
            }
        };
    }
}

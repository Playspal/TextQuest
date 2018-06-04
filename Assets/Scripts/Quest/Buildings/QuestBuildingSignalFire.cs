using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBuildingSignalFire : QuestBuilding
{
    private int _hp = 100;
    private int _hpMax = 100;
    
    public QuestBuildingSignalFire()
    {
        BuildingType = QuestBuildingType.SignalFire;
    
        Name = LocalizationKeys.BuildingSignalFireName.Get();
        Description = LocalizationKeys.BuildingSignalFireDescription.Get();
        DescriptionJob = LocalizationKeys.BuildingSignalFireDescriptionJob.Get();
        ActionNameConstruct = LocalizationKeys.BuildingSignalFireActionNameConstruct.Get();

        Cost = new List<QuestResource>()
        {
            new QuestResource(QuestResourceType.Wood, 1)
        };

        _constructionDuration = 120;
    }

	public override void ConstructionStart()
	{
		base.ConstructionStart();

        _hp = _hpMax;
	}

	public override void ProcessMinutes(int value)
	{
		base.ProcessMinutes(value);

        if (State == QuestBuildingState.Builded)
        {
            if (Quest.Instance.Status.Weather.Weather == QuestWeather.Type.Rain)
            {
                if (Worker == null)
                {
                    _hp -= value * 2;

                    if (_hp <= 0)
                    {
                        Quest.Instance.AddStory(LocalizationKeys.BuildingSignalFireDeconstructedByRain.Get(), null);
                        Deconstruct();
                    }
                }
            }
            else
            {
                if (Worker == null)
                {
                    _hp -= value;

                    if (_hp <= 0)
                    {
                        Quest.Instance.AddStory(LocalizationKeys.BuildingSignalFireDeconstructedByTime.Get(), null);
                        Deconstruct();
                    }
                }
                else
                {
                    _hp = _hpMax;
                }
            }
        }
	}
}

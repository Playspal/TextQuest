using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCharacterStatusWater : QuestCharacterStatus
{
    // How much water will be added by rain per iteration
    private const int RefillByRain = 5;

    // How much water will be added by one water resource
    private const int RefillByResourceWater = 25;
    
    // How much thirst will be increased per iteration
    private const int SpendPerIteration = 6;

    // Try to refill after value will be lower than this
    private const int Threshold = 75;
    
    public QuestCharacterStatusWater(int value) : base(value)
    {
    }
    
	public override void Process()
	{
		base.Process();
        
        if(Quest.Instance.Status.Weather.Weather == QuestWeather.Type.Rain)
        {
            Value += RefillByRain;
        }
        else
        {
            Value -= SpendPerIteration;
        
            if(Value <= Threshold)
            {
                RefillUsingResource();
            }
        }
	}

	public override void RefillUsingResource()
	{
		base.RefillUsingResource();
        
        if(Quest.Instance.Status.Resources.Water.Update(-1))
        {
            Value += RefillByResourceWater;
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCharacterStatusFood : QuestCharacterStatus
{
    // How much food will be added by one water resource
    private const int RefillByResourceFood = 50;
    
    // How much water will be added per iteration
    private const int SpendPerIteration = 3;

    // Try to refill after value will be lower than this
    private const int Threshold = 50;
    
    public QuestCharacterStatusFood(int value) : base(value)
    {
        StatusType = QuestCharacterStatusType.Food;
    }

	public override void Process()
	{
		base.Process();
        
        Value -= SpendPerIteration;

        if (Value < Threshold)
        {
            RefillUsingResource();
        }
	}

	public override void RefillUsingResource()
	{
        if(!_refillEnabled)
        {
            return;
        }
    
		base.RefillUsingResource();
        
        if(Quest.Instance.Status.Resources.Food.Update(-1))
        {
            Value += RefillByResourceFood;
        }
	}
}

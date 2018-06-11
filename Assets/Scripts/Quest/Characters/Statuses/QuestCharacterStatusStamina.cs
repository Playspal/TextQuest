using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCharacterStatusStamina : QuestCharacterStatus
{
    public QuestCharacterStatusStamina(int value) : base(value)
    {
        StatusType = QuestCharacterStatusType.Stamina;
    }
    
    public override void Process()
    {
        base.Process();

        Value -= 1;
    }
}

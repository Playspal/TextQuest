using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCharacterStatusMadness : QuestCharacterStatus
{
    public QuestCharacterStatusMadness(int value) : base(value)
    {
        StatusType = QuestCharacterStatusType.Madness;
    }
}

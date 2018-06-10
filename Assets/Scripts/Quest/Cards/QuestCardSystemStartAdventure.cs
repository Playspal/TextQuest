using System.Collections.Generic;

public class QuestCardSystemStartAdventure : QuestCard
{
    private QuestLocation _location;
    
    public void SetLocation(QuestLocation location)
    {
        _location = location;
    }

	public override QuestCardType GetCardType()
	{
        return QuestCardType.System;
	}

	public override bool IsReadyToUse()
    {
        return false;
    }

    public override float GetCooldown()
    {
        return 0;
    }
    
    public override string GetQuestion()
    {
        if(_location.IsDiscovered)
        {
            return _location.Name + "... Возможно там еще остались какие-то ценные ресурсы.";
        }
        
        return "Примерно в " + _location.GetDistance(true) + " километрах от сюда есть неисследованная локация. Возможно там есть что-то ценное";
    }

	public override QuestAction GetAction1()
	{
		return new QuestAction
        (
            "Отправиться одному",
            ()=>
            {
                if(UpdateResource(QuestResourceType.Water, -_location.GetRequiredWater(1)))
                {
                    Quest.Instance.StartAdventure
                    (
                        _location,
                        new List<QuestCharacter>()
                        {
                            Quest.Instance.Status.Characters.FindLeader()
                        }
                    );
                    End("Я пошел один");
                }
                else
                {
                    End("Не хватает воды");
                }
            }
        );
	}

	public override QuestAction GetAction2()
	{
        return new QuestAction
        (
            "Отправиться всей группой",
            ()=>
            {
                if(UpdateResource(QuestResourceType.Water, -_location.GetRequiredWater(Quest.Instance.Status.Characters.FindAlive().Count)))
                {
                    Quest.Instance.StartAdventure
                    (
                        _location,
                        Quest.Instance.Status.Characters.FindAlive()
                    );
                    End("Мы пошли всей кодлой");
                }
                else
                {
                    End("Не хватает воды");
                }
            }
        );
	}
}
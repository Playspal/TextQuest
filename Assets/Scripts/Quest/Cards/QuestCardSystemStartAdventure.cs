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
            "Отправить экспедицию",
            ()=>
            {
                QuestCharactersGroup groupAlive = Quest.Instance.Status.Characters.FindAlive();
                QuestCharactersGroup groupReadyForAdventure = groupAlive.FindAllWhoReadyForAdventure();

                if (groupReadyForAdventure.Count < groupAlive.Count)
                {
                        QuestCardSystemStartAdventureConfirm01 card = QuestCards.GetCard<QuestCardSystemStartAdventureConfirm01>() as QuestCardSystemStartAdventureConfirm01;
                        card.SetLocation(_location);
                        
                        End(null, card);
                }
                else
                {
                    if (UpdateResource(QuestResourceType.Water, -_location.GetRequiredWater(groupAlive.Count)))
                    {
                        Quest.Instance.StartAdventure(_location, groupAlive);
                        End("Экспедиция отправилась в путь. Если все сложится удачно, мы сможем принести " + groupAlive.GetInventorySize() + " ресурсов.");
                    }
                    else
                    {
                        End("Не хватает воды");
                    }
                }
            }
        );
	}
    
    public override QuestAction GetAction2()
    {
        return new QuestAction
        (
            "Остаться в лагере",
            ()=>
            {
                End("У нас еще много дел в лагере");
            }
        );
    }
}
using System.Collections.Generic;

public class QuestCardSystemStartAdventureConfirm01 : QuestCard
{
    private QuestLocation _location;
    
    public void SetLocation(QuestLocation location)
    {
        _location = location;
    }

	public override QuestCardType GetCardType()
	{
        return QuestCardType.StartAdventure;
	}

	public override bool IsReadyToUse()
    {
        QuestCharactersGroup groupAlive = Quest.Instance.Status.Characters.FindAlive();
        QuestCharactersGroup groupReadyForAdventure = groupAlive.FindAllWhoReadyForAdventure();

        return groupReadyForAdventure.Count < groupAlive.Count;
    }

    public override float GetCooldown()
    {
        return 0;
    }
    
    public override string GetQuestion()
    {
        QuestCharactersGroup groupAlive = Quest.Instance.Status.Characters.FindAlive();
        QuestCharactersGroup groupReadyForAdventure = groupAlive.FindAllWhoReadyForAdventure();

        if (groupReadyForAdventure.Count == 0)
        {
            return "Все люди очень устали и просят не брать их в экспедицию";
        }
        else
        {
            return "Некоторые люди очень устали и просят не брать их в экспедицию";
        }
    }

	public override QuestAction GetAction1()
	{
        return new QuestAction
        (
            "Отправить всех",
            ()=>
            {            
                QuestCharactersGroup groupAlive = Quest.Instance.Status.Characters.FindAlive();

                if (UpdateResource(QuestResourceType.Water, -_location.GetRequiredWater(groupAlive.Count)))
                {
                    Quest.Instance.StartAdventure(_location, groupAlive);
                    End("В экспедиции важна каждая пара рук - чем больше людей, тем больше ресурсов мы сможем принести в лагерь. Если все сложится удачно, мы сможем принести " + groupAlive.GetInventorySize() + " ресурсов.");
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
        QuestCharactersGroup groupAlive = Quest.Instance.Status.Characters.FindAlive();
        QuestCharactersGroup groupReadyForAdventure = groupAlive.FindAllWhoReadyForAdventure();

        if (groupReadyForAdventure.Count == 0)
        {
            return new QuestAction
            (
                "Отменить экспедицию",
                () =>
                {
                    End("Мы отправим экспедицию после того, как люди отдохнут");
                }
            );
        }
        else
        {
            return new QuestAction
            (
                "Разрешить им остаться в лагере",
                () =>
                {
                    if (UpdateResource(QuestResourceType.Water, -_location.GetRequiredWater(groupReadyForAdventure.Count)))
                    {
                        Quest.Instance.StartAdventure(_location, groupReadyForAdventure);
                        End("Пусть уставшие отдыхают в лагере, но в случае нападения у нас будет меньше шансов на победу. Если все сложится удачно, мы сможем принести " + groupReadyForAdventure.GetInventorySize() + " ресурсов.");
                    }
                    else
                    {
                        End("Не хватает воды");
                    }
                }
            );
        }
    }
}
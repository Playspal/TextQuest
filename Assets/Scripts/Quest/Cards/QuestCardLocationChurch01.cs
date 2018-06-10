public class QuestCardLocationChurch01 : QuestCard
{
	public override QuestCardType GetCardType()
	{
        return QuestCardType.IsInLocation;
	}

	public override bool IsReadyToUse()
    {
        return
        (
            Quest.Instance.Status.CurrentLocationType == QuestLocationType.Church
        );
    }

    public override float GetCooldown()
    {
        return 1;
    }
    
    public override string GetQuestion()
    {
        return "ввв";
    }

	public override QuestAction GetAction1()
	{
		return new QuestAction
        (
            "Кнопка 1",
            ()=>
            {
                UpdateTime(6);
                UpdateResource(QuestResourceType.Food, -1);

                End("Ответ 1");
            }
        );
	}

	public override QuestAction GetAction2()
	{
        return new QuestAction
        (
            "Кнопка 2",
            ()=>
            {
                UpdateTime(6);
                UpdateResource(QuestResourceType.Food, -1);

                End("Ответ 2");
            }
        );
	}
}
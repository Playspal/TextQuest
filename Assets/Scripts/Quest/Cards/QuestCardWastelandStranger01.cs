public class QuestCardWastelandStranger01 : QuestCard
{
    public override bool IsReadyToUse()
    {
        return
        (
            true
         );
    }

	public override QuestCardType GetCardType()
	{
        return QuestCardType.IsInWasteland;
	}

	public override float GetCooldown()
    {
        return 3;
    }
    
    public override string GetQuestion()
    {
        return "По пути мы встретили незнакомца. Он сказал, что его зовут Х и он хочет присоединиться к нашему лагерю.";
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
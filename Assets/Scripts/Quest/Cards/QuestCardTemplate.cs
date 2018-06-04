public class QuestCardTemplate : QuestCard
{
    public override bool IsReadyToUse()
    {
        return
        (
            Quest.Instance.Status.Characters.FindAlive().Count == 1
         );
    }

    public override float GetCooldown()
    {
        return 0;
    }
    
    public override string GetQuestion()
    {
        return "Вопрос";
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
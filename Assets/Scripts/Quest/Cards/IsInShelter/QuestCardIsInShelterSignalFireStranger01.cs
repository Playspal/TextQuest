public class QuestCardIsInShelterSignalFireStranger01 : QuestCard
{
    private QuestCharacter _character;

	public override QuestCardType GetCardType()
	{
        return QuestCardType.IsInShelter;
	}

	public override bool IsReadyToUse()
    {
        return
        (
            Quest.Instance.Status.Buildings.IsBuilded(QuestBuildingType.SignalFire)
         );
    }

    public override float GetCooldown()
    {
        return 3;
    }
    
    public override string GetQuestion()
    {
        _character = Quest.Instance.Status.Characters.GetNewCharacter();
    
        return "Привет, меня зовут " + _character.Name + ". Я увидел дым от вашего костра. Можно присоединиться к вашей группе?";
    }

	public override QuestAction GetAction1()
	{
		return new QuestAction
        (
            "Да, нам очень нужны новые люди",
            ()=>
            {
                UpdateTime(1);
                UpdateResource(QuestResourceType.Population, +1);
                UpdateResource(QuestResourceType.Food, -1);

                Quest.Instance.Status.Characters.AddCharacterToShelter(_character);

                End("" + _character.Name + " присоединился к нашей группе");
            }
        );
	}

	public override QuestAction GetAction2()
	{
        return new QuestAction
        (
            "Прости, приятель, но сейчас это не возможно",
            ()=>
            {
                UpdateTime(1);
                UpdateResource(QuestResourceType.Food, -1);

                End("" + _character.Name + " молча ушел.");
            }
        );
	}
}
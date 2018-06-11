using System.Collections.Generic;

public class QuestCardLocationChurch01 : QuestCard
{
    private QuestLocation _location;

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

	public override void Begin()
	{
		_location = Quest.Instance.Status.CurrentLocation;
	}

	public override float GetCooldown()
    {
        return 10;
    }
    
    public override string GetQuestion()
    {
        return "В церкви мы обнаружили небольшую группу людей во главе со священником. Священник проводил проповедь размахивая руками и громко выкрикивая слова";
    }

	public override QuestAction GetAction1()
	{
		return new QuestAction
        (
            "Послушать проповедь",
            ()=>
            {
                Quest.Instance.Status.Characters.FindAliveAdventurers().UpdateCharactersStatus(QuestCharacterStatusType.Madness, -10);
            
                UpdateTime(2);

                End("Проповедь длилась несколько часов. После того, как она закончилась люди стали расходиться не обращая на нас внимания");
                
                _location.GetLoot("После того, как все ушли, мы обыскали церковный подвал и нашли немного ресурсов");
            }
        );
	}

	public override QuestAction GetAction2()
	{
        return new QuestAction
        (
            "Уйти",
            ()=>
            {
                UpdateTime(0);

                End("Мы решили не связываться с религиозными фанатиками и вернуться в лагерь");
            }
        );
	}
}
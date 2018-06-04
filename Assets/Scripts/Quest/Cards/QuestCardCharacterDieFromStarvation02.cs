using System.Collections.Generic;

public class QuestCardCharacterDieFromStarvation01 : QuestCard
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
            Quest.Instance.Status.Characters.Find(null, QuestCharacterBurialType.LeftInsideOfShelter) == null &&
            Quest.Instance.Status.Characters.Find(null, QuestCharacterBurialType.LeftOutsideOfShelter) == null &&
            Quest.Instance.Status.Characters.Find(QuestCharacterDeathReason.Starvation, QuestCharacterBurialType.None) != null &&
            Quest.Instance.Status.Resources.Wood.Value <= 0 &&
            Quest.Instance.Status.Resources.Gas.Value <= 0
        );
    }

    public override float GetCooldown()
    {
        return 0;
    }
    
    public override string GetQuestion()
    {
        _character = Quest.Instance.Status.Characters.Find
        (
            QuestCharacterDeathReason.Starvation,
            QuestCharacterBurialType.None
        );
    
        return _character.Name + " умер от голода. Нужно что-то сделать с телом, но нет ресурсов для похорон и кремации.";
    }

	public override QuestAction GetAction1()
	{
		return new QuestAction
        (
            "Бросить тело неподалеку от лагеря",
            ()=>
            {
                UpdateTime(6);
                _character.SetBurialType(QuestCharacterBurialType.LeftOutsideOfShelter);

                End("Я бросил тело в небольшую яму рядом с лагерем. ");
            }
        );
	}

	public override QuestAction GetAction2()
	{
        return new QuestAction
        (
            "Оставить тело в лагере",
            ()=>
            {
                UpdateTime(1);
                _character.SetBurialType(QuestCharacterBurialType.LeftInsideOfShelter);
                
                End("Я накрыл тело листьями. Позже мы тебя обязательно похороним, " + _character.Name);
            }
        );
	}
}
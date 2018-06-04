using System.Collections.Generic;

public class QuestCardCharacterDieFromStarvation03 : QuestCard
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
            Quest.Instance.Status.Characters.Find(QuestCharacterDeathReason.Starvation, QuestCharacterBurialType.None) != null &&
            Quest.Instance.Status.Resources.Wood.Value > 0 &&
            Quest.Instance.Status.Resources.Gas.Value > 0
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
    
        return _character.Name + " умер от голода. Нужно что-то сделать с телом.";
    }

	public override QuestAction GetAction1()
	{
		return new QuestAction
        (
            "Похоронить",
            ()=>
            {
                UpdateTime(6);
                UpdateResource(QuestResourceType.Wood, -1);

                _character.SetBurialType(QuestCharacterBurialType.Buried);

                End(_character.Name + " был похоронен недалеко от лагеря");
            }
        );
	}

	public override QuestAction GetAction2()
	{
        return new QuestAction
        (
            "Сжечь тело",
            ()=>
            {
                UpdateTime(3);
                UpdateResource(QuestResourceType.Gas, -1);
                
                _character.SetBurialType(QuestCharacterBurialType.Burned);
            
                if(Quest.Instance.Status.Characters.FindAlive().Count > 1)
                {
                    End("Мы облили тело бензином и подожгли. От запаха обгоревшей плоти " + Quest.Instance.Status.Characters.FindAlive()[1].Name + " проблевался.");
                }
                else
                {
                    End("Я потратил несколько часов на то, чтобы сжечь тело. Вся моя одежда пропиталась вонью сгоревшей плоти.");
                }
            }
        );
	}
}
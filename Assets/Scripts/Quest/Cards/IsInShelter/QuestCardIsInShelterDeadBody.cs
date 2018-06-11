using System.Collections.Generic;

public class QuestCardIsInShelterDeadBody : QuestCard
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
            Quest.Instance.Status.Characters.FindDead(null, QuestCharacterBurialType.None) != null
        );
    }

    public override float GetCooldown()
    {
        return 0;
    }
    
    public override string GetQuestion()
    {
        _character = Quest.Instance.Status.Characters.FindDead(null, QuestCharacterBurialType.None);
        _character.SetBurialType(QuestCharacterBurialType.Unknown);
        
        return _character.Name + " умер. Нужно что-то сделать с телом";
    }

	public override QuestAction GetAction1()
	{
        if(Quest.Instance.Status.Resources.Wood.Value > 0)
        {
            return GetActionBury();
        }
        
        if(Quest.Instance.Status.Resources.Gas.Value > 0)
        {
            return GetActionBurn();
        }
    
        return GetActionLeftOutsideOfShelter();
	}

	public override QuestAction GetAction2()
	{
        if(Quest.Instance.Status.Resources.Wood.Value > 0 && Quest.Instance.Status.Resources.Gas.Value > 0)
        {
            return GetActionBurn();
        }
        
        if(Quest.Instance.Status.Resources.Wood.Value <= 0 && Quest.Instance.Status.Resources.Gas.Value > 0)
        {
            return GetActionLeftOutsideOfShelter();
        }
        
        if(Quest.Instance.Status.Resources.Wood.Value > 0 && Quest.Instance.Status.Resources.Gas.Value <= 0)
        {
            return GetActionLeftOutsideOfShelter();
        }
    
        return GetActionLeftInShelter();
	}
    
    private QuestAction GetActionBurn()
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
                    End("Мы облили тело бензином и подожгли. От запаха обгоревшей плоти " + Quest.Instance.Status.Characters.FindAlive().Get(1).Name + " проблевался.");
                }
                else
                {
                    End("Я потратил несколько часов на то, чтобы сжечь тело. Вся моя одежда пропиталась вонью сгоревшей плоти.");
                }
            }
        );
    }
    
    private QuestAction GetActionBury()
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
    
    private QuestAction GetActionLeftOutsideOfShelter()
    {
        QuestCharactersGroup charactersOutsideOfShelter = Quest.Instance.Status.Characters.FindDeadAll(null, QuestCharacterBurialType.LeftOutsideOfShelter);

        string action = "";
        string answer = "";
    
        if(charactersOutsideOfShelter.Count == 0)
        {
            action = "Бросить тело неподалеку от лагеря";
            answer = "Я бросил тело в небольшую яму рядом с лагерем.";
        }
        
        if(charactersOutsideOfShelter.Count == 1)
        {
            action = "Выбросить тело рядом с лагерем";
            answer = "Я бросил тело в небольшую яму рядом с лагерем. " + charactersOutsideOfShelter.Get(0).Name + " и " + _character.Name + " будут гнить вместе.";
        }
        
        if(charactersOutsideOfShelter.Count >= 2)
        {
            string names = "";

            int count = UnityEngine.Mathf.Min(3, charactersOutsideOfShelter.Count);
            
            for (int i = 0; i < count; i++)
            {
                if(i == count - 1)
                {
                    if(charactersOutsideOfShelter.Count > count)
                    {
                        names += " и остальные тела";
                    }
                    else
                    {
                        names += " и " + charactersOutsideOfShelter.Get(i).Name;
                    }
                }
                else
                {
                    names += string.IsNullOrEmpty(names) == false ? ", " : "";
                    names += charactersOutsideOfShelter.Get(i).Name;
                }
            }
            
            action = "Выбросить тело рядом с лагерем";
            answer = "Я бросил тело в яму, в которой гниют " + names + ".";
        }
    
        return new QuestAction
        (
            action,
            ()=>
            {
                UpdateTime(6);
                _character.SetBurialType(QuestCharacterBurialType.LeftOutsideOfShelter);

                End(answer);
            }
        );
    }
    
    private QuestAction GetActionLeftInShelter()
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
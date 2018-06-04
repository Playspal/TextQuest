using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBuilding
{
    public QuestBuildingState State = QuestBuildingState.NotBuilded;
    public QuestCharacter Worker { get; private set; }
    
    public QuestBuildingType BuildingType { get; protected set; }
    
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public string DescriptionJob { get; protected set; }
    public string ActionNameConstruct { get; protected set; }
    
    public List<QuestResource> Cost { get; protected set; }
    
    public bool IsBuilded
    {
        get
        {
            return State == QuestBuildingState.Builded;
        }
    }

    protected int _constructionDuration = 60;
    protected int _constructionTime = 0;
    
    public virtual int GetConstructionTimeleft()
    {
        return _constructionDuration - _constructionTime;
    }

    public virtual void SetWorker(QuestCharacter character)
    {
        Worker = character;
    }
    
    public virtual void Deconstruct()
    {
        State = QuestBuildingState.NotBuilded;
        SetWorker(null);
        
        _constructionTime = 0;
    }
    
    public virtual void ConstructionStart()
    {
        State = QuestBuildingState.ConstructionInProgress;

        for (int i = 0; i < Cost.Count; i++)
        {
            Quest.Instance.Status.Resources.Update
            (
                Cost[i].Type,
                -Cost[i].Value
            );
        }
    }
    
    public virtual void ConstructionProcess(int minutes)
    {
        if(Worker == null)
        {
            return;
        }
    
        _constructionTime += minutes;
        
        if(_constructionTime >= _constructionDuration)
        {
            ConstructionFinish();
        }
    }
    
    public virtual void ConstructionFinish()
    {
        _constructionTime = _constructionDuration;
        State = QuestBuildingState.Builded;
    }
    
    public virtual void ProcessMinutes(int value)
    {
        if (State == QuestBuildingState.ConstructionInProgress)
        {
            ConstructionProcess(value);
        }
        
        if(Worker != null && Worker.IsDead)
        {
            SetWorker(null);
        }
    }
}

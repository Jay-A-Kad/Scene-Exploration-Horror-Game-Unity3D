using System.Collections.Generic;
using NUnit.Framework;

public class QuestRuntime
{
    private readonly List<QuestObjectiveRuntime> objectives = new List<QuestObjectiveRuntime>();
    private int currentObjectiveIndex = 0;

    public string QuestId { get; private set; }
    public string Title { get; private set; }
    public IReadOnlyList<QuestObjectiveRuntime> Objectives => objectives;

    public bool IsStarted { get; private set; }
    public bool IsCompleted { get; private set; }

    public QuestRuntime(QuestDefinition definition)
    {
        QuestId = definition.QuestId;
        Title = definition.QuestTitle;
        if (definition.Objectives != null)
        {
            foreach (var objData in definition.Objectives)
            {
                objectives.Add(new QuestObjectiveRuntime(objData.ObjectiveId, objData.Description));
            }
        }

    }

    public void Start()
    {
        if (IsStarted || objectives.Count == 0) return;
        IsStarted = true;
        objectives[0].Activate();
    }

    public bool ContainsObjective(string objectiveId)
    {
        foreach (var obj in objectives)
        {
            if (obj.ObjectiveId == objectiveId)
            {
                return true;
            }
        }
        return false;
    }

    public bool TryCompleteObjective(string objectiveId)
    {
        if (!IsStarted || IsCompleted) return false;
        if (currentObjectiveIndex >= objectives.Count) return false;

        var current = objectives[currentObjectiveIndex];

        if (current.ObjectiveId != objectiveId) return false;

        current.Complete();
        currentObjectiveIndex++;
        if (currentObjectiveIndex < objectives.Count)
        {
            objectives[currentObjectiveIndex].Activate();
        }
        else
        {
            IsCompleted = true;
        }
        return true;

    }



}
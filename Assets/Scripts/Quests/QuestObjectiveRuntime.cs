using System;

public class QuestObjectiveRuntime
{
    public string ObjectiveId { get; private set; }
    public string Description { get; private set; }
    public QuestObjectiveState State { get; private set; }

    public QuestObjectiveRuntime(string objectiveId, string description)
    {
        ObjectiveId = objectiveId;
        Description = Description;
        State = QuestObjectiveState.Locked;
    }
    public void Activate()
    {
        if (State == QuestObjectiveState.Locked)
        {
            State = QuestObjectiveState.Active;
        }
    }
    public void Complete()
    {
        State = QuestObjectiveState.Completed;
    }
}
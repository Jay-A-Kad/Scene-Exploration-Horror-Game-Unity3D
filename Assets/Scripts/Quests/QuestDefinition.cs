using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest Definition")]
public class QuestDefinition : ScriptableObject
{
    [SerializeField] private string questId;
    [SerializeField] private string questTitle;
    [SerializeField] private QuestObjectiveData[] objectives;

    public string QuestId => questId;
    public string QuestTitle => questTitle;
    public QuestObjectiveData[] Objectives => objectives;
}
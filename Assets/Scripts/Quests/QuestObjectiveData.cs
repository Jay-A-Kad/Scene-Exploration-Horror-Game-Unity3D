using UnityEngine;

[SerializeField]
public class QuestObjectiveData : MonoBehaviour
{
    [SerializeField] private string objectiveId;
    [SerializeField] private string description;

    public string ObjectiveId => objectiveId;
    public string Description => description;
}

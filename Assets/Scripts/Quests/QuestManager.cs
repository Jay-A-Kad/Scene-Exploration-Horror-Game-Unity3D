using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [Header("Quest")]
    [SerializeField] private QuestDefinition[] availableQuests;

    private readonly List<QuestRuntime> activeQuests = new List<QuestRuntime>();
    private readonly List<QuestRuntime> completedQuests = new List<QuestRuntime>();

    public IReadOnlyList<QuestRuntime> ActiveQuests => activeQuests;
    public IReadOnlyList<QuestRuntime> CompletedQuests => completedQuests;
    public event Action OnQuestLogChanged;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void Start()
    {
        if (availableQuests != null)
        {
            foreach (var questDef in availableQuests)
            {
                activeQuests.Add(new QuestRuntime(questDef));
            }
        }
    }
    public bool StartQuest(QuestDefinition questDef)
    {
        if (questDef == null) return false;
        if (HasQuest(questDef.QuestId)) return false;

        QuestRuntime newQuest = new QuestRuntime(questDef);
        newQuest.Start();
        activeQuests.Add(newQuest);

        OnQuestLogChanged?.Invoke();
        return true;
    }



    public bool CompleteObjective(string ObjectiveId)
    {
        foreach (var quest in activeQuests)
        {
            QuestRuntime completedQuest = quest;
            if (!completedQuest.ContainsObjective(ObjectiveId)) continue;

            bool completed = completedQuest.TryCompleteObjective(ObjectiveId);
            if (!completed) return false;

            if (completedQuest.IsCompleted)
            {
                activeQuests.Remove(completedQuest);
                completedQuests.Add(completedQuest);
            }
            OnQuestLogChanged?.Invoke();



        }
        return true;
    }

    public bool HasQuest(string questId)
    {
        foreach (var quest in activeQuests)
        {
            if (quest.QuestId == questId) return true;
        }
        foreach (var quest in completedQuests)
        {
            if (quest.QuestId == questId) return true;
        }
        return false;
    }

}
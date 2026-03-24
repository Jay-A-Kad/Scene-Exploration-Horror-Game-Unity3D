

using System.Collections.Generic;
using UnityEngine;

public class HotelStoryManager : MonoBehaviour
{
    [SerializeField] private DialogueUI dialgoueUI;
    [SerializeField] private List<HotelStoryStep> storySteps = new List<HotelStoryStep>();
    private StoryProgression<HotelMilestone> storyProgression;

    private void Awake()
    {
        var runtimeSteps = new List<IStoryStep<HotelMilestone>>();
        for (int i = 0; i < storySteps.Count; i++)
        {
            runtimeSteps.Add(storySteps[i]);
        }
        storyProgression = new StoryProgression<HotelMilestone>(runtimeSteps);
        storyProgression.OnStepStarted += HandleStepStarted;
        if (dialgoueUI != null)
        {
            dialgoueUI.OnDialogueFinished += HandleDialogueFnished;
        }
    }

    private void OnDestory()
    {
        if (storyProgression != null)
        {
            storyProgression.OnStepStarted -= HandleStepStarted;
        }
        if (dialgoueUI != null)
        {
            dialgoueUI.OnDialogueFinished -= HandleDialogueFnished;
        }
    }

    public bool NotifyMilestone(HotelMilestone milestone)
    {
        return storyProgression.TryTrigger(milestone);
    }
    public bool IsExpectedMilestone(HotelMilestone milestone)
    {
        return storyProgression != null && storyProgression.IsExpectedMilestone(milestone);
    }
    private void HandleStepStarted(IStoryStep<HotelMilestone> step)
    {
        if (dialgoueUI == null)
        {
            Debug.LogError("DialogueUI reference is missing on HotelStoryManager.");
            return;
        }

        dialgoueUI.ShowDialogue(step.Lines);
    }
    private void HandleDialogueFnished()
    {
        if (storyProgression != null)
        {
            storyProgression.FinishActiveStep();
        }
    }
}
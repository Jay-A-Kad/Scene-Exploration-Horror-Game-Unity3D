

using System;
using System.Collections.Generic;
using NUnit.Framework;

public class StoryProgression<TMilestone> where TMilestone : Enum
{
    private readonly List<IStoryStep<TMilestone>> steps = new List<IStoryStep<TMilestone>>();
    private int nextStepIndex = 0;
    private IStoryStep<TMilestone> activeStep;

    public bool isDialogueRunning { get; private set; }
    public bool isComplete => nextStepIndex >= steps.Count;

    //events
    public event Action<IStoryStep<TMilestone>> OnStepStarted;
    public event Action<IStoryStep<TMilestone>> OnStepCompleted;

    public StoryProgression(IEnumerable<IStoryStep<TMilestone>> orderedSteps)
    {
        foreach (var step in orderedSteps)
        {
            steps.Add(step);
        }
    }


    public bool TryTrigger(TMilestone milestone)
    {
        if (isDialogueRunning || isComplete) return false;

        var expectedStep = steps[nextStepIndex];
        if (!EqualityComparer<TMilestone>.Default.Equals(expectedStep.Id, milestone))
            return false;

        activeStep = expectedStep;
        isDialogueRunning = true;

        OnStepStarted?.Invoke(activeStep);
        return true;
    }

    public void FinishActiveStep()
    {
        if (!isDialogueRunning || activeStep == null) return;

        OnStepCompleted?.Invoke(activeStep);
        activeStep = null;
        isDialogueRunning = false;
        nextStepIndex++;
    }

    public bool IsExpectedMilestone(TMilestone milestone)
    {
        if (isDialogueRunning || isComplete) return false;
        return EqualityComparer<TMilestone>.Default.Equals(steps[nextStepIndex].Id, milestone);
    }

}
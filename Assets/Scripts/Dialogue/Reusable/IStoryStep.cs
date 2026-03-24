using System.Collections.Generic;

public interface IStoryStep<TMilestone>
{
    TMilestone Id { get; }
    IReadOnlyList<DialogueLine> Lines { get; }
}
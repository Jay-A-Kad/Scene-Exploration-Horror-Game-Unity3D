
using System;
using System.Collections.Generic;

[Serializable]
public class HotelStoryStep : IStoryStep<HotelMilestone>
{
    public HotelMilestone milestone;
    public DialogueLine[] lines;
    public HotelMilestone Id => milestone;
    public IReadOnlyList<DialogueLine> Lines => lines;
}
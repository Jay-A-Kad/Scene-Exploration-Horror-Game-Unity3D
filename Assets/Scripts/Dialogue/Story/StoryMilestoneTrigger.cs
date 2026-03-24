

using UnityEngine;
using UnityEngine.Rendering;


[RequireComponent(typeof(Collider))]
public class StoryMilestoneTrigger : MonoBehaviour
{
    [SerializeField] private HotelStoryManager storyManager;
    [SerializeField] private HotelMilestone milestone;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private bool triggerOnce = true;

    private bool hasTriggered = false;


    private void Reset()
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        TryActivate(other);
    }
    private void OnTriggerStay(Collider other)
    {
        TryActivate(other);
    }
    private void TryActivate(Collider other)
    {
        if (hasTriggered) return;

        if (storyManager == null) return;

        if (!other.CompareTag(playerTag)) return;

        if (!storyManager.IsExpectedMilestone(milestone)) return;

        bool accepted = storyManager.NotifyMilestone(milestone);
        if (accepted && triggerOnce)
        {
            hasTriggered = true;
            GetComponent<Collider>().enabled = false;
        }
    }
}
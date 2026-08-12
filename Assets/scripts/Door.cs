using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour, IInteractable
{
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    public float rotationSpeed = 2f;

    private QuestManager questManager;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, 90, 0) * closedRotation;

        questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
    }

    public string GetInteractionText()
    {
        return isOpen ? "Close" : "Open";
    }

    public void Interact()
    {
        isOpen = !isOpen;
        StopAllCoroutines();
        StartCoroutine(RotateDoor());

        if (questManager != null)
        {
            questManager.CompleteQuest(0, isOpen); // 0 - "Открыть дверь"
        }
    }

    IEnumerator RotateDoor()
    {
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }
        transform.rotation = targetRotation;
    }
}

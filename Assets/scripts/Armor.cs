using UnityEngine;

public class ProtectiveGear : MonoBehaviour, IInteractable
{
    public enum GearType { Glasses, Gloves, Clothing }
    public GearType gearType;

    private bool isEquipped = false;
    private Renderer objectRenderer;
    private QuestManager questManager;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        questManager = GameObject.Find("QuestManager")?.GetComponent<QuestManager>();
    }

    public string GetInteractionText()
    {
        string gearName = "";

        switch (gearType)
        {
            case GearType.Glasses:
                gearName = "защитные очки";
                break;
            case GearType.Gloves:
                gearName = "защитные перчатки";
                break;
            case GearType.Clothing:
                gearName = " защитную одежду";
                break;
        }

        return isEquipped ? $"Снять {gearName}" : $"Надеть {gearName}";
    }


    public void Interact()
    {
        isEquipped = !isEquipped;

        if (objectRenderer != null)
            objectRenderer.enabled = !isEquipped;

        UpdateQuest();
    }

    private void UpdateQuest()
    {
        if (questManager == null)
        {
            Debug.LogWarning("QuestManager не найден.");
            return;
        }

        switch (gearType)
        {
            case GearType.Glasses:
                questManager.CompleteQuest(2, isEquipped); break;
            case GearType.Gloves:
                questManager.CompleteQuest(3, isEquipped); break;
            case GearType.Clothing:
                questManager.CompleteQuest(4, isEquipped); break;
        }
    }
}

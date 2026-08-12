using UnityEngine;

public class LevelEndInteractable : MonoBehaviour, IInteractable
{
    public string GetInteractionText()
    {
        return "Завершить уровень";
    }

    public void Interact()
    {
        var questManager = FindFirstObjectByType<QuestManager>();
        if (questManager != null)
        {
            questManager.ShowFinalResults();
        }
        else
        {
            Debug.LogError("QuestManager не найден на сцене.");
        }
    }
}


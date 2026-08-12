using UnityEngine;

public class DebrisInteractable : MonoBehaviour, IInteractable
{
    public string GetInteractionText()
    {
        return "Убрать обломки";
    }

    public void Interact()
    {
        Debug.Log("Осколки убраны!");

        // ✅ Завершаем квест "Очистить"
        QuestManager qm = FindFirstObjectByType<QuestManager>();
        if (qm != null)
            qm.CompleteQuest(8, true); // 8-й квест — индекс 7

        Destroy(gameObject);
    }
}



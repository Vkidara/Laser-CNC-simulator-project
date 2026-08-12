using UnityEngine;
using TMPro;

public class ProtectiveGear3 : MonoBehaviour, IInteractable
{
    [SerializeField] private string gearName = "элемент защиты"; // Название элемента (очки, перчатки и т.п.)
    [SerializeField] private TextMeshProUGUI statusText;         // Назначить в инспекторе
    private bool isEquipped = false;
    private Renderer[] renderers;

    void Start()
    {
        // Собираем все рендеры в объекте и его потомках
        renderers = GetComponentsInChildren<Renderer>();

        // Если текст не назначен вручную — ищем по имени
        if (statusText == null)
        {
            var textObj = GameObject.Find("ProtectionText");
            if (textObj != null)
                statusText = textObj.GetComponent<TextMeshProUGUI>();
        }

        if (statusText != null)
            statusText.gameObject.SetActive(false);
    }

    public string GetInteractionText()
    {
        return isEquipped ? $"Снять {gearName}" : $"Надеть {gearName}";
    }

    public void Interact()
    {
        isEquipped = !isEquipped;

        // Меняем видимость рендеров
        foreach (var rend in renderers)
            rend.enabled = !isEquipped;

        // Обновляем текстовую надпись
        if (statusText != null)
        {
            if (isEquipped)
            {
                statusText.text = $"{gearName} надеты";
                statusText.gameObject.SetActive(true);
            }
            else
            {
                statusText.gameObject.SetActive(false);
            }
        }
        // Например, для очков
        QuestManager questManager = FindFirstObjectByType<QuestManager>();
        questManager.CompleteQuest(3, true); // 1 — индекс квеста "Надеть очки"

    }
}



using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public Light[] lights;
    private bool lightsOn = false;

    private QuestManager questManager;

    void Start()
    {
        // Поиск QuestManager через тег (лучше, чем по имени объекта)
        GameObject managerObj = GameObject.FindWithTag("QuestManager");
        if (managerObj != null)
        {
            questManager = managerObj.GetComponent<QuestManager>();
        }
    }

    public void ToggleLights()
    {
        lightsOn = !lightsOn;

        foreach (Light light in lights)
        {
            light.enabled = lightsOn;
        }

        UpdateQuest();
    }

    private void UpdateQuest()
    {
        if (questManager == null) return;

        bool allLightsOn = true;
        foreach (Light light in lights)
        {
            if (!light.enabled)
            {
                allLightsOn = false;
                break;
            }
        }

        // Квест "Включить свет" — под индексом 0
        questManager.CompleteQuest(1, allLightsOn);
    }

    // Реализация IInteractable
    public string GetInteractionText()
    {
        return lightsOn ? "Выключить свет" : "Включить свет";
    }

    public void Interact()
    {
        ToggleLights();
    }
}



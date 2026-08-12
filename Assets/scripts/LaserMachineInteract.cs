using UnityEngine;

public class LaserMachineInteract : MonoBehaviour, IInteractable
{
    public GameObject drawingSelectionPanel;
    public PlayerMovement playerMovement;
    public GameObject questHintText;
    public QuestMenuController questMenuController;

    public GameObject drawingTypeSelectionPanel; // 🆕 новая панель


    public static bool isInMachineMenu = false;

    private PlayerInteraction playerInteraction; // 🆕 Добавим поле

    void Start()
    {
        playerInteraction = FindFirstObjectByType<PlayerInteraction>(); // 🆕 Находим в Start
    }

    public string GetInteractionText()
    {
        return "Открыть интерфейс выбора чертежа";
    }

    public void Interact()
    {
        OpenDrawingSelection();
    }

    private void OpenDrawingSelection()
    {
        if (drawingTypeSelectionPanel != null)     // 🆕 вместо drawingSelectionPanel
            drawingTypeSelectionPanel.SetActive(true);

        if (playerMovement != null)
            playerMovement.SetCanMove(false);

        CursorManager.Instance.RegisterMenuOpened();

        isInMachineMenu = true;

        if (questHintText != null)
            questHintText.SetActive(false);

        if (questMenuController != null)
            questMenuController.CloseMenu();

        if (playerInteraction == null)
            playerInteraction = FindFirstObjectByType<PlayerInteraction>();

        if (playerInteraction != null)
            playerInteraction.SetInteractionBlocked(true);
    }


    public static void CloseMachineMenu()
    {
        LaserMachineInteract instance = FindFirstObjectByType<LaserMachineInteract>();

        if (instance != null)
        {
            if (instance.drawingSelectionPanel != null)
                instance.drawingSelectionPanel.SetActive(false);

            if (instance.playerMovement != null)
                instance.playerMovement.SetCanMove(true);

            if (instance.questHintText != null)
                instance.questHintText.SetActive(true);

            if (instance.playerInteraction == null)
                instance.playerInteraction = FindFirstObjectByType<PlayerInteraction>();

            if (instance.playerInteraction != null)
                instance.playerInteraction.SetInteractionBlocked(false); // ✅ Разблокируем
        }

        CursorManager.Instance.RegisterMenuClosed();
        isInMachineMenu = false;
        Debug.Log("[LaserMachineInteract] isInMachineMenu = false");
    }
}



using UnityEngine;

public class DrawingMenuController : MonoBehaviour
{
    public GameObject drawingSelectionPanel;
    public PlayerMovement playerMovement;
    public GameObject questHintText;

    public void OnBackButtonPressed()
    {
        // Закрываем панель
        if (drawingSelectionPanel != null)
            drawingSelectionPanel.SetActive(false);

        // Возвращаем управление игроку
        if (playerMovement != null)
            playerMovement.enabled = true;

        // Возвращаем курсор в игровой режим
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Показываем подсказку про J, если нужно
        if (questHintText != null)
            questHintText.SetActive(true);

        // Сообщаем, что интерфейс станка больше не активен
        LaserMachineInteract.CloseMachineMenu();
    }
}


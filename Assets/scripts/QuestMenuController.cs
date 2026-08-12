using UnityEngine;

public class QuestMenuController : MonoBehaviour
{
    public GameObject questMenuPanel;
    public PlayerMovement playerMovement;

    private bool isMenuVisible = false;

    void Start()
    {
        if (questMenuPanel != null)
            questMenuPanel.SetActive(false);

        CursorManager.Instance.ForceLockCursor(); // Через CursorManager
    }

    void Update()
    {
       // if (LaserMachineInteract.isInMachineMenu)
         //   return;

        if (Input.GetKeyDown(KeyCode.J))
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        isMenuVisible = !isMenuVisible;

        if (questMenuPanel != null)
            questMenuPanel.SetActive(isMenuVisible);

        if (playerMovement != null)
            playerMovement.enabled = !isMenuVisible;

        if (isMenuVisible)
            CursorManager.Instance.RegisterMenuOpened();
        else
            CursorManager.Instance.RegisterMenuClosed();
    }

    public void CloseMenu()
    {
        isMenuVisible = false;

        if (questMenuPanel != null)
            questMenuPanel.SetActive(false);

        if (playerMovement != null)
            playerMovement.enabled = true;

        CursorManager.Instance.RegisterMenuClosed();
    }
}



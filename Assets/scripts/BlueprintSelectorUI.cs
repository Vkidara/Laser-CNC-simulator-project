using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BlueprintSelectorUI : MonoBehaviour
{
    public List<Button> blueprintButtons;
    public Image blueprintPreviewImage;
    public LaserBlueprintHolder blueprintHolder;
    public GameObject panelToClose;

    public PlayerMovement playerMovement;
    public GameObject hintText;

    private QuestManager questManager;
    private bool hasCompletedQuest = false;

    private PlayerInteraction playerInteraction; // 🆕 Добавим поле

    public static bool IsOpen { get; private set; } = false;

    void Start()
    {
        SetupButtons();

        GameObject qm = GameObject.FindWithTag("QuestManager");
        if (qm != null)
        {
            questManager = qm.GetComponent<QuestManager>();
        }

        playerInteraction = FindFirstObjectByType<PlayerInteraction>(); // 🆕 Ищем PlayerInteraction
    }

    void SetupButtons()
    {
        foreach (var button in blueprintButtons)
        {
            BlueprintButtonInfo info = button.GetComponent<BlueprintButtonInfo>();
            if (info != null)
            {
                button.onClick.AddListener(() =>
                {
                    SelectBlueprint(info.blueprintData);
                });
            }
        }
    }

    public void SelectBlueprint(BlueprintData blueprint)
    {
        blueprintHolder.SetSelectedBlueprint(blueprint);

        if (!hasCompletedQuest && questManager != null)
        {
            questManager.CompleteQuest(6, true);
            hasCompletedQuest = true;
        }

        CloseMenu();
    }

    public void ShowMenu()
    {
        if (panelToClose != null)
            panelToClose.SetActive(true);

        if (playerMovement != null)
            playerMovement.SetCanMove(false);

        if (playerInteraction == null)
            playerInteraction = FindFirstObjectByType<PlayerInteraction>(); // ✅ гарантированно

        if (playerInteraction != null)
            playerInteraction.SetInteractionBlocked(true); // ✅ блокируем

        if (hintText != null)
            hintText.SetActive(false);

        CursorManager.Instance.RegisterMenuOpened();
        IsOpen = true;
    }


    public void CloseMenu()
    {
        if (panelToClose != null)
            panelToClose.SetActive(false);

        if (playerMovement != null)
            playerMovement.SetCanMove(true);

        if (playerInteraction != null)
            playerInteraction.SetInteractionBlocked(false); // ✅ Возвращаем взаимодействие


        if (playerInteraction == null)
            playerInteraction = FindFirstObjectByType<PlayerInteraction>();

        if (playerInteraction != null)
            playerInteraction.SetInteractionBlocked(false);


        if (hintText != null)
            hintText.SetActive(true);

        CursorManager.Instance.RegisterMenuClosed();
        IsOpen = false;

        LaserMachineInteract.CloseMachineMenu();
    }

    public void ShowPreview(Sprite previewImage)
    {
        if (blueprintPreviewImage != null)
        {
            blueprintPreviewImage.sprite = previewImage;
            blueprintPreviewImage.enabled = true;
        }
    }

    public void HidePreview()
    {
        if (blueprintPreviewImage != null)
        {
            blueprintPreviewImage.enabled = false;
        }
    }
}




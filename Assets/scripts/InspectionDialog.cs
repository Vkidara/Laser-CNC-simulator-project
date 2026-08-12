using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InspectionDialog : MonoBehaviour
{
    public static InspectionDialog Instance;

    public GameObject panel;
    public TMP_Text descriptionText;
    public Button continueButton;
    public TMP_Text buttonText;

    private Action onCloseCallback;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public static void Show(string description, string buttonLabel, Action onConfirm)
    {
        if (Instance == null)
        {
            Debug.LogError("❌ InspectionDialog.Instance is null");
            return;
        }

        // 🔒 Блокируем движение и показываем курсор
        CursorManager.Instance?.RegisterMenuOpened();
        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
        if (player != null) player.SetCanMove(false);

        Instance.panel.SetActive(true);
        Instance.descriptionText.text = description;
        Instance.buttonText.text = buttonLabel;

        Instance.continueButton.onClick.RemoveAllListeners();
        Instance.continueButton.onClick.AddListener(() =>
        {
            Instance.panel.SetActive(false);

            // ✅ Возвращаем управление и курсор
            CursorManager.Instance?.RegisterMenuClosed();
            PlayerMovement p = FindFirstObjectByType<PlayerMovement>();
            if (p != null) p.SetCanMove(true);

            onConfirm?.Invoke();
        });
    }
}



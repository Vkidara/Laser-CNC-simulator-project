using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class FinalResultPanel : MonoBehaviour
{
    public static FinalResultPanel Instance;

    public GameObject panel;
    public TMP_Text scoreText;
    public TMP_Text gradeText;
    public TMP_Text errorsText;
    public Button closeButton;

    private PlayerMovement playerMovement;
    private PlayerInteraction playerInteraction;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (closeButton != null)
            closeButton.onClick.AddListener(Hide);

        panel.SetActive(false);
    }

    public void Show(int score, string grade, List<string> errors)
    {
        panel.SetActive(true);

        scoreText.text = $"Счёт: {score}";
        gradeText.text = $"Оценка: {grade}";
        errorsText.text = string.Join("\n", errors);

        CursorManager.Instance?.RegisterMenuOpened();

        if (playerMovement == null)
            playerMovement = FindFirstObjectByType<PlayerMovement>();
        if (playerMovement != null)
            playerMovement.SetCanMove(false);

        if (playerInteraction == null)
            playerInteraction = FindFirstObjectByType<PlayerInteraction>();
        if (playerInteraction != null)
            playerInteraction.SetInteractionBlocked(true); // ✅ Скрываем взаимодействие
    }

    public void Hide()
    {
        panel.SetActive(false);

        CursorManager.Instance?.RegisterMenuClosed();

        if (playerMovement == null)
            playerMovement = FindFirstObjectByType<PlayerMovement>();
        if (playerMovement != null)
            playerMovement.SetCanMove(true);

        if (playerInteraction == null)
            playerInteraction = FindFirstObjectByType<PlayerInteraction>();
        if (playerInteraction != null)
            playerInteraction.SetInteractionBlocked(false); // ✅ Возвращаем взаимодействие
    }
}

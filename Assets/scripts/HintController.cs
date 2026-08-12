using UnityEngine;
using UnityEngine.UI;

public class HintController : MonoBehaviour
{
    public GameObject questMenuPanel; // Ссылка на меню квестов
    public GameObject pauseMenuPanel; // Ссылка на меню выхода (если есть)
    public GameObject hintText;       // Текстовая подсказка "Нажмите J..."

    void Update()
    {
        bool isQuestMenuOpen = questMenuPanel != null && questMenuPanel.activeSelf;
        bool isPauseMenuOpen = pauseMenuPanel != null && pauseMenuPanel.activeSelf;

        // Показываем подсказку, только если оба меню закрыты
        if (hintText != null)
        {
            hintText.SetActive(!isQuestMenuOpen && !isPauseMenuOpen);
        }
    }
}


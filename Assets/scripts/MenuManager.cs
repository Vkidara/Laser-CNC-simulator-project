using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelMenu;

    [Header("Preview UI")]
    public Image levelPreviewImage;
    public TextMeshProUGUI levelPreviewText;

    [Header("Level Info")]
    public Sprite level1Preview;
    public Sprite level2Preview;

    public string level1Description = "📘 Уровень 1: Обучение. Научись пользоваться лазером.";
    public string level2Description = "🛠️ Уровень 2: Работа с металлом. Более сложные задачи.";

    void Start()
    {
        ShowMainMenu();
        CursorManager.Instance.ForceUnlockCursor();
        HidePreview();
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void ShowLevelMenu()
    {
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

    public void StartLevel1()
    {
        SceneManager.LoadScene("level1");
        CursorManager.Instance.ForceLockCursor();
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene("level2");
        CursorManager.Instance.ForceLockCursor();
    }

    public void QuitGame()
    {
        Debug.Log("Игра закрывается...");
        Application.Quit();
    }

    public void ShowLevelPreview(string levelName)
    {
        if (levelPreviewImage == null || levelPreviewText == null) return;

        levelPreviewImage.gameObject.SetActive(true);
        levelPreviewText.gameObject.SetActive(true);

        switch (levelName)
        {
            case "level1":
                levelPreviewImage.sprite = level1Preview;
                levelPreviewText.text = level1Description;
                break;
            case "level2":
                levelPreviewImage.sprite = level2Preview;
                levelPreviewText.text = level2Description;
                break;
            default:
                levelPreviewImage.gameObject.SetActive(false);
                levelPreviewText.gameObject.SetActive(false);
                break;
        }
    }

    public void HidePreview()
    {
        if (levelPreviewImage != null) levelPreviewImage.gameObject.SetActive(false);
        if (levelPreviewText != null) levelPreviewText.gameObject.SetActive(false);
    }
}

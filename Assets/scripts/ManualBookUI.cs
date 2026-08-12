using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManualBookUI : MonoBehaviour, IInteractable
{
    public GameObject bookPanel;
    public TMP_Text pageText;
    public Button nextPageButton;
    public Button prevPageButton;
    public Button exitButton;

    public PlayerMovement playerMovement;

    [TextArea(3, 10)]
    public string[] pages;

    private int currentPage = 0;
    private bool isOpen = false;

    public static bool IsOpen => instance != null && instance.isOpen;
    private static ManualBookUI instance;

    private QuestManager questManager;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        bookPanel.SetActive(false);

        nextPageButton.onClick.AddListener(NextPage);
        prevPageButton.onClick.AddListener(PreviousPage);
        exitButton.onClick.AddListener(CloseBook);

        GameObject qmObj = GameObject.FindWithTag("QuestManager");
        if (qmObj != null)
        {
            questManager = qmObj.GetComponent<QuestManager>();
        }
    }

    public string GetInteractionText()
    {
        return "Прочитать руководство";
    }

    public void Interact()
    {
        if (!isOpen)
            OpenBook();
    }

    private void OpenBook()
    {
        currentPage = 0;
        isOpen = true;

        bookPanel.SetActive(true);
        UpdatePage();

        if (playerMovement != null)
            playerMovement.SetCanMove(false);

        CursorManager.Instance.RegisterMenuOpened();

        // 🔥 Отмечаем квест "Ознакомьтесь с руководством" выполненным (индекс 4)
        questManager?.CompleteQuest(5, true);
    }

    private void CloseBook()
    {
        isOpen = false;
        bookPanel.SetActive(false);

        if (playerMovement != null)
            playerMovement.SetCanMove(true);

        CursorManager.Instance.RegisterMenuClosed();
    }

    private void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            UpdatePage();
        }
        else
        {
            CloseBook();
        }
    }

    private void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
        }
    }

    private void UpdatePage()
    {
        if (pageText != null)
            pageText.text = pages[currentPage];
    }
}



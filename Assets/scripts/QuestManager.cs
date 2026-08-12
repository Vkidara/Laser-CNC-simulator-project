using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Transform questListContainer;
    public GameObject questButtonPrefab;
    public TMP_Text questDescriptionText;

    private List<Quest> quests = new List<Quest>();
    private List<Button> questButtons = new List<Button>();
    private int selectedQuestIndex = -1;
    private string qualityStatus = "unknown"; // perfect, faint, burn, overburn, undercut

    public static QuestManager Instance;

    // Для проверки оборудования
    private HashSet<string> requiredInspections = new HashSet<string>
    {
        "Лазерное зеркало",
        "Крышка защитного экрана",
        "Панель управления"
    };

    private HashSet<string> completedInspections = new HashSet<string>();

    private List<InspectionIssue> possibleIssues = new List<InspectionIssue>
    {
        new InspectionIssue {
            inspectionName = "Лазерное зеркало",
            problemDescription = "На зеркале пыль — это может повлиять на фокусировку луча. Рекомендация: протереть пыль чистой сухой тряпкой.",
            fixInstruction = "Протереть."
        },
        new InspectionIssue {
            inspectionName = "Защитное покрытие",
            problemDescription = "Защитное покрытие загрязнено - риск возгорания. Рекомендация: почистить с использованием химических средств.",
            fixInstruction = "Почистить."
        },
        new InspectionIssue {
            inspectionName = "Панель управления",
            problemDescription = "Один из тумблеров смещён — возможна ошибка запуска. Рекомендация: проверьте положения всех переключателей перед работой.",
            fixInstruction = "Проверить."
        }
    };

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InitializeQuests();
        CreateQuestButtons();
        SelectQuest(0);
    }

    void InitializeQuests()
    {
        quests.Clear();

        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "level1")
        {
            quests.Add(new Quest("Проверить оборудование", "Осмотрите ключевые элементы станка: зеркало, крышку, панель управления.", 15, "Проверка оборудования обязательна перед работой."));
            quests.Add(new Quest("Включить свет", "Включите освещение с помощью выключателя.", 5, "Недостаточная освещенность снижает безопасность на рабочем месте."));
            quests.Add(new Quest("Надеть очки", "Наденьте защитные очки.", 20, "Для работы с лазерным станком техника безопасности требует использование защитных очков."));
            quests.Add(new Quest("Надеть перчатки", "Наденьте защитные перчатки.", 10, "Перчатки защищают от ожогов и порезов при обращении с заготовками."));
            quests.Add(new Quest("Надеть защитную одежду", "Наденьте защитную спецодежду.", 10, "Спецодежда предотвращает попадание искр и загрязнений."));
            quests.Add(new Quest("Ознакомьтесь с руководством", "Ознакомьтесь с инструкцией работы на станке.", 15, "Невнимание к инструкции может привести к ошибкам в работе."));
            quests.Add(new Quest("Выбрать чертеж", "Выберите нужный чертеж на панели выбора.", 10, "Невыбранный чертеж приведет к неправильной гравировке."));
            quests.Add(new Quest("Выполнить гравировку", "Запустите процесс гравировки.", 30, "Цель уровня не выполнена."));
        }
        else if (sceneName == "level2")
        {
            quests.Add(new Quest("Проверить оборудование", "Осмотрите ключевые элементы станка: зеркало, крышку, панель управления.", 15, "Проверка оборудования обязательна перед работой."));
            quests.Add(new Quest("Включить свет", "Включите освещение с помощью выключателя.", 5, "Недостаточная освещенность снижает безопасность на рабочем месте."));
            quests.Add(new Quest("Надеть очки", "Наденьте защитные очки.", 20, "Для работы с лазерным станком техника безопасности требует использование защитных очков."));
            quests.Add(new Quest("Надеть перчатки", "Наденьте защитные перчатки.", 10, "Перчатки защищают от ожогов и порезов при обращении с заготовками."));
            quests.Add(new Quest("Надеть защитную одежду", "Наденьте защитную спецодежду.", 10, "Спецодежда предотвращает попадание искр и загрязнений."));
            quests.Add(new Quest("Ознакомьтесь с руководством", "Ознакомьтесь с инструкцией работы на станке.", 15, "Невнимание к инструкции может привести к ошибкам в работе."));
            quests.Add(new Quest("Выбрать чертеж", "Выберите нужный чертеж на панели выбора.", 10, "Невыбранный чертеж приведет к неправильной резке."));
            quests.Add(new Quest("Выполнить резку", "Начните резку заготовки.", 30, "Цель уровня не выполнена."));
            quests.Add(new Quest("Очистить", "Очистите обработанную заготовку от осколков.", 30, "Осколки должны быть убраны."));
        }
    }

    void CreateQuestButtons()
    {
        for (int i = 0; i < quests.Count; i++)
        {
            int index = i;
            GameObject buttonObj = Instantiate(questButtonPrefab, questListContainer);
            Button button = buttonObj.GetComponent<Button>();
            TMP_Text buttonText = buttonObj.GetComponentInChildren<TMP_Text>();

            if (button == null || buttonText == null) continue;

            buttonText.text = quests[i].Title;
            button.onClick.AddListener(() => SelectQuest(index));
            questButtons.Add(button);
        }

        UpdateQuestListUI();
    }

    void UpdateQuestListUI()
    {
        for (int i = 0; i < quests.Count; i++)
        {
            TMP_Text buttonText = questButtons[i].GetComponentInChildren<TMP_Text>();
            buttonText.text = quests[i].Title + (quests[i].IsCompleted ? " (выполнено)" : "");
        }
    }

    void SelectQuest(int index)
    {
        if (index < 0 || index >= quests.Count) return;
        selectedQuestIndex = index;
        questDescriptionText.text = quests[index].Description;
    }

    public void CompleteQuest(int questIndex, bool isCompleted)
    {
        if (questIndex < 0 || questIndex >= quests.Count) return;
        quests[questIndex].IsCompleted = isCompleted;
        UpdateQuestListUI();

        if (selectedQuestIndex == questIndex)
        {
            questDescriptionText.text = quests[questIndex].Description;
        }
    }

    public void CompleteQuestByTitle(string title, bool isCompleted)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].Title == title)
            {
                CompleteQuest(i, isCompleted);
                return;
            }
        }
    }

    public void RegisterInspection(string name, bool wasProblem)
    {
        completedInspections.Add(name);

        if (completedInspections.SetEquals(requiredInspections))
        {
            CompleteQuestByTitle("Проверить оборудование", true);
        }

        if (wasProblem)
            Debug.Log($"🛠 Обнаружена и устранена проблема на: {name}");
        else
            Debug.Log($"Осмотр завершён: {name}");
    }

    public InspectionIssue GetIssueFor(string name)
    {
        return possibleIssues.FirstOrDefault(i => i.inspectionName == name);
    }

    public void SetResultQuality(string quality)
    {
        qualityStatus = quality;
        Debug.Log("Качество результата установлено: " + quality);
    }
    public void ShowFinalResults()
    {
        int totalScore = 0;
        List<string> failureMessages = new List<string>();

        bool engravingDone = false;
        bool cuttingDone = false;
        bool criticalQualityFailure = false;

        foreach (var quest in quests)
        {
            if (quest.Title == "Выполнить гравировку")
                engravingDone = quest.IsCompleted;

            if (quest.Title == "Выполнить резку")
            {
                // Если результат плохой — квест считается не выполненным
                if (SceneManager.GetActiveScene().name == "level2" && (qualityStatus == "burned" || qualityStatus == "undercut"))
                {
                    cuttingDone = false;
                    failureMessages.Add("❌ Резка выполнена с браком — результат неудовлетворительный.");
                }
                else
                {
                    cuttingDone = quest.IsCompleted;
                }
            }

            if (quest.IsCompleted)
            {
                totalScore += quest.ImportanceScore;
            }
            else if (!string.IsNullOrEmpty(quest.FailureExplanation))
            {
                failureMessages.Add($"❌ {quest.Title} — {quest.FailureExplanation}");
            }
        }

        // Анализ качества
        if (qualityStatus == "faint" || qualityStatus == "burn")
        {
            failureMessages.Add("⚠️ Обработка завершена, но качество результата неудовлетворительное.");
            totalScore -= 10;
        }
        else if (qualityStatus == "overburn" || qualityStatus == "undercut")
        {
            failureMessages.Add("❌ Результат критически повреждён: обжог или недорез.");
            criticalQualityFailure = true;
        }

        string sceneName = SceneManager.GetActiveScene().name;
        string grade;

        if (sceneName == "level1")
        {
            grade = (engravingDone && !criticalQualityFailure) ? GetGrade(totalScore) : "F";
        }
        else if (sceneName == "level2")
        {
            grade = (cuttingDone && !criticalQualityFailure) ? GetGrade(totalScore) : "F";
        }
        else
        {
            grade = GetGrade(totalScore);
        }

        FinalResultPanel.Instance.Show(totalScore, grade, failureMessages);
    }

    private string GetGrade(int score)
    {
        if (score >= 90) return "A";
        else if (score >= 75) return "B";
        else if (score >= 60) return "C";
        else if (score >= 40) return "D";
        else return "F";
    }

    [System.Serializable]
    public class Quest
    {
        public string Title;
        public string Description;
        public int ImportanceScore;
        public string FailureExplanation;
        public bool IsCompleted;

        public Quest(string title, string description, int score, string failureExplanation)
        {
            Title = title;
            Description = description;
            ImportanceScore = score;
            FailureExplanation = failureExplanation;
            IsCompleted = false;
        }
    }
}


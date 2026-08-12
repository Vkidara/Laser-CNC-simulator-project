
using UnityEngine;
using System.Collections;

public class LaserMachineController : MonoBehaviour, IInteractable
{
    public GameObject machineSettingsPanel;
    public PlayerMovement playerMovement;
    public GameObject questHintText;
    public QuestMenuController questMenuController;
    public Transform workpiecePlacePoint;

    public GameObject currentWorkpiece = null;

    public GameObject presetSelectionPanel; // 🔹 Панель выбора материала

    public ModeSelectionUI modeSelectionUI;

    public LaserCuttingController cuttingController;

    private static LaserMachineController instance;
    public static bool isInMachineSettings = false;

    private float engravingPower;
    private float engravingSpeed;
    private int engravingPasses;

    private bool hasCompletedEngravingQuest = false;
    private QuestManager questManager;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GameObject qm = GameObject.FindWithTag("QuestManager");
        if (qm != null)
            questManager = qm.GetComponent<QuestManager>();
    }

    public string GetInteractionText() => "Открыть интерфейс станка";

    public void Interact()
    {
        if (PlayerInventory.Instance == null || PlayerInventory.Instance.heldWorkpiece == null)
        {
            Debug.Log("Сначала возьмите заготовку!");
            return;
        }

        bool hasEngravingBlueprint = LaserBlueprintHolder.GetSelectedBlueprint() != null;
        bool hasCuttingJob = LaserJobManager.Instance.CurrentJob == LaserJobManager.JobType.Cutting &&
                             !string.IsNullOrEmpty(LaserJobManager.Instance.SelectedPattern);

        if (!hasEngravingBlueprint && !hasCuttingJob)
        {
            Debug.Log("Сначала выберите чертеж на ноутбуке!");
            return;
        }


        if (currentWorkpiece != null)
        {
            Debug.Log("В станке уже есть заготовка!");
            return;
        }

        PlaceWorkpieceOnMachine();

        // ➕ Добавляем: если выбрана резка — передаём заготовку и открываем настройки
        if (LaserJobManager.Instance.CurrentJob == LaserJobManager.JobType.Cutting)
        {
            if (cuttingController != null)
            {
                cuttingController.workpiece = currentWorkpiece;

                // 👉 Вместо вызова LaserCuttingUI сразу — открываем панель выбора режима
                if (modeSelectionUI != null && modeSelectionUI.modeSelectionPanel != null)
                {
                    modeSelectionUI.modeSelectionPanel.SetActive(true);
                }

                if (playerMovement != null)
                    playerMovement.SetCanMove(false);

                CursorManager.Instance.RegisterMenuOpened();
                isInMachineSettings = true;

                if (questHintText != null)
                    questHintText.SetActive(false);

                if (questMenuController != null)
                    questMenuController.CloseMenu();
            }

        }
        else
        {
            OpenMachineSettings(); // 👈 если не резка — обычная гравировка
        }
    }


    private void PlaceWorkpieceOnMachine()
    {
        GameObject workpiece = PlayerInventory.Instance.heldWorkpiece;

        if (workpiece != null && workpiecePlacePoint != null)
        {
            workpiece.SetActive(true);
            workpiece.transform.SetParent(workpiecePlacePoint);
            workpiece.transform.localPosition = Vector3.zero;
            workpiece.transform.localRotation = Quaternion.identity;

            currentWorkpiece = workpiece;
            PlayerInventory.Instance.heldWorkpiece = null;

            Debug.Log("Заготовка установлена на станок: " + workpiece.name);
        }
        else
        {
            Debug.LogWarning("Не удалось установить заготовку.");
        }
    }



    private void OpenMachineSettings()
    {
        if (presetSelectionPanel != null)
            modeSelectionUI.modeSelectionPanel.SetActive(true);

        if (playerMovement != null)
            playerMovement.SetCanMove(false);

        CursorManager.Instance.RegisterMenuOpened();
        isInMachineSettings = true;

        if (questHintText != null)
            questHintText.SetActive(false);

        if (questMenuController != null)
            questMenuController.CloseMenu();

        // 🔽 Используем SelectedPattern напрямую
        string blueprint = LaserJobManager.Instance.SelectedPattern;
        if (!string.IsNullOrEmpty(blueprint))
        {
            // выбран чертёж для резки — открываем панель выбора материала
            CuttingMaterialSelectionUI materialUI = FindFirstObjectByType<CuttingMaterialSelectionUI>();
            if (materialUI != null)
                materialUI.panel.SetActive(true);
            else
                Debug.LogWarning("Не найден CuttingMaterialSelectionUI!");
        }
    }



    public static void CloseMachineSettings()
    {
        if (instance != null)
        {
            if (instance.machineSettingsPanel != null)
                instance.machineSettingsPanel.SetActive(false);

            if (instance.modeSelectionUI != null)
                instance.modeSelectionUI.modeSelectionPanel.SetActive(false); // 👈 если UI выбора режима не выключается

            if (instance.playerMovement != null)
                instance.playerMovement.SetCanMove(true);

            if (instance.questHintText != null)
                instance.questHintText.SetActive(true);
        }

        CursorManager.Instance.RegisterMenuClosed();
        isInMachineSettings = false;
    }


    public void SetEngravingParameters(float power, float speed, int passes)
    {
        engravingPower = power;
        engravingSpeed = speed;
        engravingPasses = passes;
    }

    public void StartEngraving()
    {
        if (currentWorkpiece == null)
        {
            Debug.LogWarning("Нет заготовки для гравировки");
            return;
        }

        var blueprint = LaserBlueprintHolder.GetSelectedBlueprint();
        if (blueprint == null || blueprint.engravingSprite == null)
        {
            Debug.LogWarning("Чертеж не выбран или не содержит гравируемый спрайт");
            return;
        }

        string effect = EvaluateEngravingEffect(engravingPower, engravingSpeed);
        StartCoroutine(EngravingCoroutine(currentWorkpiece, blueprint.engravingSprite, effect));
    }

    private IEnumerator EngravingCoroutine(GameObject workpiece, Sprite sprite, string effectType)
    {
        var engravingSurface = workpiece.GetComponentInChildren<WorkpieceEngravingSurface>();
        if (engravingSurface == null)
        {
            Debug.LogWarning("Не найден компонент EngravingSurface на заготовке.");
            yield break;
        }

        LaserBeamEffect laserEffect = GetComponent<LaserBeamEffect>();
        if (laserEffect != null)
            laserEffect.StartLaser(engravingPower, engravingSpeed, effectType);

        // ⏱ Начинаем одновременно луч и гравировку
        engravingSurface.ApplyEngraving(sprite, effectType, revealDuration: 5f);  // 👈 добавим revealDuration как параметр

        yield return new WaitForSeconds(5f);  // длительность луча и проявления

        if (laserEffect != null)
            laserEffect.StopLaser();

        float engravingDuration = 7f * engravingPasses;
        float remainingTime = engravingDuration - 5f;
        if (remainingTime > 0f)
            yield return new WaitForSeconds(remainingTime);

        engravingSurface.StopSmokeEffect();

        Debug.Log("Гравировка завершена.");

        if (!hasCompletedEngravingQuest && questManager != null)
        {
            questManager.CompleteQuest(7, true);
            hasCompletedEngravingQuest = true;

            // 🔽 Учитываем качество гравировки
            questManager.SetResultQuality(effectType);
        }

    }



    private string EvaluateEngravingEffect(float power, float speed)
    {
        if (currentWorkpiece == null)
            return "none";

        WorkpieceItem workpieceItem = currentWorkpiece.GetComponent<WorkpieceItem>();
        if (workpieceItem == null)
        {
            Debug.LogWarning("Не удалось получить тип материала заготовки.");
            return "default";
        }

        WorkpieceItem.MaterialType material = workpieceItem.materialType;

        float energyDensity = power / speed;

        switch (material)
        {
            case WorkpieceItem.MaterialType.Wood:
                if (energyDensity < 0.05f)
                    return "none";
                else if (energyDensity < 0.2f)
                    return "faint";
                else if (energyDensity < 0.3f)
                    return "default";
                else if (energyDensity < 0.5f)
                    return "burn";
                else
                    return "overburn";

            case WorkpieceItem.MaterialType.Metal:
                if (energyDensity < 0.1f)
                    return "none";
                else if (energyDensity < 0.4f)
                    return "faint";
                else if (energyDensity < 1.0f)
                    return "default";
                else if (energyDensity < 1.6f)
                    return "burn";
                else
                    return "overburn";

            default:
                return "default";
        }
    }

}

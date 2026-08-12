using UnityEngine;
using System.Collections;

public class LaserCuttingController : MonoBehaviour
{
    private string selectedPattern;
    private WorkpieceItem.MaterialType selectedMaterial;

    private float power;
    private float speed;
    private int passes;

    public GameObject laserEffectOrigin;
    public LineRenderer laserLine;
    public GameObject workpiece;
    public GameObject cuttingSettingsPanel;

    [SerializeField] private CuttingPatternDatabase cuttingPatternDatabase;
    [SerializeField] private CuttingPresetDatabase cuttingPresetDatabase;

    private Sprite currentPatternSprite;
    private Material workpieceMaterialInstance;

    public void SetCuttingPattern(string patternName)
    {
        selectedPattern = patternName;
        Debug.Log("Выбран чертеж для резки: " + selectedPattern);
        OpenCuttingSettings();
    }

    private void OpenCuttingSettings()
    {
        if (cuttingSettingsPanel != null)
        {
            cuttingSettingsPanel.SetActive(true);
            PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
            if (player != null)
                player.SetCanMove(false);

            CursorManager.Instance.RegisterMenuOpened();
            LaserMachineController.isInMachineSettings = true;
        }
        else
        {
            Debug.LogWarning("cuttingSettingsPanel не назначена!");
        }
    }

    public void SetMaterial(WorkpieceItem.MaterialType material)
    {
        selectedMaterial = material;
    }

    public void SetCuttingParameters(float power, float speed, int passes)
    {
        this.power = power;
        this.speed = speed;
        this.passes = passes;
    }

    public void StartCutting()
    {
        if (workpiece == null) return;

        currentPatternSprite = cuttingPatternDatabase.GetPatternByName(selectedPattern);
        if (currentPatternSprite == null) return;

        ApplyPatternToWorkpiece();

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null)
            player.SetCanMove(true);

        CursorManager.Instance.RegisterMenuClosed();
        cuttingSettingsPanel.SetActive(false);
        LaserMachineController.isInMachineSettings = false;

        Debug.Log($"▶ Начинаем резку: {selectedPattern}, Материал: {selectedMaterial}, Power={power}, Speed={speed}, Passes={passes}");
        StartCoroutine(CuttingCoroutine());
    }

    private IEnumerator CuttingCoroutine()
    {
        EnableLaserEffect();
        float cuttingTime = 5f;
        yield return new WaitForSeconds(cuttingTime);
        DisableLaserEffect();

        var resultType = GetCuttingResultType();
        Debug.Log("✅ Резка завершена: " + resultType);

        if (workpiece != null)
        {
            var visualHandler = workpiece.GetComponent<CuttingVisualHandler>();
            if (visualHandler != null)
                visualHandler.ShowCutResult(selectedPattern, selectedMaterial, resultType.ToString());

            var debrisHandler = workpiece.GetComponent<CuttingDebrisHandler>();
            if (debrisHandler != null)
            {
                debrisHandler.HideAllDebris();
                debrisHandler.ShowDebris(selectedPattern, selectedMaterial, resultType);
            }
        }

        LaserJobManager.Instance.ResetJob();
        // 🔽 Передаём качество реза в систему квестов
        QuestManager qm = FindFirstObjectByType<QuestManager>();
        if (qm != null)
        {
            qm.SetResultQuality(resultType.ToString().ToLower());
        }

    }


    private CuttingVisualHandler.CutQuality GetCuttingResultType()
    {
        var preset = cuttingPresetDatabase.GetPreset(selectedMaterial);
        if (preset == null) return CuttingVisualHandler.CutQuality.Undercut;

        float energy = (power * passes) / Mathf.Max(0.01f, speed);
        if (energy < preset.minEnergy) return CuttingVisualHandler.CutQuality.Undercut;
        if (energy > preset.maxEnergy) return CuttingVisualHandler.CutQuality.Burned;
        return CuttingVisualHandler.CutQuality.Perfect;
    }


    private void ApplyPatternToWorkpiece()
    {
        var visualHandler = workpiece.GetComponent<CuttingVisualHandler>();
        if (visualHandler != null) visualHandler.ResetVisuals();

        if (workpiece == null || currentPatternSprite == null) return;

        Renderer rend = workpiece.GetComponent<Renderer>();
        if (rend != null)
        {
            workpieceMaterialInstance = new Material(rend.material);
            workpieceMaterialInstance.mainTexture = currentPatternSprite.texture;
            rend.material = workpieceMaterialInstance;
        }
    }

    private void EnableLaserEffect()
    {
        if (laserLine != null && laserEffectOrigin != null)
        {
            laserLine.enabled = true;
            laserLine.SetPosition(0, laserEffectOrigin.transform.position);
            laserLine.SetPosition(1, laserEffectOrigin.transform.position + laserEffectOrigin.transform.forward * 1f);
        }
    }

    private void DisableLaserEffect()
    {
        if (laserLine != null)
            laserLine.enabled = false;
    }



}
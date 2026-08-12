using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class LaserSettingsUI : MonoBehaviour
{
    public Slider powerSlider;
    public TMP_Text powerValueText;

    public Slider speedSlider;
    public TMP_Text speedValueText;

    public Slider passesSlider;
    public TMP_Text passesValueText;

    public LaserMachineController machineController;

    private float selectedPower;
    private float selectedSpeed;
    private int selectedPasses;
    private WorkpieceItem.MaterialType selectedMaterial;

    private class MaterialProfile
    {
        public float minPower, maxPower;
        public float minSpeed, maxSpeed;
        public int minPasses, maxPasses;

        public MaterialProfile(float minP, float maxP, float minS, float maxS, int minPa, int maxPa)
        {
            minPower = minP;
            maxPower = maxP;
            minSpeed = minS;
            maxSpeed = maxS;
            minPasses = minPa;
            maxPasses = maxPa;
        }
    }

    private readonly Dictionary<WorkpieceItem.MaterialType, MaterialProfile> materialProfiles = new()
    {
        { WorkpieceItem.MaterialType.Wood, new MaterialProfile(10, 60, 50, 400, 1, 3) },
        { WorkpieceItem.MaterialType.Metal, new MaterialProfile(60, 100, 20, 300, 1, 3) }
    };

    void OnEnable()
    {
        CursorManager.Instance.RegisterMenuOpened();
    }

    void OnDisable()
    {
        CursorManager.Instance.RegisterMenuClosed();
        CursorManager.Instance.RegisterMenuClosed();
    }

    public void ApplyPreset(LaserPreset preset)
    {
        if (powerSlider != null)
        {
            powerSlider.minValue = preset.MinPower;
            powerSlider.maxValue = preset.MaxPower;
            powerSlider.value = preset.MinPower;
        }

        if (speedSlider != null)
        {
            speedSlider.minValue = preset.MinSpeed;
            speedSlider.maxValue = preset.MaxSpeed;
            speedSlider.value = preset.MinSpeed;
        }

        if (passesSlider != null)
        {
            passesSlider.minValue = preset.MinPasses;
            passesSlider.maxValue = preset.MaxPasses;
            passesSlider.value = preset.MinPasses;
        }

        SetMaterial((WorkpieceItem.MaterialType)preset.MaterialType);

    }


    public void SetMaterial(WorkpieceItem.MaterialType material)
    {
        selectedMaterial = material;
        UpdateSliderRanges();
    }

    void Update()
    {
        selectedPower = powerSlider.value;
        powerValueText.text = selectedPower.ToString("F0");

        selectedSpeed = speedSlider.value;
        speedValueText.text = selectedSpeed.ToString("F0");

        selectedPasses = (int)passesSlider.value;
        passesValueText.text = selectedPasses.ToString();
    }

    void UpdateSliderRanges()
    {
        var profile = materialProfiles[selectedMaterial];

        powerSlider.minValue = profile.minPower;
        powerSlider.maxValue = profile.maxPower;
        powerSlider.value = profile.minPower;

        speedSlider.minValue = profile.minSpeed;
        speedSlider.maxValue = profile.maxSpeed;
        speedSlider.value = profile.minSpeed;

        passesSlider.minValue = profile.minPasses;
        passesSlider.maxValue = profile.maxPasses;
        passesSlider.value = profile.minPasses;
    }

    public void OnStartEngravingPressed()
    {
        if (machineController != null)
        {
            machineController.SetEngravingParameters(selectedPower, selectedSpeed, selectedPasses);
            machineController.StartEngraving();
            CloseUI();
            LaserMachineController.CloseMachineSettings();
        }
    }

    public void OnCancelPressed()
    {
        CloseUI();
        LaserMachineController.CloseMachineSettings();
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}

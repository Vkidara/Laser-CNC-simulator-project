using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LaserCuttingUI : MonoBehaviour
{
    public Slider powerSlider;
    public TMP_Text powerValueText;

    public Slider speedSlider;
    public TMP_Text speedValueText;

    public Slider passesSlider;
    public TMP_Text passesValueText;

    public LaserCuttingController cuttingController;
    public CuttingPresetDatabase presetDatabase;

    private bool isMenuOpen = false;

    void Update()
    {
        powerValueText.text = powerSlider.value.ToString("F0");
        speedValueText.text = speedSlider.value.ToString("F0");
        passesValueText.text = passesSlider.value.ToString("F0");
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
        if (!isMenuOpen)
        {
            CursorManager.Instance.RegisterMenuOpened();
            isMenuOpen = true;
        }
    }

    public void CloseUI()
    {
        CursorManager.Instance.RegisterMenuClosed();
        CursorManager.Instance.RegisterMenuClosed();
        gameObject.SetActive(false);
    }

    public void OnStartCuttingPressed()
    {
        cuttingController.SetCuttingParameters(
            powerSlider.value,
            speedSlider.value,
            (int)passesSlider.value
        );

        cuttingController.StartCutting();
        CloseUI();
    }

    public void OnCancelPressed()
    {
        CloseUI();
    }

    public void SetMaterial(WorkpieceItem.MaterialType material)
    {
        if (cuttingController != null)
        {
            cuttingController.SetMaterial(material);
        }

        if (presetDatabase != null)
        {
            var preset = presetDatabase.GetPreset(material);
            if (preset != null)
            {
                // Обновляем диапазоны слайдеров согласно материалу
                powerSlider.minValue = preset.minPower;
                powerSlider.maxValue = preset.maxPower;

                speedSlider.minValue = preset.minSpeed;
                speedSlider.maxValue = preset.maxSpeed;

                passesSlider.minValue = preset.minPasses;
                passesSlider.maxValue = preset.maxPasses;
                passesSlider.wholeNumbers = true;

                // Также можно сбрасывать значения слайдеров в пределах допустимого
                powerSlider.value = preset.minPower;
                speedSlider.value = preset.minSpeed;
                passesSlider.value = preset.minPasses;
            }
            else
            {
                Debug.LogWarning("Пресет для материала не найден.");
            }
        }
        else
        {
            Debug.LogWarning("PresetDatabase не назначен в LaserCuttingUI.");
        }
    }
}

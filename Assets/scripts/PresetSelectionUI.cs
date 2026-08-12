using UnityEngine;

public class PresetSelectionUI : MonoBehaviour
{
    public GameObject presetSelectionPanel;
    public LaserSettingsUI settingsUI;

    public void SelectWoodPreset()
    {
        ApplyPreset(LaserPresetsDatabase.GetPreset(MaterialType.Wood));
    }

    public void SelectMetalPreset()
    {
        ApplyPreset(LaserPresetsDatabase.GetPreset(MaterialType.Metal));
    }

    void ApplyPreset(LaserPreset preset)
    {
        if (settingsUI != null)
        {
            settingsUI.ApplyPreset(preset);
            settingsUI.OpenUI(); // Открытие с управлением курсором
        }

        presetSelectionPanel.SetActive(false);
    }

    public void OnCancelPressed()
    {
        presetSelectionPanel.SetActive(false);
        CursorManager.Instance.RegisterMenuClosed();
        LaserMachineController.CloseMachineSettings();
    }
}

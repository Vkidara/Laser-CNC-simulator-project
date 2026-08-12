using UnityEngine;

public class ModeSelectionUI : MonoBehaviour
{
    public GameObject modeSelectionPanel;
    public GameObject presetSelectionPanel;  // для гравировки
    public GameObject cutSelectionPanel;     // для резки

    public void OnEngravingSelected()
    {
        modeSelectionPanel.SetActive(false);
        presetSelectionPanel.SetActive(true);
        CursorManager.Instance.RegisterMenuOpened();
    }

    public void OnCuttingSelected()
    {
        modeSelectionPanel.SetActive(false);

        CuttingMaterialChoiceUI materialUI = FindAnyObjectByType<CuttingMaterialChoiceUI>();
        if (materialUI != null)
        {
            materialUI.panel.SetActive(true);
            // ❌ Убираем лишний вызов:
            // CursorManager.Instance.RegisterMenuOpened();
        }
        else
        {
            Debug.LogWarning("Не найден CuttingMaterialChoiceUI!");
        }
    }


    public void OnCancelPressed()
    {
        modeSelectionPanel.SetActive(false);
        CursorManager.Instance.RegisterMenuClosed();
        LaserMachineController.CloseMachineSettings();
    }
}

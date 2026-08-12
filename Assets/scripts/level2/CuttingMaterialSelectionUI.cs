using UnityEngine;

public class CuttingMaterialChoiceUI : MonoBehaviour
{
    public GameObject panel;
    public LaserCuttingUI cuttingUI;

    public void SelectWood()
    {
        cuttingUI.SetMaterial(WorkpieceItem.MaterialType.Wood);
        cuttingUI.cuttingController.SetCuttingPattern(LaserJobManager.Instance.SelectedPattern);

        panel.SetActive(false);
        cuttingUI.OpenUI(); // теперь курсором займётся LaserCuttingUI сам
                            // ❌ НЕ вызываем CursorManager здесь
    }


    public void SelectMetal()
    {
        cuttingUI.SetMaterial(WorkpieceItem.MaterialType.Metal);
        cuttingUI.cuttingController.SetCuttingPattern(LaserJobManager.Instance.SelectedPattern);

        panel.SetActive(false);
        cuttingUI.OpenUI(); // тоже здесь курсором управляет другой UI
    }


    public void OnCancelPressed()
    {
        panel.SetActive(false);
        CursorManager.Instance.RegisterMenuClosed();
        LaserJobManager.Instance.ResetJob();
        LaserMachineController.CloseMachineSettings();
    }
}

using UnityEngine;

public class CuttingMaterialSelectionUI : MonoBehaviour
{
    public GameObject panel;
    public LaserCuttingUI cuttingUI;

    public void SelectWood()
    {
        cuttingUI.SetMaterial(WorkpieceItem.MaterialType.Wood);
        ClosePanel();
    }

    public void SelectMetal()
    {
        cuttingUI.SetMaterial(WorkpieceItem.MaterialType.Metal);
        ClosePanel();
    }

    public void OnCancelPressed()
    {
        ClosePanel();
        LaserMachineInteract.CloseMachineMenu();
    }

    private void ClosePanel()
    {
        panel.SetActive(false);
    }
}


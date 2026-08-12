using UnityEngine;

public class DrawingTypeSelectionUI : MonoBehaviour
{
    public GameObject typeSelectionPanel;       // Панель выбора типа чертежа
    public GameObject engravingDrawingPanel;    // Панель с чертежами гравировки
    public GameObject cuttingDrawingPanel;      // Панель с чертежами резки

    public void OnEngravingDrawingsSelected()
    {
        typeSelectionPanel.SetActive(false);
        engravingDrawingPanel.SetActive(true);
    }

    public void OnCuttingDrawingsSelected()
    {
        typeSelectionPanel.SetActive(false);
        cuttingDrawingPanel.SetActive(true);
    }

    public void OnCancelPressed()
    {
        typeSelectionPanel.SetActive(false);
        LaserMachineInteract.CloseMachineMenu();
    }
}


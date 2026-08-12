using UnityEngine;
using UnityEngine.UI;

public class CuttingDrawingSelectionUI : MonoBehaviour
{
    public GameObject panel;

    [Header("Preview UI")]
    public Image previewImage;
    public Sprite squarePreview;
    public Sprite circlePreview;
    public Sprite customPreview;

    public void SelectSquareCut()
    {
        LaserJobManager.Instance.SetCuttingJob("Square");

        QuestManager qm = FindFirstObjectByType<QuestManager>();
        if (qm != null)
            qm.CompleteQuest(6, true); // ✅ индекс зависит от порядка инициализации

        ClosePanelAndRestore();
    }

    public void SelectCircleCut()
    {
        LaserJobManager.Instance.SetCuttingJob("Circle");

        QuestManager qm = FindFirstObjectByType<QuestManager>();
        if (qm != null)
            qm.CompleteQuest(6, true); // индекс тот же

        ClosePanelAndRestore();
    }

    public void SelectCustomCut()
    {
        LaserJobManager.Instance.SetCuttingJob("Custom");

        QuestManager qm = FindFirstObjectByType<QuestManager>();
        if (qm != null)
            qm.CompleteQuest(6, true);

        ClosePanelAndRestore();
    }

    public void OnCancelPressed()
    {
        LaserJobManager.Instance.ResetJob();
        ClosePanelAndRestore();
    }

    private void ClosePanelAndRestore()
    {
        panel.SetActive(false);
        FindFirstObjectByType<PlayerMovement>().SetCanMove(true);
        FindFirstObjectByType<PlayerInteraction>().SetInteractionBlocked(false);
        CursorManager.Instance.RegisterMenuClosed();
    }

    public void ShowPreview(string type)
    {
        if (previewImage == null) return;

        switch (type.ToLower())
        {
            case "square": previewImage.sprite = squarePreview; break;
            case "circle": previewImage.sprite = circlePreview; break;
            case "custom": previewImage.sprite = customPreview; break;
        }

        previewImage.gameObject.SetActive(true);
    }

    public void HidePreview()
    {
        if (previewImage != null)
            previewImage.gameObject.SetActive(false);
    }
}


using UnityEngine;

public class InspectionPoint : MonoBehaviour, IInteractable
{
    public string inspectionName;
    private bool inspected = false;
    private bool hasProblem = false;
    private InspectionIssue foundIssue;

    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    public string GetInteractionText()
    {
        return inspected ? null : "Осмотреть: " + inspectionName;
    }

    public void Interact()
    {
        if (inspected) return;

        inspected = true;

        // Отключаем коллайдер, чтобы больше нельзя было осматривать
        if (_collider != null)
            _collider.enabled = false;

        // 50% шанс на проблему
        if (Random.value < 0.5f)
        {
            hasProblem = true;
            foundIssue = QuestManager.Instance.GetIssueFor(inspectionName);
        }

        if (hasProblem && foundIssue != null)
        {
            InspectionDialog.Show(
                foundIssue.problemDescription,
                foundIssue.fixInstruction,
                () => QuestManager.Instance.RegisterInspection(inspectionName, true)
            );
        }
        else
        {
            InspectionDialog.Show(
                "Проблем не обнаружено.",
                "Продолжить",
                () => QuestManager.Instance.RegisterInspection(inspectionName, false)
            );
        }
    }
}



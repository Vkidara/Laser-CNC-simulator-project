using UnityEngine;

public class WorkpieceReturnPoint : MonoBehaviour, IInteractable
{
    public string GetInteractionText()
    {
        return "Положить заготовку обратно";
    }

    public void Interact()
    {
        PlayerInventory.Instance.ReturnWorkpieceToOriginalPlace();
    }
}


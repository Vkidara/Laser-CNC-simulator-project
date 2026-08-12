using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public LayerMask interactableLayer;
    public Text interactionText;

    private Camera playerCamera;
    private IInteractable currentInteractable;

    private bool interactionBlocked = false; // 🆕 Добавлено

    void Start()
    {
        playerCamera = Camera.main;
        if (interactionText != null)
            interactionText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (interactionBlocked || LaserMachineController.isInMachineSettings || ManualBookUI.IsOpen || BlueprintSelectorUI.IsOpen)
        {
            HideInteractionText();
            return;
        }

        CheckForInteractable();

        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact();
        }
    }

    void CheckForInteractable()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                currentInteractable = interactable;
                ShowInteractionText("[E] " + interactable.GetInteractionText());
                return;
            }
        }

        currentInteractable = null;
        HideInteractionText();
    }

    void ShowInteractionText(string message)
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(true);
            interactionText.text = message;
        }
    }

    void HideInteractionText()
    {
        if (interactionText != null)
            interactionText.gameObject.SetActive(false);
    }

    // 🆕 Позволяет другим скриптам блокировать взаимодействие
    public void SetInteractionBlocked(bool blocked)
    {
        interactionBlocked = blocked;

        if (blocked)
            HideInteractionText(); // сразу убираем, если нужно
    }
}

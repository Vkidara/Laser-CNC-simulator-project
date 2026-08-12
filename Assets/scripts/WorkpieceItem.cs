using UnityEngine;

public class WorkpieceItem : MonoBehaviour, IInteractable
{
    public enum MaterialType { Wood, Metal }
    public MaterialType materialType;

    [HideInInspector] public Transform originalPlacePoint;

    private bool initialized = false;

    void Start()
    {
        // Сохраняем оригинальную точку при старте, только один раз
        if (!initialized && originalPlacePoint == null && transform.parent != null)
        {
            originalPlacePoint = transform.parent;
            initialized = true;
            Debug.Log("Сохранили оригинальную точку: " + originalPlacePoint.name);
        }
    }

    public string GetInteractionText()
    {
        return "Взять заготовку";
    }

    public void Interact()
    {
        if (PlayerInventory.Instance != null)
        {
            Debug.Log("Пробую подобрать заготовку: " + this.gameObject.name);

            PlayerInventory.Instance.PickupWorkpiece(this.gameObject);

            transform.SetParent(null);

            Debug.Log("Содержимое после подбора: " + PlayerInventory.Instance.heldWorkpiece);
        }
    }
}


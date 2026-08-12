using UnityEngine;
using System.Collections.Generic;

public class CuttingDebrisHandler : MonoBehaviour, IInteractable
{
    [System.Serializable]
    public class DebrisVariant
    {
        public string patternName;
        public WorkpieceItem.MaterialType materialType;
        public CuttingVisualHandler.CutQuality cutQuality;
        public GameObject debrisObject;
    }

    public List<DebrisVariant> debrisVariants;

    public string GetInteractionText()
    {
        foreach (var variant in debrisVariants)
        {
            if (variant.debrisObject != null && variant.debrisObject.activeSelf)
                return "Убрать остатки";
        }

        return "";
    }

    public void Interact()
    {
        foreach (var variant in debrisVariants)
        {
            if (variant.debrisObject != null && variant.debrisObject.activeSelf)
            {
                variant.debrisObject.SetActive(false);
                Debug.Log("Остатки удалены.");
            }
        }
    }

    public void ShowDebris(string pattern, WorkpieceItem.MaterialType material, CuttingVisualHandler.CutQuality quality)
    {
        foreach (var variant in debrisVariants)
        {
            bool patternMatch = variant.patternName.Trim().ToLower() == pattern.Trim().ToLower();
            bool materialMatch = variant.materialType == material;
            bool qualityMatch = variant.cutQuality == quality;

            if (patternMatch && materialMatch && qualityMatch && variant.debrisObject != null)
            {
                variant.debrisObject.SetActive(true);
            }
        }
    }

    public void HideAllDebris()
    {
        foreach (var variant in debrisVariants)
        {
            if (variant.debrisObject != null)
                variant.debrisObject.SetActive(false);
        }
    }
}


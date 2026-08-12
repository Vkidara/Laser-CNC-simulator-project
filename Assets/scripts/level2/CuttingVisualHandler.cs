using UnityEngine;
using System.Collections.Generic;

public class CuttingVisualHandler : MonoBehaviour
{
    public enum CutQuality
    {
        Undercut,
        Perfect,
        Burned
    }

    [System.Serializable]
    public class CutVariant
    {
        public string patternName;
        public WorkpieceItem.MaterialType materialType;
        public CutQuality cutQuality;
        public GameObject visualObject;
    }

    public GameObject originalVisual;
    public List<CutVariant> cutVariants;
    private string currentPattern;

    private void Awake()
    {
        ResetVisuals();
    }

    public void ShowCutResult(string pattern, WorkpieceItem.MaterialType material, string resultType)
    {
        ResetVisuals();
        foreach (var variant in cutVariants)
        {
            bool patternMatch = variant.patternName.Trim().ToLower() == pattern.Trim().ToLower();
            bool materialMatch = variant.materialType == material;
            bool qualityMatch = variant.cutQuality.ToString().ToLower() == resultType.ToLower();

            if (patternMatch && materialMatch && qualityMatch && variant.visualObject != null)
            {
                if (originalVisual != null)
                    originalVisual.SetActive(false);

                variant.visualObject.SetActive(true);
                currentPattern = pattern;
                return;
            }
        }

        Debug.LogWarning($"Нет подходящего визуала: Pattern={pattern}, Material={material}, Result={resultType}");
    }

    public void ResetVisuals()
    {
        if (originalVisual != null)
            originalVisual.SetActive(true);

        foreach (var variant in cutVariants)
        {
            if (variant.visualObject != null)
                variant.visualObject.SetActive(false);
        }

        currentPattern = null;
    }

    public string GetCurrentPattern()
    {
        return currentPattern;
    }
}
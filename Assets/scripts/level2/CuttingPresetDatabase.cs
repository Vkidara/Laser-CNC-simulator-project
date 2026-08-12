using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MaterialCutPreset
{
    public WorkpieceItem.MaterialType materialType;

    // Диапазоны энергии (для определения качества реза)
    public float minEnergy;
    public float maxEnergy;

    // Диапазоны параметров для UI
    public float minPower;
    public float maxPower;

    public float minSpeed;
    public float maxSpeed;

    public int minPasses;
    public int maxPasses;
}

[CreateAssetMenu(menuName = "Laser/CuttingPresetDatabase")]
public class CuttingPresetDatabase : ScriptableObject
{
    public List<MaterialCutPreset> presets;

    public MaterialCutPreset GetPreset(WorkpieceItem.MaterialType type)
    {
        return presets.Find(p => p.materialType == type);
    }
}

using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CuttingPatternDatabase", menuName = "Laser/Cutting Pattern Database")]
public class CuttingPatternDatabase : ScriptableObject
{
    [System.Serializable]
    public class PatternEntry
    {
        public string patternName;
        public Sprite cuttingSprite;
    }

    public List<PatternEntry> patterns = new List<PatternEntry>();

    private Dictionary<string, Sprite> patternLookup;

    private void OnEnable()
    {
        if (patternLookup == null)
            InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        patternLookup = new Dictionary<string, Sprite>();
        foreach (var entry in patterns)
        {
            if (!string.IsNullOrEmpty(entry.patternName) && entry.cuttingSprite != null)
            {
                if (!patternLookup.ContainsKey(entry.patternName))
                {
                    patternLookup.Add(entry.patternName, entry.cuttingSprite);
                }
            }
        }
    }

    /// <summary>
    /// Получить спрайт чертежа по имени.
    /// </summary>
    public Sprite GetPatternByName(string patternName)
    {
        if (patternLookup == null || patternLookup.Count == 0)
            InitializeDictionary();

        if (patternLookup.TryGetValue(patternName, out Sprite sprite))
            return sprite;

        Debug.LogWarning($"Не найден чертеж по имени: {patternName}");
        return null;
    }
}



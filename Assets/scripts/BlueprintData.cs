using UnityEngine;

[CreateAssetMenu(fileName = "NewBlueprint", menuName = "Laser/Blueprint")]
public class BlueprintData : ScriptableObject
{
    public string blueprintName;
    public Sprite icon;
    public Sprite previewImage;

    [Header("Чертеж для гравировки")]
    public Sprite engravingSprite; // ← вот это добавляем
}




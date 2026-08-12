using UnityEngine;

public class LaserBlueprintHolder : MonoBehaviour
{
    private static BlueprintData selectedBlueprint; // Сделаем поле static

    public void SetSelectedBlueprint(BlueprintData blueprint)
    {
        selectedBlueprint = blueprint;
        Debug.Log("Выбран чертеж: " + blueprint.blueprintName);
    }

    public static BlueprintData GetSelectedBlueprint()
    {
        return selectedBlueprint;
    }
}



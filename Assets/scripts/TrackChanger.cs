using UnityEngine;

public class TrackChanger : MonoBehaviour, IInteractable
{
    public Radio radio;

    public void ChangeTrack()
    {
        if (radio != null)
        {
            radio.NextTrack();
        }
    }

    // >>> Реализация IInteractable
    public string GetInteractionText()
    {
        if (radio != null && radio.IsRadioOn())
        {
            return "Переключить трек";
        }
        else
        {
            return ""; // Если радио выключено, подсказку не показываем
        }
    }

    public void Interact()
    {
        if (radio != null && radio.IsRadioOn())
        {
            ChangeTrack();
        }
    }
}

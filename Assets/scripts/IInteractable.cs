using UnityEngine;

public interface IInteractable
{
    string GetInteractionText(); // Метод для получения текста взаимодействия
    void Interact(); // Метод, который выполняет действие
}

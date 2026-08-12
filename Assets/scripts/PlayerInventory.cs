using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public GameObject heldWorkpiece = null;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void PickupWorkpiece(GameObject workpiece)
    {
        if (heldWorkpiece != null)
        {
            Debug.Log("Вы уже держите заготовку!");
            return;
        }

        // Очистить ссылку станка
        LaserMachineController machine = workpiece.GetComponentInParent<LaserMachineController>();
        if (machine != null && machine.currentWorkpiece == workpiece)
        {
            machine.currentWorkpiece = null;
            Debug.Log("Заготовка снята со станка.");
        }

        heldWorkpiece = workpiece;
        workpiece.SetActive(false); // Скрываем из мира
        Debug.Log("Заготовка поднята!");
    }


    public void PlaceWorkpiece(Transform placePoint)
    {
        if (heldWorkpiece == null)
        {
            Debug.Log("Нет заготовки в руках!");
            return;
        }

        // ПЕРВОЕ - активировать объект!
        heldWorkpiece.SetActive(true);
        Debug.Log("Активировали " + heldWorkpiece.name);

        // Потом только менять позицию и родителя
        heldWorkpiece.transform.position = placePoint.position;
        heldWorkpiece.transform.rotation = placePoint.rotation;
        heldWorkpiece.transform.SetParent(placePoint);

        // Привязать к станку
        LaserMachineController machine = placePoint.GetComponentInParent<LaserMachineController>();
        if (machine != null)
        {
            machine.currentWorkpiece = heldWorkpiece;
        }

        // Очистить руки
        heldWorkpiece = null;

        Debug.Log("Заготовка успешно размещена и активирована!");
    }

    public void ReturnWorkpieceToOriginalPlace()
    {
        if (heldWorkpiece == null)
        {
            Debug.Log("Нет заготовки для возврата!");
            return;
        }

        WorkpieceItem item = heldWorkpiece.GetComponent<WorkpieceItem>();
        if (item != null && item.originalPlacePoint != null)
        {
            heldWorkpiece.SetActive(true);
            heldWorkpiece.transform.SetParent(item.originalPlacePoint);
            heldWorkpiece.transform.localPosition = Vector3.zero;
            heldWorkpiece.transform.localRotation = Quaternion.identity;

            Debug.Log("Заготовка возвращена на исходную позицию!");

            // ✅ Проверяем наличие результатов резки
            CuttingVisualHandler handler = heldWorkpiece.GetComponent<CuttingVisualHandler>();
            if (handler != null && !string.IsNullOrEmpty(handler.GetCurrentPattern()))
            {
                QuestManager qm = FindFirstObjectByType<QuestManager>();
                if (qm != null)
                    qm.CompleteQuest(7, true); // индекс "Выполнить резку"
            }

            heldWorkpiece = null;
        }
        else
        {
            Debug.Log("Не найдена исходная позиция для возврата.");
        }
    }




    public void DropHeldWorkpiece()
    {
        heldWorkpiece = null;
    }


}


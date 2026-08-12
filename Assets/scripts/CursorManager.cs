using UnityEngine;
using System.Collections.Generic;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    private int openMenusCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Живёт между сценами
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterMenuOpened()
    {
        openMenusCount++;
        UpdateCursorState();
    }

    public void RegisterMenuClosed()
    {
        openMenusCount = Mathf.Max(0, openMenusCount - 1);
        UpdateCursorState();
    }

    private void UpdateCursorState()
    {
        Debug.Log($"[CursorManager] openMenusCount = {openMenusCount}");

        if (openMenusCount > 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    // На всякий случай можно сразу явно
    public void ForceUnlockCursor()
    {
        openMenusCount = 1;
        UpdateCursorState();
    }

    public void ForceLockCursor()
    {
        openMenusCount = 0;
        UpdateCursorState();
    }
}


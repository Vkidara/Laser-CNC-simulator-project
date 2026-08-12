using UnityEngine;
using UnityEngine.SceneManagement;  // Для загрузки сцен
using UnityEngine.UI;              // Для UI компонентов

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;        // Ссылка на Canvas с меню (назначьте в инспекторе)
    public string mainMenuSceneName = "menu"; // Имя главной сцены, на которую нужно вернуться
    public GameObject player;            // Ссылка на персонажа (назначьте в инспекторе)
    private bool isMenuActive = false;
    private PlayerMovement playerMovement;  // Ссылка на скрипт управления персонажем

    void Start()
    {
        // Меню скрыто по умолчанию, и курсор не виден
        menuCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.None;  // Отключаем захват курсора
        Cursor.visible = false;  // Курсор не видим при старте игры

        // Получаем компонент player_movement
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Открытие меню при нажатии клавиши "+"
        if (Input.GetKeyDown(KeyCode.M))  // == KeyCode.Plus, но Equals удобнее для клавиатуры
        {
            ToggleMenu();
        }
    }

    // Метод для переключения состояния меню
    public void ToggleMenu()
    {
        isMenuActive = !isMenuActive;
        menuCanvas.SetActive(isMenuActive);  // Включаем/выключаем Canvas с меню

        if (isMenuActive)
        {
            Cursor.lockState = CursorLockMode.None;  // Отключаем захват курсора
            Cursor.visible = true;  // Курсор становится видимым

            // Останавливаем управление персонажем
            if (playerMovement != null)
                playerMovement.enabled = false;  // Отключаем скрипт player_movement
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;  // Включаем захват курсора
            Cursor.visible = false;  // Курсор снова скрывается

            // Включаем управление персонажем
            if (playerMovement != null)
                playerMovement.enabled = true;  // Включаем скрипт player_movement обратно
        }
    }

    // Метод для кнопки "Назад" — скрыть меню
    public void HideMenu()
    {
        isMenuActive = false;
        menuCanvas.SetActive(false);  // Скрыть меню

        Cursor.lockState = CursorLockMode.Locked;  // Включаем захват курсора
        Cursor.visible = false;  // Курсор снова скрывается

        // Включаем управление персонажем
        if (playerMovement != null)
            playerMovement.enabled = true;  // Включаем скрипт player_movement обратно
    }

    // Метод для кнопки "Выйти" — вернуться на главную сцену
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);  // Загружаем сцену с главным меню
    }
}

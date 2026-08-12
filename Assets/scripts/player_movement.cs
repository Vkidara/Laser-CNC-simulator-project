using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 600;
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;

    private float rotationX = 0f;
    private bool canMove = true; // 🆕 Добавили переменную

    void Update()
    {
        if (canMove)
        {
            Move();
            Look();
        }
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move * speed * Time.deltaTime;
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    // 🆕 Метод чтобы блокировать перемещение и вращение
    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}


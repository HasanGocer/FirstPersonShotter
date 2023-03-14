using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoSingleton<PlayerMovement>
{
    Joystick joystick; // Joystick nesnesi
    public float speed = 5f; // Hareket hýzý
    public float rotationSpeed = 100f; // Dönüþ hýzý
    [HideInInspector] public bool isUseJoystick;
    private Rigidbody rb;
    private Touch touch;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joystick = MainManager.Instance.joystick;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.gameStat == GameManager.GameStat.start)
        {
            // Joystick ile karakterin hareketi
            float horizontalInput = joystick.Horizontal;
            float verticalInput = joystick.Vertical;

            if (horizontalInput != 0 || verticalInput != 0) isUseJoystick = true;
            else isUseJoystick = false;
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
            MainManager.Instance.mainPlayer.transform.TransformDirection(movement);
            movement.Normalize();

            rb.MovePosition(rb.transform.localPosition + movement * speed * Time.fixedDeltaTime);

            // Joystick ile karakterin dönüþü
            float rotateInput = joystick.Horizontal;

            Vector3 rotation = new Vector3(0, rotateInput, 0);
            MainManager.Instance.mainPlayer.transform.TransformDirection(rotation);
            rb.MoveRotation(rb.transform.localRotation * Quaternion.Euler(rotation * rotationSpeed * Time.fixedDeltaTime));
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoSingleton<PlayerMovement>
{
    Joystick joystick; // Joystick nesnesi
    public float speed = 5f; // Hareket h�z�
    public float rotationSpeed = 100f; // D�n�� h�z�
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

        // Joystick ile karakterin hareketi
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        if (horizontalInput != 0 || verticalInput != 0) isUseJoystick = true;
        else isUseJoystick = false;
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement.Normalize();
        MainManager.Instance.mainPlayer.transform.TransformDirection(movement);

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        // Joystick ile karakterin d�n���
        float rotateInput = joystick.Horizontal;

        Vector3 rotation = new Vector3(0f, rotateInput, 0f);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation * rotationSpeed * Time.fixedDeltaTime));
    }
}

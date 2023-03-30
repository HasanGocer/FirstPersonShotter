using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoSingleton<PlayerMovement>
{
    Joystick joystick; // Joystick nesnesi
    public float speed = 5f; // Hareket hýzý
    public float rotationSpeed = 100f; // Dönüþ hýzý
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joystick = MainManager.Instance.joystick;
    }

    void Update()
    {
        if (GameManager.Instance.gameStat == GameManager.GameStat.start && (joystick.Horizontal != 0 || joystick.Vertical != 0))
        {
            // Joystick ile karakterin hareketi
            float horizontalInput = joystick.Horizontal;
            float verticalInput = joystick.Vertical;


            float rotateInput = joystick.Horizontal;
            Vector3 rotation = new Vector3(0, rotateInput, 0);
            rb.transform.rotation = Quaternion.Lerp(rb.transform.rotation, Quaternion.Euler(rotation * rotationSpeed) * rb.transform.rotation, Time.deltaTime);

            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
            movement = transform.TransformDirection(movement);
            (movement).Normalize();

            rb.velocity = Vector3.zero;
            rb.velocity = movement * speed;
        }
    }
}

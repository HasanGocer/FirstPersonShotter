using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick; // Joystick nesnesi
    public float speed = 5f; // Hareket hýzý
    public float rotationSpeed = 100f; // Dönüþ hýzý
    private Rigidbody rb;
    private Touch touch;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joystick = MainManager.Instance.joystick;
    }

    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                joystick.gameObject.SetActive(true);
                joystick.transform.position = touch.position;
                joystick.transform.rotation = Quaternion.identity;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = touch.position;
                joystick.OnPointerDown(eventData);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                joystick.OnPointerUp(new PointerEventData(EventSystem.current));
                joystick.gameObject.SetActive(false);
            }

            // Joystick ile karakterin hareketi
            float horizontalInput = joystick.Horizontal;
            float verticalInput = joystick.Vertical;

            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
            movement.Normalize();
            MainManager.Instance.mainPlayer.transform.TransformDirection(movement);

            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

            // Joystick ile karakterin dönüþü
            float rotateInput = joystick.Horizontal;

            Vector3 rotation = new Vector3(0f, rotateInput, 0f);
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation * rotationSpeed * Time.fixedDeltaTime));
        }
    }
}

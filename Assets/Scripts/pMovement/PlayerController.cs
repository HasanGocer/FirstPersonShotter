using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotateSpeed = 10f;

    private Rigidbody rb;
    private Touch touch;
    private bool isMoving = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleTouchInput();
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            HandleMovementPhysics();
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    private void HandleMovementPhysics()
    {
        // Dokunulan yere göre karakterin dönüþünü ayarla
        Vector2 touchDeltaPosition = touch.deltaPosition;
        float horizontalInput = touchDeltaPosition.x / Screen.width;
        rb.rotation *= Quaternion.Euler(0f, horizontalInput * rotateSpeed, 0f);

        // Yatay ve dikey hareket vektörlerini hesapla
        float verticalInput = touchDeltaPosition.y / Screen.height;
        Vector3 forwardMovement = transform.forward * verticalInput * moveSpeed;
        Vector3 rightMovement = transform.right * horizontalInput * moveSpeed;

        // Yürüme hareketini uygula
        Vector3 targetVelocity = forwardMovement + rightMovement;
        targetVelocity.y = rb.velocity.y;
        rb.velocity = targetVelocity;
    }
}


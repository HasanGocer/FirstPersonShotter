using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCamera : MonoBehaviour
{
    [SerializeField] private float sensitivity = 1.0f;
    [SerializeField] private float rotationSpeed = 10f;

    private float rotX;
    private float rotY;

    private bool isMobile = false;

    private void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            isMobile = true;
        }
    }

    private void Update()
    {
        if (isMobile && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            rotX += touchDeltaPosition.x * sensitivity * Time.deltaTime * rotationSpeed;
            rotY -= touchDeltaPosition.y * sensitivity * Time.deltaTime * rotationSpeed;
            rotY = Mathf.Clamp(rotY, -90f, 90f);

            transform.rotation = Quaternion.Euler(rotY, rotX, 0f);
        }
    }
}
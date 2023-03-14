using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform target;  // Kameranýn takip edeceði hedef obje
    [SerializeField] private float distance = 10f;  // Kamera ile hedef obje arasýndaki mesafe
    [SerializeField] private float height = 5f;  // Kameranýn hedef objeden yüksekliði
    [SerializeField] private float rotationDamping = 1f;  // Kameranýn yön deðiþtirirken yavaþlamasý
    [SerializeField] private float heightDamping = 1f;  // Kameranýn yüksekliðini deðiþtirirken yavaþlamasý
    [SerializeField] private float zoomSpeed = 1f;  // Kamera zoom hýzý

    private float currentDistance;  // Mevcut mesafe
    private float currentHeight;  // Mevcut yükseklik
    private float currentRotationAngle;  // Mevcut yön açýsý
    private float currentRotationAngleVertical;  // Mevcut yön açýsý
    private float currentHeightVelocity;  // Mevcut yükseklik deðiþim hýzý
    private float currentZoom;  // Mevcut zoom seviyesi

    private void Start()
    {
        currentDistance = distance;
        currentHeight = target.position.y + height;
        currentRotationAngle = transform.eulerAngles.y;
        currentRotationAngleVertical = transform.eulerAngles.x;
        currentZoom = Camera.main.fieldOfView;
    }

    private void LateUpdate()
    {
        // Kameranýn yönünü hedef objeye doðru döndürme
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, target.eulerAngles.y, rotationDamping * Time.deltaTime);
        currentRotationAngleVertical = PlayerRotation.Instance.xRot;
        // Kameranýn zoom seviyesini ayarlama
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, 20f, 60f);

        // Kameranýn mesafesini ayarlama
        currentDistance = Mathf.Lerp(currentDistance, distance, Time.deltaTime);

        // Kameranýn yüksekliðini ayarlama
        currentHeight = Mathf.Lerp(currentHeight, target.position.y + height, heightDamping * Time.deltaTime);

        // Kameranýn pozisyonunu güncelleme
        Vector3 cameraPosition = target.position - (Quaternion.Euler(currentRotationAngleVertical, currentRotationAngle, 0f) * Vector3.forward * currentDistance);
        cameraPosition.y = currentHeight;
        transform.position = cameraPosition;

        // Kameranýn yönünü güncelleme
        transform.LookAt(target);

        // Kameranýn zoom seviyesini güncelleme
        Camera.main.fieldOfView = currentZoom;
    }
}

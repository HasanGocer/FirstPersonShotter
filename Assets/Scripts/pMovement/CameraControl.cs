using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform target;  // Kameran�n takip edece�i hedef obje
    [SerializeField] private float distance = 10f;  // Kamera ile hedef obje aras�ndaki mesafe
    [SerializeField] private float height = 5f;  // Kameran�n hedef objeden y�ksekli�i
    [SerializeField] private float rotationDamping = 1f;  // Kameran�n y�n de�i�tirirken yava�lamas�
    [SerializeField] private float heightDamping = 1f;  // Kameran�n y�ksekli�ini de�i�tirirken yava�lamas�
    [SerializeField] private float zoomSpeed = 1f;  // Kamera zoom h�z�

    private float currentDistance;  // Mevcut mesafe
    private float currentHeight;  // Mevcut y�kseklik
    private float currentRotationAngle;  // Mevcut y�n a��s�
    private float currentRotationAngleVertical;  // Mevcut y�n a��s�
    private float currentHeightVelocity;  // Mevcut y�kseklik de�i�im h�z�
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
        // Kameran�n y�n�n� hedef objeye do�ru d�nd�rme
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, target.eulerAngles.y, rotationDamping * Time.deltaTime);
        currentRotationAngleVertical = PlayerRotation.Instance.xRot;
        // Kameran�n zoom seviyesini ayarlama
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, 20f, 60f);

        // Kameran�n mesafesini ayarlama
        currentDistance = Mathf.Lerp(currentDistance, distance, Time.deltaTime);

        // Kameran�n y�ksekli�ini ayarlama
        currentHeight = Mathf.Lerp(currentHeight, target.position.y + height, heightDamping * Time.deltaTime);

        // Kameran�n pozisyonunu g�ncelleme
        Vector3 cameraPosition = target.position - (Quaternion.Euler(currentRotationAngleVertical, currentRotationAngle, 0f) * Vector3.forward * currentDistance);
        cameraPosition.y = currentHeight;
        transform.position = cameraPosition;

        // Kameran�n y�n�n� g�ncelleme
        transform.LookAt(target);

        // Kameran�n zoom seviyesini g�ncelleme
        Camera.main.fieldOfView = currentZoom;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float sensitivity = 2.0f; // Kamera hassasiyeti
    private Vector2 lastTouchPosition; // Son dokunma konumu
    private bool isDragging = false; // Dokunma s�r�klendi mi kontrol�
    private float dragTimeThreshold = 0.2f; // S�r�kleme hareketinin alg�lanma s�resi e�i�i
    private float dragDistanceThreshold = 15f; // S�r�kleme hareketinin alg�lanma mesafesi e�i�i
    private float dragStartTime; // S�r�kleme hareketinin ba�lang�� zaman�
    private Vector3 initialRotation; // Kameran�n ba�lang�� rotasyonu

    void Start()
    {
        // Kameran�n ba�lang�� rotasyonu al�n�r
        initialRotation = transform.eulerAngles;
    }

    void Update()
    {
        // Dokunma say�s� kontrol edilir
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            // Dokunma hareketi kontrol edilir
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Dokunma ba�lang�� noktas� al�n�r ve s�r�kleme hareketi kontrol� ba�lat�l�r
                    lastTouchPosition = touch.position;
                    dragStartTime = Time.time;
                    isDragging = true;
                    break;

                case TouchPhase.Moved:
                    // Kamera rotasyonu hesaplan�r ve uygulan�r
                    Vector2 delta = touch.position - lastTouchPosition;
                    transform.eulerAngles = new Vector3(
                        transform.eulerAngles.x/* - delta.y * sensitivity*/,
                        transform.eulerAngles.y + delta.x * sensitivity,
                        0f);
                    lastTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    // Dokunma hareketi bitti�inde s�r�kleme hareketi kontrol� sonland�r�l�r
                    isDragging = false;

                    // S�r�kleme hareketi kontrol� i�in s�n�r de�erler hesaplan�r
                    float dragTime = Time.time - dragStartTime;
                    float dragDistance = (touch.position - lastTouchPosition).magnitude;

                    // S�r�kleme hareketi s�n�r de�erleri a��l�rsa, kamera rotasyonu s�f�rlan�r
                    if (dragTime <= dragTimeThreshold && dragDistance >= dragDistanceThreshold)
                    {
                        transform.eulerAngles = initialRotation;
                    }
                    break;
            }
        }
    }
}

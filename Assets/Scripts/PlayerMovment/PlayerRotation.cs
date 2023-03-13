using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float sensitivity = 2.0f; // Kamera hassasiyeti
    private Vector2 lastTouchPosition; // Son dokunma konumu
    private bool isDragging = false; // Dokunma sürüklendi mi kontrolü
    private float dragTimeThreshold = 0.2f; // Sürükleme hareketinin algýlanma süresi eþiði
    private float dragDistanceThreshold = 15f; // Sürükleme hareketinin algýlanma mesafesi eþiði
    private float dragStartTime; // Sürükleme hareketinin baþlangýç zamaný
    private Vector3 initialRotation; // Kameranýn baþlangýç rotasyonu

    void Start()
    {
        // Kameranýn baþlangýç rotasyonu alýnýr
        initialRotation = transform.eulerAngles;
    }

    void Update()
    {
        // Dokunma sayýsý kontrol edilir
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            // Dokunma hareketi kontrol edilir
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Dokunma baþlangýç noktasý alýnýr ve sürükleme hareketi kontrolü baþlatýlýr
                    lastTouchPosition = touch.position;
                    dragStartTime = Time.time;
                    isDragging = true;
                    break;

                case TouchPhase.Moved:
                    // Kamera rotasyonu hesaplanýr ve uygulanýr
                    Vector2 delta = touch.position - lastTouchPosition;
                    transform.eulerAngles = new Vector3(
                        transform.eulerAngles.x/* - delta.y * sensitivity*/,
                        transform.eulerAngles.y + delta.x * sensitivity,
                        0f);
                    lastTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    // Dokunma hareketi bittiðinde sürükleme hareketi kontrolü sonlandýrýlýr
                    isDragging = false;

                    // Sürükleme hareketi kontrolü için sýnýr deðerler hesaplanýr
                    float dragTime = Time.time - dragStartTime;
                    float dragDistance = (touch.position - lastTouchPosition).magnitude;

                    // Sürükleme hareketi sýnýr deðerleri aþýlýrsa, kamera rotasyonu sýfýrlanýr
                    if (dragTime <= dragTimeThreshold && dragDistance >= dragDistanceThreshold)
                    {
                        transform.eulerAngles = initialRotation;
                    }
                    break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoSingleton<PlayerRotation>
{
    [SerializeField] private float sensitivity = 2.0f; // Kamera hassasiyeti
    [SerializeField] private float xdistance;
    private Vector2 lastTouchPosition; // Son dokunma konumu
    private bool isDragging = false; // Dokunma s�r�klendi mi kontrol�
    private float dragTimeThreshold = 0.2f; // S�r�kleme hareketinin alg�lanma s�resi e�i�i
    private float dragDistanceThreshold = 15f; // S�r�kleme hareketinin alg�lanma mesafesi e�i�i
    private float dragStartTime; // S�r�kleme hareketinin ba�lang�� zaman�
    private Vector3 initialRotation; // Kameran�n ba�lang�� rotasyonu
    [HideInInspector] public float xRot;

    bool isTouch;
    Touch touch;
    int touchCount = -2;
    void Start()
    {
        // Kameran�n ba�lang�� rotasyonu al�n�r
        initialRotation = transform.eulerAngles;
    }

    void Update()
    {
        // Dokunma say�s� kontrol edilir
        if (GameManager.Instance.gameStat == GameManager.GameStat.start)
            if (Input.touchCount > 0)
                if (TouchCheck())
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

                            xRot = transform.eulerAngles.x - delta.y * sensitivity / 5;
                            if (xRot > 300) xRot -= 360;
                            xRot = Mathf.Clamp(xRot, -xdistance, xdistance);

                            transform.eulerAngles = new Vector3
                                (xRot,
                                transform.eulerAngles.y + delta.x * sensitivity,
                                0f); ;
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
    private bool TouchCheck()
    {
        if (PlayerMovement.Instance.joystickTouchCount == Input.touchCount || Input.touchCount == ShotSystem.Instance.touchCount || PlayerMovement.Instance.joystickTouchCount == touchCount || touchCount == ShotSystem.Instance.touchCount)
        {
            isTouch = false;
            touchCount = -2;
            return false;
        }
        else
        {
            if (touchCount == -2)
                if (!isTouch)
                {
                    touchCount = Input.touchCount - 1;
                    print(touchCount);
                    touch = Input.GetTouch(touchCount);
                    isTouch = true;
                }
                else
                    touch = Input.GetTouch(touchCount);
            return true;
        }
    }
}

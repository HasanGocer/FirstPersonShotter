using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoSingleton<PlayerRotation>
{
    [SerializeField] private float sensitivity = 2.0f; // Kamera hassasiyeti
    [SerializeField] private float xdistance;
    [SerializeField] GameObject _player;
    private Vector2 lastTouchPosition; // Son dokunma konumu
    private bool isDragging = false; // Dokunma sürüklendi mi kontrolü
    private float dragTimeThreshold = 0.2f; // Sürükleme hareketinin algýlanma süresi eþiði
    private float dragDistanceThreshold = 15f; // Sürükleme hareketinin algýlanma mesafesi eþiði
    private float dragStartTime; // Sürükleme hareketinin baþlangýç zamaný
    private Vector3 initialRotation; // Kameranýn baþlangýç rotasyonu
    [HideInInspector] public float xRot;

    RectTransform rectTransform;
    float yMin, yMax;

    void Start()
    {
        if (GameManager.Instance.gameStat == GameManager.GameStat.start)
            initialRotation = _player.transform.eulerAngles;
        rectTransform = GetComponent<RectTransform>();
        yMin = (Camera.main.pixelHeight / 2) - (rectTransform.rect.height / 2);
        yMax = (Camera.main.pixelHeight / 2) + (rectTransform.rect.height / 2);
    }

    void Update()
    {
        // Dokunma sayýsý kontrol edilir
        if (GameManager.Instance.gameStat == GameManager.GameStat.start)
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(Input.touchCount - 1);
                if (touch.position.x > yMin && touch.position.x < yMax)

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

                            xRot = _player.transform.eulerAngles.x - delta.y * sensitivity / 5;
                            if (xRot > 300) xRot -= 360;
                            xRot = Mathf.Clamp(xRot, -xdistance, xdistance);

                            _player.transform.eulerAngles = new Vector3
                                (xRot,
                              _player.transform.eulerAngles.y + delta.x * sensitivity,
                                0f); ;
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
                                _player.transform.eulerAngles = initialRotation;
                            }
                            break;
                    }
            }

    }
}

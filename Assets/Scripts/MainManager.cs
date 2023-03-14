using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainManager : MonoSingleton<MainManager>
{
    public GameObject mainPlayer;
    public AnimController animController;
    public Joystick joystick;
    public Rigidbody rb;
    public float mainHealth;

    public void StartGhostManager()
    {
        mainHealth = ItemData.Instance.field.mainHealth;
    }
}

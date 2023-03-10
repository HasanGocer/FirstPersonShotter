using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainManager : MonoSingleton<MainManager>
{
    public GameObject mainPlayer;
    public GameObject hitPos;
    public AnimController animController;
    public Joystick joystick;
    public float mainHealth;

    public void StartGhostManager()
    {
        mainHealth = ItemData.Instance.field.mainHealth;
    }
}

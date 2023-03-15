using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerManager : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}

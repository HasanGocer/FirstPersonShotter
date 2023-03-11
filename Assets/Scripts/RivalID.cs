using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalID : MonoBehaviour
{
    public float rivalHealth;
    public CapsuleCollider capsuleCollider;
    public AnimController animController;
    public CharacterBar characterBar;
    public GameObject hitPos;
    public bool isLive = true;

    public void RivalIDStart()
    {
        rivalHealth = ItemData.Instance.field.rivalHealth;
    }
}

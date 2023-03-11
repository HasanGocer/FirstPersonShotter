using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalID : MonoBehaviour
{
    public float rivalHealth;
    public CapsuleCollider capsuleCollider;
    public AnimController animController;
    public CharacterBar characterBar;
    public ClothesID clothesID;
    public GameObject eyePosition;
    public bool isLive = true;
    public bool isSeen;

    public void RivalIDStart()
    {
        rivalHealth = ItemData.Instance.field.rivalHealth;
    }
}

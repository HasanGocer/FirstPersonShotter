using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendID : MonoBehaviour
{
    public float friendHealth;
    public CapsuleCollider capsuleCollider;
    public AnimController animController;
    public CharacterBar characterBar;
    public ClothesID clothesID;
    public GameObject eyePosition;
    public bool isLive = true;

    public void FriendHealthStart()
    {
        friendHealth = ItemData.Instance.field.friendHealth;
    }
}

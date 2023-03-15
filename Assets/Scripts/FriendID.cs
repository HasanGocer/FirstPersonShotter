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
    public GameObject gunFirePos;
    public bool isLive = true;
    public bool isSeen;

    public void FriendHealthStart()
    {
        friendHealth = ItemData.Instance.field.friendHealth;
    }
}

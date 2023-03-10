using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendID : MonoBehaviour
{
    public float friendHealth;
    public CapsuleCollider capsuleCollider;
    public AnimController animController;
    public GameObject hitPos;
    public bool isLive = true;

    public void FriendHealthStart()
    {
        friendHealth = ItemData.Instance.field.friendHealth;
    }
}

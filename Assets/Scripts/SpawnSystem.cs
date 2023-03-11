using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoSingleton<SpawnSystem>
{
    [SerializeField] List<GameObject> _rivalPos = new List<GameObject>();
    [SerializeField] List<bool> _rivalBool = new List<bool>();

    [SerializeField] List<GameObject> _friendPos = new List<GameObject>();
    [SerializeField] List<bool> _friendBool = new List<bool>();

    [SerializeField] int _OPRivalCount, _OPFriendCount;
    [SerializeField] int friendSpawnCount, rivalSpawnCount;

    public void SpawnTime()
    {
        for (int i = 0; i < friendSpawnCount; i++) SpawnFriend();
        for (int i = 0; i < rivalSpawnCount; i++) SpawnRival();
    }

    private void SpawnFriend()
    {
        SelectFriendPosCount(out int friendPosCount);
        GameObject friend = GetFriendObject(friendPosCount);
        FriendFunctionPlacement(friend);
    }
    private void SpawnRival()
    {
        SelectRivalPosCount(out int rivalPosCount);
        GameObject rival = GetRivalObject(rivalPosCount);
        RivalFunctionPlacement(rival);
    }

    private void SelectFriendPosCount(out int friendPosCount)
    {
        do
            friendPosCount = Random.Range(0, _friendBool.Count);
        while (_friendBool[friendPosCount]);
        _friendBool[friendPosCount] = true;
    }
    private GameObject GetFriendObject(int friendPosCount)
    {
        return ObjectPool.Instance.GetPooledObject(_OPFriendCount, _friendPos[friendPosCount].transform.position);
    }
    private void FriendFunctionPlacement(GameObject friend)
    {
        FriendID friendID = friend.GetComponent<FriendID>();

        friendID.clothesID.StartClothesPlacement();
        StartCoroutine(SeenManager.Instance.FriendSeenMechanic(friend, friendID, friendID.eyePosition));
    }

    private void SelectRivalPosCount(out int rivalPosCount)
    {
        do
            rivalPosCount = Random.Range(0, _rivalBool.Count);
        while (_rivalBool[rivalPosCount]);
        _rivalBool[rivalPosCount] = true;
    }
    private GameObject GetRivalObject(int rivalPosCount)
    {
        return ObjectPool.Instance.GetPooledObject(_OPRivalCount, _rivalPos[rivalPosCount].transform.position);
    }
    private void RivalFunctionPlacement(GameObject rival)
    {
        RivalID rivalID = rival.GetComponent<RivalID>();

        rivalID.clothesID.StartClothesPlacement();
        StartCoroutine(SeenManager.Instance.RivalSeenMechanic(rival, rivalID, rivalID.eyePosition));
    }
}

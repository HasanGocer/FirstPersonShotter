using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSystem : MonoSingleton<FinishSystem>
{
    [Header("Finish_Field")]
    [Space(10)]

    public int friendCount = 5;
    public int rivalCount = 5;

    public void FriendDown()
    {
        friendCount--;
        if (friendCount <= 0) GameStat.Instance.GameSave(GameStat.WinStat.rival);
    }

    public void RivalDown()
    {
        rivalCount--;
        if (rivalCount <= 0 && GameManager.Instance.gameStat == GameManager.GameStat.start) GameStat.Instance.GameSave(GameStat.WinStat.friend);
    }
}

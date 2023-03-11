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
        if (friendCount <= 0)
        {
            //lose
        }
    }

    public void RivalDown()
    {
        rivalCount--;
        if (rivalCount <= 0 && GameManager.Instance.gameStat == GameManager.GameStat.start) FinishTime();
    }

    private void FinishTime()
    {
        GameManager gameManager = GameManager.Instance;
        Buttons buttons = Buttons.Instance;
        MoneySystem moneySystem = MoneySystem.Instance;
        gameManager.gameStat = GameManager.GameStat.finish;
        StartCoroutine(BarSystem.Instance.BarImageFillAmountIenum());
        LevelManager.Instance.LevelCheck();
        buttons.winPanel.SetActive(true);
        buttons.barPanel.SetActive(true);
        buttons.finishGameMoneyText.text = moneySystem.NumberTextRevork(gameManager.addedMoney);
        moneySystem.MoneyTextRevork(gameManager.addedMoney);
    }
}

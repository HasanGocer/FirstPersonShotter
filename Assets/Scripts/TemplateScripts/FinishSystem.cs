using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSystem : MonoSingleton<FinishSystem>
{
    [Header("Finish_Field")]
    [Space(10)]

    int freeCount = 0;

    public void FinishCheck()
    {
        if (GameManager.Instance.gameStat == GameManager.GameStat.start)
            FinishTime();
    }
    public void FinishTime()
    {
        GameManager gameManager = GameManager.Instance;
        Buttons buttons = Buttons.Instance;
        MoneySystem moneySystem = MoneySystem.Instance;
        StartCoroutine(BarSystem.Instance.BarImageFillAmountIenum());
        LevelManager.Instance.LevelCheck();
        buttons.winPanel.SetActive(true);
        buttons.barPanel.SetActive(true);
        buttons.finishGameMoneyText.text = moneySystem.NumberTextRevork(gameManager.addedMoney);
        gameManager.gameStat = GameManager.GameStat.finish;
        moneySystem.MoneyTextRevork(gameManager.addedMoney);
    }
}

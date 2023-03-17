using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStat : MonoSingleton<GameStat>
{
    public enum WinStat
    {
        friend,
        rival
    }

    [SerializeField] private int _friendWinCount, _rivalWinCount;
    [SerializeField] private int _WinCount;

    public int mapCount;

    [SerializeField] int addedMoney;

    private void Awake()
    {
        mapCount = -1;
    }

    public void GameSave(WinStat winStat)
    {
        if (winStat == WinStat.friend)
        {

            _friendWinCount++;
            addedMoney += GameManager.Instance.addedMoney;
            if (_friendWinCount == _WinCount)
            {
                Buttons buttons = Buttons.Instance;
                MoneySystem moneySystem = MoneySystem.Instance;

                GameManager.Instance.gameStat = GameManager.GameStat.finish;
                StartCoroutine(BarSystem.Instance.BarImageFillAmountIenum());
                LevelManager.Instance.LevelCheck();
                buttons.winPanel.SetActive(true);
                buttons.barPanel.SetActive(true);
                buttons.finishGameMoneyText.text = moneySystem.NumberTextRevork(addedMoney);
                moneySystem.MoneyTextRevork(addedMoney);
            }
            else
            {
                Buttons buttons = Buttons.Instance;

                buttons.ContinuePanel.SetActive(true);
                buttons.rivalText.text = _rivalWinCount.ToString();
                buttons.friendText.text = _friendWinCount.ToString();
            }
        }
        else
        {
            _rivalWinCount++;
            if (_rivalWinCount == _WinCount)
            {
                Buttons buttons = Buttons.Instance;

                buttons.failPanel.SetActive(true);
            }
            else
            {
                Buttons buttons = Buttons.Instance;

                buttons.ContinuePanel.SetActive(true);
                buttons.rivalText.text = _rivalWinCount.ToString();
                buttons.friendText.text = _friendWinCount.ToString();
            }
        }
    }
}

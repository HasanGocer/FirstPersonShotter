using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CharacterBar : MonoBehaviour
{
    public enum CharacterStat
    {
        friend = 0,
        rival = 1,
        main = 2
    }


    public CharacterStat characterStat;
    [SerializeField] private Image _bar;
    [SerializeField] private GameObject _barPanel;

    public void MainCharacterStart()
    {
        _barPanel.SetActive(true);
    }

    public void BarUpdate(float max, float count, float down)
    {
        float nowBar = count / max;
        float afterBar = (count - down) / max;
        StartCoroutine(BarUpdateIenumurator(nowBar, afterBar));
    }

    private IEnumerator BarUpdateIenumurator(float start, float finish)
    {
        yield return null;
        float temp = 0;
        while (true)
        {
            temp += Time.deltaTime;
            _bar.fillAmount = Mathf.Lerp(start, finish, temp);
            yield return new WaitForEndOfFrame();
            if (_bar.fillAmount == 0)
            {
                FinishGame();
                break;
            }
            if (_bar.fillAmount == finish) break;
        }
    }

    private void FinishGame()
    {
        if (characterStat == CharacterStat.rival) RivalDown();
        else if (characterStat == CharacterStat.friend) FriendDown();
        else PlayerDown();
    }
    private void RivalDown()
    {
        if (gameObject.GetComponent<RivalID>().isLive)
        {
            MainManager mainManager = MainManager.Instance;
            RivalID rivalID = gameObject.GetComponent<RivalID>();
            NavMeshAgent navMeshAgent = gameObject.GetComponent<NavMeshAgent>();

            navMeshAgent.isStopped = true;
            rivalID.isLive = false;
            rivalID.animController.CallDeadAnim();
            rivalID.capsuleCollider.enabled = false;
            _barPanel.SetActive(false);

            GameManager.Instance.addedMoney += (int)mainManager.rivalDownAddedMoney;
            CoinSpawn.Instance.Spawn(gameObject, mainManager.mainPlayer);
            FinishSystem.Instance.RivalDown();
        }
    }
    private void FriendDown()
    {
        if (gameObject.GetComponent<FriendID>().isLive)
        {
            FriendID friendID = gameObject.GetComponent<FriendID>();
            NavMeshAgent navMeshAgent = gameObject.GetComponent<NavMeshAgent>();

            navMeshAgent.isStopped = true;
            friendID.isLive = false;

            friendID.animController.CallDeadAnim();
            friendID.capsuleCollider.enabled = false;
            _barPanel.SetActive(false);

            FinishSystem.Instance.FriendDown();
        }
    }
    private void PlayerDown()
    {
        if (GameManager.Instance.gameStat == GameManager.GameStat.start)
        {
            GameManager.Instance.gameStat = GameManager.GameStat.finish;
            //fail
        }
    }
}
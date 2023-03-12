using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBar : MonoBehaviour
{
    public enum CharacterStat
    {
        friend = 0,
        rival = 1
    }


    public CharacterStat characterStat;
    [SerializeField] private Image _bar;
    [SerializeField] private GameObject _barPanel;

    public void BarUpdate(float max, float count, float down)
    {
        print(max);
        print(count);
        print(down);
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
        else FriendDown();
    }
    private void RivalDown()
    {
        print("seeR");
        RivalID rivalID = gameObject.GetComponent<RivalID>();
        rivalID.isLive = false;
        rivalID.animController.CallDeadAnim();
        rivalID.capsuleCollider.enabled = false;
        _barPanel.SetActive(false);

        FinishSystem.Instance.RivalDown();
    }
    private void FriendDown()
    {
        print("seeF");
        FriendID friendID = gameObject.GetComponent<FriendID>();
        friendID.isLive = false;
        friendID.animController.CallDeadAnim();
        friendID.capsuleCollider.enabled = false;
        _barPanel.SetActive(false);

        FinishSystem.Instance.FriendDown();
    }
}
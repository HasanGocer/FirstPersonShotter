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
    [SerializeField] private Image bar;

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
            bar.fillAmount = Mathf.Lerp(start, finish, temp);
            yield return new WaitForEndOfFrame();
            if (bar.fillAmount == finish)
            {
                FinishGame();
                break;
            }
        }
    }

    private void FinishGame()
    {
        if (characterStat == CharacterStat.rival) FinishSystem.Instance.RivalDown();
        else FinishSystem.Instance.FriendDown();
    }
}
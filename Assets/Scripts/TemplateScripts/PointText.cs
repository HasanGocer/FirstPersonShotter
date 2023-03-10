using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using DamageNumbersPro;

public class PointText : MonoSingleton<PointText>
{
    [Header("Text_Field")]
    [Space(10)]

    [SerializeField] private int _OPRedTextCount;
    [SerializeField] private int _OPGreenTextCount;
    [SerializeField] private float _textMoveTime;

    public void CallRedText(GameObject Pos, int count)
    {
        StartCoroutine(CallPointRedText(Pos, count));
    }

    public void CallGreenText(GameObject Pos, int count)
    {
        StartCoroutine(CallPointGreenText(Pos, count));
    }

    private IEnumerator CallPointRedText(GameObject Pos, int count)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPRedTextCount);

        obj.GetComponent<DamageNumberMesh>().leftText = "" + count;
        obj.transform.position = Pos.transform.position;
        yield return new WaitForSeconds(_textMoveTime);
        ObjectPool.Instance.AddObject(_OPRedTextCount, obj);
    }
    private IEnumerator CallPointGreenText(GameObject Pos, int count)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPGreenTextCount);

        obj.GetComponent<DamageNumberMesh>().leftText = "" + count;
        obj.transform.position = Pos.transform.position;
        yield return new WaitForSeconds(_textMoveTime);
        ObjectPool.Instance.AddObject(_OPGreenTextCount, obj);
    }
}

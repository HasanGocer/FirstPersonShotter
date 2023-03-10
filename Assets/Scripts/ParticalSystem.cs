using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSystem : MonoSingleton<ParticalSystem>
{
    [SerializeField] int _OPWallShotParticalCount;
    [SerializeField] int _OPBodyShotParticalCount;

    [SerializeField] float _WallShotTime;
    [SerializeField] float _bodyShotTime;

    public void WallShotPartical(Vector3 pos)
    {
        StartCoroutine(WallShotParticalEnum(pos));
    }
    public void BodyShotPartical(Vector3 pos)
    {
        StartCoroutine(BodyShotParticalEnum(pos));
    }

    private IEnumerator WallShotParticalEnum(Vector3 pos)
    {
        GameObject partical = ObjectPool.Instance.GetPooledObject(_OPWallShotParticalCount, pos);
        yield return new WaitForSeconds(_WallShotTime);
        ObjectPool.Instance.AddObject(_OPWallShotParticalCount, partical);
    }
    private IEnumerator BodyShotParticalEnum(Vector3 pos)
    {
        GameObject partical = ObjectPool.Instance.GetPooledObject(_OPBodyShotParticalCount, pos);
        yield return new WaitForSeconds(_bodyShotTime);
        ObjectPool.Instance.AddObject(_OPBodyShotParticalCount, partical);
    }
}

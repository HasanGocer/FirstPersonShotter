using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSystem : MonoSingleton<ParticalSystem>
{
    [SerializeField] int _OPWallShotParticalCount;
    [SerializeField] float _WallShotTime;

    public void WallShotPartical(Vector3 pos)
    {
        StartCoroutine(WallShotParticalEnum(pos));
    }

    private IEnumerator WallShotParticalEnum(Vector3 pos)
    {
        GameObject partical = ObjectPool.Instance.GetPooledObject(_OPWallShotParticalCount, pos);
        yield return new WaitForSeconds(_WallShotTime);
        ObjectPool.Instance.AddObject(_OPWallShotParticalCount, partical);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSystem : MonoSingleton<ParticalSystem>
{
    [SerializeField] int _OPWallShotParticalCount;
    [SerializeField] int _OPBodyShotParticalCount;
    [SerializeField] int _OPShotGunParticalCount;


    public void WallShotPartical(Vector3 pos)
    {
        ObjectPool.Instance.GetPooledObjectAdd(_OPWallShotParticalCount, pos);
    }
    public void BodyShotPartical(Vector3 pos)
    {
        ObjectPool.Instance.GetPooledObjectAdd(_OPBodyShotParticalCount, pos);
    }
    public void ShotGunPartical(Vector3 pos, GameObject target)
    {
        GameObject partical = ObjectPool.Instance.GetPooledObjectAdd(_OPShotGunParticalCount, pos);
        partical.transform.LookAt(target.transform);
    }

}

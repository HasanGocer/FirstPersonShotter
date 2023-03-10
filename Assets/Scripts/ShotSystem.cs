using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSystem : MonoSingleton<ShotSystem>
{
    [SerializeField] GameObject _character, _gunPos;
    [SerializeField] int _hitDistance;

    //Main karakterin Shot kodu
    public void Hit()
    {
        RaycastHit hit;

        if (Physics.Raycast(_gunPos.transform.position, transform.TransformDirection(Vector3.forward) * _hitDistance, out hit, _hitDistance))
            if (hit.collider.gameObject.CompareTag("Wall")) ParticalSystem.Instance.WallShotPartical(hit.point);
            else if (hit.collider.gameObject.CompareTag("RivalPlayer")) RivalHit(hit.point, hit.collider.gameObject);
    }

    public void RivalHit(Vector3 hitPos, GameObject rival)
    {
        RivalID rivalID = rival.GetComponent<RivalID>();

        rivalID.rivalHealth -= ItemData.Instance.field.mainDamage;
        ParticalSystem.Instance.BodyShotPartical(hitPos);
    }
}

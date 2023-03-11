using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeenManager : MonoSingleton<SeenManager>
{
    [SerializeField] float _hitDistance;

    public IEnumerator RivalSeenMechanic(GameObject rival, RivalID rivalID, GameObject eyePosition)
    {
        RaycastHit hit;

        while (rivalID.isLive)
        {
            if (Physics.Raycast(eyePosition.transform.position, rival.transform.TransformDirection(Vector3.forward) * _hitDistance, out hit, _hitDistance))
                if (hit.collider.gameObject.CompareTag("FriendPlayer")) RivalHit(hit.point, hit.collider.gameObject);
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FriendSeenMechanic(GameObject friend, FriendID friendID, GameObject eyePosition)
    {
        RaycastHit hit;

        while (friendID.isLive)
        {
            if (Physics.Raycast(eyePosition.transform.position, friend.transform.TransformDirection(Vector3.forward) * _hitDistance, out hit, _hitDistance))
                if (hit.collider.gameObject.CompareTag("RivalPlayer")) FriendHit(hit.point, hit.collider.gameObject);
            yield return new WaitForEndOfFrame();
        }
    }

    private void FriendHit(Vector3 hitPos, GameObject rival)
    {
        RivalID rivalID = rival.GetComponent<RivalID>();
        ItemData.Field field = ItemData.Instance.field;

        rivalID.rivalHealth -= field.mainDamage;
        rivalID.characterBar.BarUpdate(field.rivalHealth, rivalID.rivalHealth, field.mainDamage);
        ParticalSystem.Instance.BodyShotPartical(hitPos);
    }
    private void RivalHit(Vector3 hitPos, GameObject friend)
    {
        FriendID friendID = friend.GetComponent<FriendID>();
        ItemData.Field field = ItemData.Instance.field;

        friendID.friendHealth -= field.rivalDamage;
        friendID.characterBar.BarUpdate(field.mainHealth, friendID.friendHealth, field.rivalDamage);
        ParticalSystem.Instance.BodyShotPartical(hitPos);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class SeenManager : MonoSingleton<SeenManager>
{
    [SerializeField] float _hitDistance;
    [SerializeField] float _hitCountdawn;
    [SerializeField] float _NPCTurnRivalTime;

    public IEnumerator RivalSeenMechanic(GameObject rival, RivalID rivalID, GameObject eyePosition)
    {
        RaycastHit hit;
        NavMeshAgent navMeshAgent = rival.GetComponent<NavMeshAgent>();

        while (rivalID.isLive)
        {
            if (Physics.Raycast(eyePosition.transform.position, rival.transform.TransformDirection(Vector3.forward) * _hitDistance, out hit, _hitDistance))
                if (hit.collider.gameObject.CompareTag("FriendPlayer"))
                {
                    GameObject friend = hit.collider.gameObject;
                    FriendID friendID = friend.GetComponent<FriendID>();
                    NavMeshAgent navMeshAgent1 = friend.GetComponent<NavMeshAgent>();

                    navMeshAgent.isStopped = true;

                    StartCoroutine(RivalHit(hit.point, hit.collider.gameObject));

                    if (!friendID.isSeen)
                    {
                        navMeshAgent1.isStopped = true;
                        friendID.isSeen = true;
                        friend.transform.DOLookAt(rival.transform.position, _NPCTurnRivalTime);
                    }

                    yield return new WaitForSeconds(_hitCountdawn);
                    navMeshAgent.isStopped = false;
                }
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FriendSeenMechanic(GameObject friend, FriendID friendID, GameObject eyePosition)
    {
        RaycastHit hit;
        NavMeshAgent navMeshAgent = friend.GetComponent<NavMeshAgent>();


        while (friendID.isLive)
        {
            if (Physics.Raycast(eyePosition.transform.position, friend.transform.TransformDirection(Vector3.forward) * _hitDistance, out hit, _hitDistance))
                if (hit.collider.gameObject.CompareTag("RivalPlayer"))
                {
                    GameObject rival = hit.collider.gameObject;
                    RivalID rivalID = rival.GetComponent<RivalID>();
                    NavMeshAgent navMeshAgent1 = rival.GetComponent<NavMeshAgent>();

                    navMeshAgent.isStopped = true;

                    StartCoroutine(FriendHit(hit.point, hit.collider.gameObject));

                    if (!rivalID.isSeen)
                    {
                        navMeshAgent1.isStopped = true;
                        rivalID.isSeen = true;
                        rival.transform.DOLookAt(friend.transform.position, _NPCTurnRivalTime);
                    }

                    yield return new WaitForSeconds(_hitCountdawn);
                    navMeshAgent.isStopped = false;
                }
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator FriendHit(Vector3 hitPos, GameObject rival)
    {
        RivalID rivalID = rival.GetComponent<RivalID>();
        ItemData.Field field = ItemData.Instance.field;

        rivalID.isSeen = true;
        rivalID.characterBar.BarUpdate(field.rivalHealth, rivalID.rivalHealth, field.mainDamage);
        rivalID.rivalHealth -= field.mainDamage;
        ParticalSystem.Instance.BodyShotPartical(hitPos);
        yield return new WaitForSeconds(_hitCountdawn);
        rivalID.isSeen = false;
    }
    private IEnumerator RivalHit(Vector3 hitPos, GameObject friend)
    {
        FriendID friendID = friend.GetComponent<FriendID>();
        ItemData.Field field = ItemData.Instance.field;

        friendID.isSeen = true;
        friendID.characterBar.BarUpdate(field.mainHealth, friendID.friendHealth, field.rivalDamage);
        friendID.friendHealth -= field.rivalDamage;
        ParticalSystem.Instance.BodyShotPartical(hitPos);
        yield return new WaitForSeconds(_hitCountdawn);
        friendID.isSeen = false;
    }
}

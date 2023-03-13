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
    [SerializeField] int _hitCount;
    RaycastHit targetRivalHit, targetFriendHit;

    public IEnumerator RivalSeenMechanic(GameObject rival, RivalID rivalID, GameObject eyePosition)
    {
        NavMeshAgent navMeshAgent = rival.GetComponent<NavMeshAgent>();
        RaycastHit hit;
        GameObject target;

        while (rivalID.isLive)
        {
            target = null;

            for (int i = -1 * _hitCount; i < _hitCount; i++)
            {
                Debug.DrawRay(eyePosition.transform.position, rival.transform.TransformDirection(Vector3.forward + new Vector3(0.03f * i, 0, 0)) * _hitDistance, Color.red, 1);
                if (Physics.Raycast(eyePosition.transform.position, rival.transform.TransformDirection(Vector3.forward + new Vector3(0.03f * i, 0, 0)) * _hitDistance, out hit, _hitDistance))
                    if (hit.collider.gameObject.CompareTag("FriendPlayer"))
                    {
                        target = hit.collider.gameObject;
                        targetRivalHit = hit;
                        break;
                    }
                    else if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        ItemData.Field field = ItemData.Instance.field;

                        navMeshAgent.isStopped = true;

                        rivalID.characterBar.BarUpdate(field.mainHealth, MainManager.Instance.mainHealth, field.rivalDamage);
                        MainManager.Instance.mainHealth -= field.rivalDamage;
                        ParticalSystem.Instance.BodyShotPartical(hit.point);
                        target = null;
                        yield return new WaitForSeconds(_hitCountdawn);
                        break;
                    }
                yield return null;
            }
            if (target != null)
            {
                RivalSelect(targetRivalHit, navMeshAgent, rival, rivalID);
                yield return new WaitForSeconds(_hitCountdawn);
            }
            else navMeshAgent.isStopped = false;

            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FriendSeenMechanic(GameObject friend, FriendID friendID, GameObject eyePosition)
    {
        RaycastHit hit;
        NavMeshAgent navMeshAgent = friend.GetComponent<NavMeshAgent>();
        GameObject target;

        while (friendID.isLive)
        {
            target = null;

            for (int i = -1 * _hitCount; i < _hitCount; i++)
            {
                Debug.DrawRay(eyePosition.transform.position, friend.transform.TransformDirection(Vector3.forward + new Vector3(0.03f * i, 0, 0)) * _hitDistance, Color.green, 1);
                if (Physics.Raycast(eyePosition.transform.position, friend.transform.TransformDirection(Vector3.forward + new Vector3(0.03f * i, 0, 0)) * _hitDistance, out hit, _hitDistance))
                    if (hit.collider.gameObject.CompareTag("RivalPlayer"))
                    {
                        target = hit.collider.gameObject;
                        targetFriendHit = hit;
                        break;
                    }
                yield return null;
            }
            if (target != null)
            {
                FriendSelect(targetFriendHit, navMeshAgent, friend, friendID);
                yield return new WaitForSeconds(_hitCountdawn);
            }
            else navMeshAgent.isStopped = false;

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator FriendHit(Vector3 hitPos, GameObject rival, FriendID friendID)
    {
        RivalID rivalID = rival.GetComponent<RivalID>();
        ItemData.Field field = ItemData.Instance.field;

        rivalID.isSeen = true;
        rivalID.characterBar.BarUpdate(field.rivalHealth, rivalID.rivalHealth, field.mainDamage);
        rivalID.rivalHealth -= field.mainDamage;
        ParticalSystem.Instance.BodyShotPartical(hitPos);
        yield return new WaitForSeconds(_hitCountdawn);
        rivalID.isSeen = false;
        friendID.isSeen = false;
    }
    private IEnumerator RivalHit(Vector3 hitPos, GameObject friend, RivalID rivalID)
    {
        FriendID friendID = friend.GetComponent<FriendID>();
        ItemData.Field field = ItemData.Instance.field;

        friendID.isSeen = true;
        friendID.characterBar.BarUpdate(field.mainHealth, friendID.friendHealth, field.rivalDamage);
        friendID.friendHealth -= field.rivalDamage;
        ParticalSystem.Instance.BodyShotPartical(hitPos);
        yield return new WaitForSeconds(_hitCountdawn);
        friendID.isSeen = false;
        rivalID.isSeen = false;
    }
    private void RivalSelect(RaycastHit hit, NavMeshAgent navMeshAgent, GameObject rival, RivalID rivalID)
    {
        GameObject friend = hit.collider.gameObject;
        FriendID friendID = friend.GetComponent<FriendID>();
        NavMeshAgent navMeshAgent1 = friend.GetComponent<NavMeshAgent>();

        rivalID.isSeen = true;
        navMeshAgent.isStopped = true;

        StartCoroutine(RivalHit(hit.point, friend, rivalID));

        if (!friendID.isSeen)
        {
            navMeshAgent1.isStopped = true;
            friendID.isSeen = true;
            friend.transform.DOLookAt(rival.transform.position, _NPCTurnRivalTime);
        }
    }
    private void FriendSelect(RaycastHit hit, NavMeshAgent navMeshAgent, GameObject friend, FriendID friendID)
    {
        GameObject rival = hit.collider.gameObject;
        RivalID rivalID = rival.GetComponent<RivalID>();
        NavMeshAgent navMeshAgent1 = rival.GetComponent<NavMeshAgent>();

        friendID.isSeen = true;
        navMeshAgent.isStopped = true;

        StartCoroutine(FriendHit(hit.point, hit.collider.gameObject, friendID));

        if (!rivalID.isSeen)
        {
            navMeshAgent1.isStopped = true;
            rivalID.isSeen = true;
            rival.transform.DOLookAt(friend.transform.position, _NPCTurnRivalTime);
        }
    }
}

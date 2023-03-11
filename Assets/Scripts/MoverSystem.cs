using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoverSystem : MonoSingleton<MoverSystem>
{
    [SerializeField] float _NPCMinDistance;
    [SerializeField] float _NPCMoveFinishCountdown;

    public IEnumerator RivalNPCMove(GameObject rival, RivalID rivalID)
    {
        NavMeshAgent navMeshAgent = rival.GetComponent<NavMeshAgent>();
        Vector3 target = GetRandomPoint(rival);

        while (rivalID.isLive)
        {
            if (!rivalID.isSeen)
            {
                navMeshAgent.destination = target;
                yield return new WaitForEndOfFrame();
                if (_NPCMinDistance > Vector3.Distance(rival.transform.position, target))
                {
                    target = GetRandomPoint(rival);
                    yield return new WaitForSeconds(_NPCMoveFinishCountdown);
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator FriendNPCMove(GameObject friend, FriendID friendID)
    {
        NavMeshAgent navMeshAgent = friend.GetComponent<NavMeshAgent>();
        Vector3 target = GetRandomPoint(friend);

        while (friendID.isLive)
        {
            if (!friendID.isSeen)
            {
                navMeshAgent.destination = target;
                yield return new WaitForEndOfFrame();
                if (_NPCMinDistance > Vector3.Distance(friend.transform.position, target))
                {
                    target = GetRandomPoint(friend);
                    yield return new WaitForSeconds(_NPCMoveFinishCountdown);
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private Vector3 GetRandomPoint(GameObject NPC)
    {
        Vector3 randomPoint = Vector3.zero;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(NPC.transform.position, out hit, 10.0f, NavMesh.AllAreas))
        {
            randomPoint = hit.position;
        }
        return randomPoint;
    }
}

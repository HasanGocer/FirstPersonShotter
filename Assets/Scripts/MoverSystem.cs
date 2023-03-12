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
        Vector3 target = GetRandomPoint(navMeshAgent);

        while (rivalID.isLive)
        {
            if (!rivalID.isSeen)
            {
                navMeshAgent.destination = target;
                yield return new WaitForEndOfFrame();
                if (_NPCMinDistance > Vector3.Distance(rival.transform.position, target))
                {
                    target = GetRandomPoint(navMeshAgent);
                    yield return new WaitForSeconds(_NPCMoveFinishCountdown);
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator FriendNPCMove(GameObject friend, FriendID friendID)
    {
        NavMeshAgent navMeshAgent = friend.GetComponent<NavMeshAgent>();
        Vector3 target = GetRandomPoint(navMeshAgent);

        while (friendID.isLive)
        {
            if (!friendID.isSeen)
            {
                print(13);
                navMeshAgent.destination = target;
                yield return new WaitForEndOfFrame();
                if (_NPCMinDistance > Vector3.Distance(friend.transform.position, target))
                {
                    target = GetRandomPoint(navMeshAgent);
                    yield return new WaitForSeconds(_NPCMoveFinishCountdown);
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private Vector3 GetRandomPoint(NavMeshAgent agent)
    {
        print(31);
        Vector3 randomPoint = Vector3.zero;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(agent.transform.position, out hit, agent.radius * 2.0f, agent.areaMask))
            randomPoint = hit.position;
        return randomPoint;
    }
}

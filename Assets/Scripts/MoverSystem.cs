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
        rivalID.animController.CallWalkAnim();

        while (rivalID.isLive)
        {
            if (!rivalID.isSeen)
            {
                navMeshAgent.destination = target;
                yield return new WaitForSeconds(Time.deltaTime);
                if (_NPCMinDistance > Vector3.Distance(rival.transform.position, target))
                {
                    rivalID.animController.CallIdleAnim();
                    target = GetRandomPoint(navMeshAgent);
                    yield return new WaitForSeconds(_NPCMoveFinishCountdown);
                    rivalID.animController.CallWalkAnim();
                }
            }
            else yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator FriendNPCMove(GameObject friend, FriendID friendID)
    {
        NavMeshAgent navMeshAgent = friend.GetComponent<NavMeshAgent>();
        Vector3 target = GetRandomPoint(navMeshAgent);
        friendID.animController.CallWalkAnim();

        while (friendID.isLive)
        {
            if (!friendID.isSeen)
            {
                navMeshAgent.destination = target;
                yield return new WaitForSeconds(Time.deltaTime);
                if (_NPCMinDistance > Vector3.Distance(friend.transform.position, target))
                {
                    friendID.animController.CallIdleAnim();
                    target = GetRandomPoint(navMeshAgent);
                    yield return new WaitForSeconds(_NPCMoveFinishCountdown);
                    friendID.animController.CallWalkAnim();
                }
            }
            else yield return new WaitForEndOfFrame();
        }
    }

    private Vector3 GetRandomPoint(NavMeshAgent agent)
    {
        Vector3 randomPoint = Vector3.zero;
        NavMeshHit hit;
        Vector3 randomPosition = agent.transform.position + Random.insideUnitSphere * 10.0f;

        while (!NavMesh.SamplePosition(new Vector3(Random.Range(-50, 50), agent.transform.position.y, Random.Range(-50, 50)), out hit, agent.radius, agent.walkableMask)) ;
        randomPoint = hit.position;
        return randomPoint;
    }
}

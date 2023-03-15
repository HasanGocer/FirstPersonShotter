using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinSpawn : MonoSingleton<CoinSpawn>
{
    [SerializeField] private int _OPCoinCount;

    public void Spawn(GameObject pos, GameObject Finish)
    {
        StartCoroutine(SpawnEnum(pos, Finish));
    }

    private IEnumerator SpawnEnum(GameObject pos, GameObject Finish)
    {
        int coinCount;
        coinCount = Random.Range(5, 12);
        List<GameObject> coins = new List<GameObject>();

        for (int i = 0; i < coinCount; i++)
        {
            GameObject obj = ObjectPool.Instance.GetPooledObject(_OPCoinCount);
            Rigidbody rb = obj.GetComponent<Rigidbody>();

            obj.transform.position = pos.transform.position;
            obj.transform.position += new Vector3(0, 6, 0);
            rb.velocity = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
            rb.useGravity = true;
            coins.Add(obj);
        }

        yield return new WaitForSeconds(2);

        for (int i = 0; i < coinCount; i++)
            Walk(coins[i], Finish);
    }
    private void Walk(GameObject obj, GameObject finish)
    {
        obj.transform.DOMove(finish.transform.position, 0.3f);
    }
}
using UnityEngine;
using System.Collections.Generic;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask enemyLayer;

    private List<GameObject> enemiesInRange = new List<GameObject>();
    private GameObject targetEnemy;

    private void Update()
    {
        // Hedef düþmaný belirleme
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
        if (hitColliders.Length > 0)
        {
            foreach (Collider collider in hitColliders)
            {
                if (!enemiesInRange.Contains(collider.gameObject))
                {
                    enemiesInRange.Add(collider.gameObject);
                }
            }

            float closestDistance = Mathf.Infinity;
            foreach (GameObject enemy in enemiesInRange)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetEnemy = enemy;
                }
            }

            // Karakterin yönünü düþmana doðru döndürme
            if (targetEnemy != null)
            {
                Vector3 direction = targetEnemy.transform.position - transform.position;
                direction.y = 0f;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
            }
        }
        else
        {
            targetEnemy = null;
            enemiesInRange.Clear();
        }
    }

    public GameObject GetTargetEnemy()
    {
        return targetEnemy;
    }
}
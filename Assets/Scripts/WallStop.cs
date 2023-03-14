using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStop : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            print(31);
            Vector3 back = Vector3.zero;

            if (0 < player.transform.position.x - transform.position.x) back.x = WallManager.Instance.wallBackDistance;
            else back.x = -WallManager.Instance.wallBackDistance;
            if (0 < player.transform.position.z - transform.position.z) back.z = WallManager.Instance.wallBackDistance;
            else back.z = -WallManager.Instance.wallBackDistance;

            MainManager.Instance.rb.MovePosition(MainManager.Instance.mainPlayer.transform.position + back);
        }
    }
}

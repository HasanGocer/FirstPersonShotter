using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSystem : MonoSingleton<ShotSystem>
{

    //Main karakterin Shot kodu
    public void MainShot()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        Bounds bounds = new Bounds((bottomLeft + topRight) / 2, topRight - bottomLeft);


        RaycastHit[] hits = Physics.RaycastAll(bounds.center, Vector3.forward, bounds.size.z);


        foreach (RaycastHit hit in hits)
        {

        }
    }
}

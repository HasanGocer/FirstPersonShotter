using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesID : MonoBehaviour
{
    [SerializeField] List<GameObject> _skins = new List<GameObject>();
    public List<GameObject> guns = new List<GameObject>();
    public List<GameObject> runPos = new List<GameObject>();
    public List<GameObject> idlePos = new List<GameObject>();

    public int skinCount, gunCount;

    public void StartClothesPlacement()
    {
        _skins[0/*skinCount = Random.Range(0, _skins.Count)*/].SetActive(true);
        guns[gunCount = Random.Range(0, guns.Count)].SetActive(true);
        guns[gunCount].transform.SetParent(runPos[skinCount].transform.parent);
    }
}

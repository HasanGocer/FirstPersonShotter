using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesID : MonoBehaviour
{
    [SerializeField] List<GameObject> _skins = new List<GameObject>();
    [SerializeField] List<GameObject> _guns = new List<GameObject>();

    public int skinCount, gunCount;

    public void StartClothesPlacement()
    {
        _skins[skinCount = Random.Range(0, _skins.Count)].SetActive(true);
        _guns[gunCount = Random.Range(0, _guns.Count)].SetActive(true);
    }
}

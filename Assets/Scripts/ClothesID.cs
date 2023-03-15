using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesID : MonoBehaviour
{
    [SerializeField] CharacterBar _characterBar;
    [SerializeField] List<GameObject> _skins = new List<GameObject>();
    public List<GameObject> guns = new List<GameObject>();
    public List<GameObject> runPos = new List<GameObject>();
    public List<GameObject> idlePos = new List<GameObject>();

    public int gunCount, skinCount, colorCount;


    public GameObject StartClothesPlacement()
    {
        if (_characterBar.characterStat == CharacterBar.CharacterStat.friend)
        {
            gunCount = ColorSelected.Instance.friendGunCount;
            skinCount = ColorSelected.Instance.friendSkinCount;
            colorCount = ColorSelected.Instance.friendColorCount;
        }

        else
        {
            gunCount = ColorSelected.Instance.rivalGunCount;
            skinCount = ColorSelected.Instance.rivalSkinCount;
            colorCount = ColorSelected.Instance.rivalColorCount;
        }



        _skins[skinCount].SetActive(true);
        guns[gunCount].SetActive(true);
        guns[gunCount].transform.SetParent(runPos[skinCount].transform.parent);
        guns[gunCount].transform.GetChild(0).GetComponent<MeshRenderer>().materials[1] = ColorSelected.Instance.playerMaterials[colorCount];
        return guns[gunCount];
    }
}

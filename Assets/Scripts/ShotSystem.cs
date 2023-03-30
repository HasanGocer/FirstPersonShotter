using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShotSystem : MonoSingleton<ShotSystem>
{
    [Header("Main_Field")]
    [Space(10)]

    [SerializeField] GameObject _gunPos;
    [SerializeField] float _hitDistance;
    [SerializeField] GameObject _shotPanel;
    [SerializeField] Button _shotButton;
    private GameObject _camera;
    private Vector3 _standartPos;


    public void ShotSystemStart()
    {
        _shotPanel.SetActive(true);
        _shotButton.onClick.AddListener(Hit);
        _camera = Camera.main.gameObject;
        _standartPos = MainManager.Instance.clothesID.guns[ColorSelected.Instance.friendGunCount].transform.position;
    }

    //Main karakterin Shot kodu
    private void Hit()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.TransformDirection(Vector3.forward) * _hitDistance, out hit, _hitDistance))
            if (hit.collider.gameObject.CompareTag("Wall")) ParticalSystem.Instance.WallShotPartical(hit.point);
            else if (hit.collider.gameObject.CompareTag("RivalPlayer")) RivalHit(hit.point, hit.collider.gameObject);
    }
    private void RivalHit(Vector3 hitPos, GameObject rival)
    {
        RivalID rivalID = rival.GetComponent<RivalID>();
        ItemData.Field field = ItemData.Instance.field;

        rivalID.characterBar.BarUpdate(field.rivalHealth, rivalID.rivalHealth, field.mainDamage);
        rivalID.rivalHealth -= ItemData.Instance.field.mainDamage;
        ParticalSystem.Instance.BodyShotPartical(hitPos);
        StartCoroutine(gunShake());
        ParticalSystem.Instance.ShotGunPartical(MainManager.Instance.clothesID.guns[ColorSelected.Instance.friendGunCount].transform.position + new Vector3(0, 0, 0.8f), rival);
    }
    private IEnumerator gunShake()
    {
        MainManager.Instance.clothesID.guns[ColorSelected.Instance.friendGunCount].transform.DOShakePosition(0.25f, 0.2f);
        yield return new WaitForSeconds(0.3f);
        MainManager.Instance.clothesID.guns[ColorSelected.Instance.friendGunCount].transform.position = _standartPos;
    }
}

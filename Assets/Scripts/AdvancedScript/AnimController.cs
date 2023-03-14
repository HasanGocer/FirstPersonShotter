using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using DG.Tweening;

public class AnimController : MonoBehaviour
{
    [SerializeField] ClothesID _clothesID;
    [SerializeField] private List<AnimancerComponent> character = new List<AnimancerComponent>();
    [SerializeField] private AnimationClip walk, death, idle;

    public void CallIdleAnim()
    {
        character[_clothesID.skinCount].Play(idle, 0.2f);
        _clothesID.guns[_clothesID.gunCount].transform.DOMove(_clothesID.idlePos[_clothesID.gunCount].transform.position, 0.2f);
    }
    public void CallDeadAnim()
    {
        character[_clothesID.skinCount].Play(death, 0.2f);
    }
    public void CallWalkAnim()
    {
        character[_clothesID.skinCount].Play(walk, 0.2f);
        _clothesID.guns[_clothesID.gunCount].transform.DOMove(_clothesID.runPos[_clothesID.gunCount].transform.position, 0.2f);
    }
}

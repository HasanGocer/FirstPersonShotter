using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class AnimController : MonoBehaviour
{
    [SerializeField] ClothesID _clothesID;
    [SerializeField] private List<AnimancerComponent> character = new List<AnimancerComponent>();
    [SerializeField] private AnimationClip walk, death, ýdle;

    public void CallIdleAnim()
    {
        character[_clothesID.skinCount].Play(ýdle, 0.2f);
    }
    public void CallDeadAnim()
    {
        character[_clothesID.skinCount].Play(death, 0.2f);
    }
    public void CallWalkAnim()
    {
        character[_clothesID.skinCount].Play(walk, 0.2f);
    }
}

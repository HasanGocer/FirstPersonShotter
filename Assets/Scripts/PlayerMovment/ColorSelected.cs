using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelected : MonoSingleton<ColorSelected>
{
    public int friendColorCount, rivalColorCount;
    public int friendSkinCount, rivalSkinCount;
    [SerializeField] private int _maxColorCount, _maxSkinCount;
    [SerializeField] private List<Button> _colorButton = new List<Button>();

    public void ColorSelectStart()
    {
        _colorButton[0].onClick.AddListener(() => SelectColor(0));
        _colorButton[1].onClick.AddListener(() => SelectColor(1));
        _colorButton[2].onClick.AddListener(() => SelectColor(2));
        _colorButton[3].onClick.AddListener(() => SelectColor(3));

        if (PlayerPrefs.HasKey("friendSkinCount")) friendSkinCount = PlayerPrefs.GetInt("friendSkinCount");
        else PlayerPrefs.SetInt("friendSkinCount", friendSkinCount);
    }

    public void SelectColor(int colorCount)
    {
        friendColorCount = colorCount;

        while (friendColorCount == (rivalColorCount = Random.Range(0, _maxColorCount))) ;
        while (friendSkinCount == (rivalSkinCount = Random.Range(0, _maxSkinCount))) ;
    }
}
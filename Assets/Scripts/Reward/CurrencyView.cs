using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyView : MonoBehaviour
{
    private const string GoldKey = nameof(GoldKey);
    private const string Lollipop = nameof(Lollipop);
    public static CurrencyView Instance { get; private set; }
    [SerializeField]
    private TMP_Text _currentGold;
    [SerializeField]
    private TMP_Text _currentLollipop;
    private void Awake()
    {

    }
    public void AddGold(int value)
    {
        SaveNewCountInPlayerPrefs(GoldKey, value);
        _currentGold.text = PlayerPrefs.GetInt(GoldKey, 0).ToString();
    }
    public void AddDimond(int value)
    {
        SaveNewCountInPlayerPrefs(Lollipop, value);
        _currentLollipop.text = PlayerPrefs.GetInt(Lollipop, 0).ToString();
    }
    private void SaveNewCountInPlayerPrefs(string key, int value)
    {
        var currentCount = PlayerPrefs.GetInt(key, 0);
        var newCount = currentCount + value;
        PlayerPrefs.SetInt(key, newCount);
    }
}

using TMPro;
using UnityEngine;

public class CurrencyView : MonoBehaviour
{
    private const string GoldKey = nameof(GoldKey);
    private const string LollipopKey = nameof(LollipopKey);
    public static CurrencyView Instance { get; private set; }
    [SerializeField]
    private TMP_Text _currentGold;
    [SerializeField]
    private TMP_Text _currentLollipop;

    public int Gold
    {
        get => PlayerPrefs.GetInt(GoldKey, 0);
        set => PlayerPrefs.SetInt(GoldKey, value);
    }
    public int Lollipop
    {
        get => PlayerPrefs.GetInt(LollipopKey, 0);
        set => PlayerPrefs.SetInt(LollipopKey, value);
    }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        RefreshText();
    }

    private void RefreshText()
    {
        _currentGold.text = Gold.ToString();
        _currentLollipop.text = Lollipop.ToString();
    }

    public void AddGold(int value)
    {
        Gold += value;

        RefreshText();
    }

    public void AddLollipop(int value)
    {
        Lollipop += value;

        RefreshText();
    }
}

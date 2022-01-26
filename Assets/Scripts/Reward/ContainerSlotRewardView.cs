using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContainerSlotRewardView : MonoBehaviour
{
    [SerializeField]
    private Image _backgroundSelect;
    [SerializeField]
    private TMP_Text _textDays;
    [SerializeField]
    private Image _iconCurrency;
    [SerializeField]
    private TMP_Text _countReward;
    public void SetData(Reward reward, int countDate, bool isSelect)
    {
        _iconCurrency.sprite = reward.IconCurrency;
        _textDays.text = $"Day {countDate}";
        _countReward.text = reward.CountCurrency.ToString();
        _backgroundSelect.gameObject.SetActive(isSelect);
    }
}

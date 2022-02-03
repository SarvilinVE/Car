using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightWindowView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _countMoneyText;
    [SerializeField]
    private TMP_Text _countHealthText;
    [SerializeField]
    private TMP_Text _countPowerText;

    [SerializeField]
    private TMP_Text _countPowerEnemyText;

    [SerializeField]
    private Button _addMoneyButton;
    [SerializeField]
    private Button _minusMoneyButton;
    [SerializeField]
    private Button _addHealthButton;
    [SerializeField]
    private Button _minusHealthButton;
    [SerializeField]
    private Button _addPowerButton;
    [SerializeField]
    private Button _minusPowerButton;
    [SerializeField]
    private Button _fightButton;
    [SerializeField]
    private Button _leaveButton;

    public TMP_Text CountMoneyText => _countMoneyText;

    public TMP_Text CountHealthText => _countHealthText;

    public TMP_Text CountPowerText => _countPowerText;

    public TMP_Text CountPowerEnemyText => _countPowerEnemyText;

    public Button AddMoneyButton => _addMoneyButton;

    public Button MinusMoneyButton => _minusMoneyButton;

    public Button AddHealthButton => _addHealthButton;

    public Button MinusHealthButton => _minusHealthButton;

    public Button AddPowerButton => _addPowerButton;

    public Button MinusPowerButton => _minusPowerButton;

    public Button FightButton => _fightButton;

    public Button LeaveButton => _leaveButton;
}

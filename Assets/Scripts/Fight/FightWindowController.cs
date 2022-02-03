using Profile;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class FightWindowController : BaseController
{
    private FightWindowView _fightWindowView;
    private ProfilePlayer _profilePlayer;

    private AssetReference _loadPrefab;
    private AsyncOperationHandle<GameObject> _handle;

    private int _allCountMoneyPlayer;
    private int _allCountHealthPlayer;
    private int _allCountPowerPlayer;

    private Money _money;
    private Health _health;
    private Power _power;

    private Enemy _enemy;

    public FightWindowController (Transform placeForUi, AssetReference loadPrefab, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _loadPrefab = loadPrefab;

        LoadView(_loadPrefab, placeForUi);
    }
    private async void LoadView(AssetReference loadPrefab, Transform placeForUi)
    {
        _handle = Addressables.InstantiateAsync(loadPrefab, placeForUi);
        
        await _handle.Task;

        if(_handle.Status == AsyncOperationStatus.Succeeded)
        {
            _fightWindowView = _handle.Result.GetComponent<FightWindowView>();
            AddGameObjects(_fightWindowView.gameObject);

            RefreshView();
        }
        else
        {
            Debug.Log($"DOWNLOAD LEVEL ERROR!!!");
        }
    }
    public void RefreshView()
    {
        _enemy = new Enemy("AngryBird");

        _money = new Money();
        _money.Attach(_enemy);

        _power = new Power();
        _power.Attach(_enemy);

        _health = new Health();
        _power.Attach(_enemy);

        _fightWindowView.AddHealthButton.onClick.AddListener(() => ChangeHealth(true));
        _fightWindowView.MinusHealthButton.onClick.AddListener(() => ChangeHealth(false));

        _fightWindowView.AddMoneyButton.onClick.AddListener(() => ChangeMoney(true));
        _fightWindowView.MinusMoneyButton.onClick.AddListener(() => ChangeMoney(false));

        _fightWindowView.AddPowerButton.onClick.AddListener(() => ChangePower(true));
        _fightWindowView.MinusPowerButton.onClick.AddListener(() => ChangePower(false));

        _fightWindowView.FightButton.onClick.AddListener(Fight);
        _fightWindowView.LeaveButton.onClick.AddListener(CloseFightWindow);
    }

    private void CloseFightWindow()
    {
        _profilePlayer.CurrentState.Value = GameState.Game;
    }

    protected override void OnDispose()
    {
        _fightWindowView.AddHealthButton.onClick.RemoveAllListeners();
        _fightWindowView.MinusHealthButton.onClick.RemoveAllListeners();

        _fightWindowView.AddMoneyButton.onClick.RemoveAllListeners();
        _fightWindowView.MinusMoneyButton.onClick.RemoveAllListeners();

        _fightWindowView.AddPowerButton.onClick.RemoveAllListeners();
        _fightWindowView.MinusPowerButton.onClick.RemoveAllListeners();

        _fightWindowView.FightButton.onClick.RemoveAllListeners();
        _fightWindowView.LeaveButton.onClick.RemoveAllListeners();

        Addressables.ReleaseInstance(_handle);

        _money.Deattache(_enemy);
        _health.Deattache(_enemy);
        _power.Deattache(_enemy);
        base.OnDispose();
    }

    private void ChangeHealth(bool isAddCount)
    {
        if (isAddCount)
        {
            _allCountHealthPlayer++;
        }
        else
        {
            _allCountHealthPlayer--;
        }

        ChangeDataWindow(_allCountHealthPlayer, DataType.Health);
    }

    private void ChangeMoney(bool isAddCount)
    {
        if (isAddCount)
        {
            _allCountMoneyPlayer++;
        }
        else
        {
            _allCountMoneyPlayer--;
        }

        ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
    }
    private void ChangePower(bool isAddCount)
    {
        if (isAddCount)
        {
            _allCountPowerPlayer++;
        }
        else
        {
            _allCountPowerPlayer--;
        }

        ChangeDataWindow(_allCountPowerPlayer, DataType.Power);
    }
    private void Fight()
    {
        Debug.Log(_allCountPowerPlayer >= _enemy.Power ? $"WIN!" : $"LOSE!");
    }

    private void ChangeDataWindow(int countChangeData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Health:
                _fightWindowView.CountHealthText.text = $"Player Health: {countChangeData}";
                _health.CountHealth = countChangeData;
                break;
            case DataType.Money:
                _fightWindowView.CountMoneyText.text = $"Player Money: {countChangeData}";
                _money.CountMoney = countChangeData;
                break;
            case DataType.Power:
                _fightWindowView.CountPowerText.text = $"Player Power: {countChangeData}";
                _power.CountPower = countChangeData;
                break;
        }

        _fightWindowView.CountPowerEnemyText.text = $"Enemy Power {_enemy.Power}";
    }
}

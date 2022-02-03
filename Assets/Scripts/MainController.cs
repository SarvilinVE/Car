using Profile;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MainController : BaseController
{
    private readonly CurrencyView _currencyView;
    private readonly DailyRewardView _dailyRewardView;
    private readonly FightWindowView _fightWindowView;
    private readonly StartFightView _startFightView;

    private readonly AssetReference _loadPrefab;

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, 
        List<ItemConfig> itemsConfig, CurrencyView currencyView,DailyRewardView dailyRewardView, AssetReference loadPrefab, StartFightView startFightView)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        _itemsConfig = itemsConfig;

        _currencyView = currencyView;
        _dailyRewardView = dailyRewardView;
        _loadPrefab = loadPrefab;
        _startFightView = startFightView;

        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }

    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private InventoryController _inventoryController;
    private DailyRewardController _dailyRewardController;
    private FightWindowController _fightWindowController;
    private StartFightController _startFightController;

    

    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly List<ItemConfig> _itemsConfig;

    protected override void OnDispose()
    {
        AllClear();

        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);

                _gameController?.Dispose();
                _dailyRewardController?.Dispose();
                _fightWindowController?.Dispose();
                break;

            case GameState.Reward:
                _dailyRewardController = new DailyRewardController(_placeForUi, _profilePlayer, _dailyRewardView, _currencyView);

                _dailyRewardController.RefreshView();
                _mainMenuController?.Dispose();
                break;

            case GameState.Game:
                _gameController = new GameController(_profilePlayer);

                _inventoryController = new InventoryController(_itemsConfig, _placeForUi);

                _startFightController = new StartFightController(_profilePlayer, _startFightView, _placeForUi);

                _mainMenuController?.Dispose();
                _dailyRewardController?.Dispose();
                _fightWindowController?.Dispose();
                break;

            case GameState.Fight:
                _fightWindowController = new FightWindowController(_placeForUi, _loadPrefab, _profilePlayer);

                _gameController?.Dispose();
                _inventoryController?.Dispose();
                _startFightController?.Dispose();
                break;

            default:
                AllClear();
                break;
        }
    }

    private void AllClear()
    {
        _inventoryController?.Dispose();
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _dailyRewardController?.Dispose();
        _fightWindowController?.Dispose();
        _startFightController?.Dispose();
    }
}

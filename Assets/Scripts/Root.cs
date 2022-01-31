using Profile;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [Header ("Views:")]
    [SerializeField]
    private CurrencyView _currencyView;
    [SerializeField]
    private DailyRewardView _dailyRewardView;
    [SerializeField]
    private FightWindowView _fightWindowView;
    [SerializeField]
    private StartFightView _startFightView;

    [SerializeField] 
    private Transform _placeForUi;
    
    [SerializeField] 
    private UnityAdsTools _unityAdsTools;

    [SerializeField]
    private List<ItemConfig> _itemConfigs;

    private MainController _mainController;


    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(15f, _unityAdsTools);
        profilePlayer.CurrentState.Value = GameState.Start;
        _mainController = new MainController(_placeForUi, profilePlayer, _itemConfigs, _currencyView, _dailyRewardView, _fightWindowView, _startFightView);
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
    }
}

using Profile;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Root : MonoBehaviour
{
    [Header ("Views:")]
    [SerializeField]
    private CurrencyView _currencyView;
    [SerializeField]
    private DailyRewardView _dailyRewardView;
    [SerializeField]
    private StartFightView _startFightView;
    [Header ("Loader resource:")]
    [SerializeField]
    private AssetReference _loadPrefab;
    [Header("Other settings:")]
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
        _mainController = new MainController(_placeForUi, profilePlayer, _itemConfigs, _currencyView, _dailyRewardView, _loadPrefab, _startFightView);
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
    }
}

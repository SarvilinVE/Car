using System;
using Profile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class DailyRewardController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/RewardWindow" };
    private readonly ProfilePlayer _profilePlayer;
    private readonly DailyRewardView _dailyRewardView;

    private List<ContainerSlotRewardView> _slots = new List<ContainerSlotRewardView>();
    private bool _isGetReward;
    public DailyRewardController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _dailyRewardView = LoadView(placeForUi);

        RefreshView();
    }
    public DailyRewardView LoadView(Transform placeForUi)
    {
        var dailyRewardView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(dailyRewardView);

        return dailyRewardView.GetComponent<DailyRewardView>();
    }
    public void InitSlots()
    {
        for (var i = 0; i < _dailyRewardView.Rewards.Count; i++)
        {
            var instanceSlot = Object.Instantiate(_dailyRewardView.ContainerSlotRewardView, _dailyRewardView.MountRootSlotsReward, false);

            _slots.Add(instanceSlot);
        }
    }
    public void RefreshView()
    {
        InitSlots();

        _dailyRewardView.StartCoroutine(RewardsStartUpdater());

        RefreshUI();

        SubscribesButtons();
    }

    private void OnGame()
    {
        _profilePlayer.CurrentState.Value = GameState.Game;
    }
    private void OnGetReward()
    {
        if (_dailyRewardView.Rewards[_dailyRewardView.CurrentSlotInActive].RewardType == RewardType.Gold)
            CurrencyView.Instance.AddGold(_dailyRewardView.Rewards[_dailyRewardView.CurrentSlotInActive].CountCurrency);
        if(_dailyRewardView.Rewards[_dailyRewardView.CurrentSlotInActive].RewardType == RewardType.Lollipop)
            CurrencyView.Instance.AddDimond(_dailyRewardView.Rewards[_dailyRewardView.CurrentSlotInActive].CountCurrency);
    }

    private void SubscribesButtons()
    {
        _dailyRewardView.StartGameButton.onClick.AddListener(OnGame);
        _dailyRewardView.GetRewardButton.onClick.AddListener(OnGetReward);
    }

    private IEnumerator RewardsStartUpdater()
    {
        while (true)
        {
            RefreshRewardState();
            yield return new WaitForSeconds(1);
        }
    }
    private void RefreshRewardState()
    {
        _isGetReward = true;

        if (_dailyRewardView.TimeGetReward.HasValue)
        {
            var timeSpan = DateTime.UtcNow - _dailyRewardView.TimeGetReward.Value;

            if(timeSpan.Seconds > _dailyRewardView.TimeDeadline)
            {
                _dailyRewardView.TimeGetReward = null;
                _dailyRewardView.CurrentSlotInActive = 0;
            }
            else if(timeSpan.Seconds < _dailyRewardView.TimeCooldown)
            {
                _isGetReward = false;
            }
        }
        RefreshUI();
    }

    private void RefreshUI()
    {
        _dailyRewardView.GetRewardButton.interactable = _isGetReward;

        if (_isGetReward)
        {
            _dailyRewardView.TimerNewReward.text = $"Reward recived"; 
        }
        else
        {
            if(_dailyRewardView.TimeGetReward != null)
            {
                var nextClaimTime = _dailyRewardView.TimeGetReward.Value.AddSeconds(_dailyRewardView.TimeCooldown);
                var currentClimeCooldown = nextClaimTime - DateTime.UtcNow;
                var timeGetReward = $"{currentClimeCooldown.Days:D2} : {currentClimeCooldown.Hours : D2} : {currentClimeCooldown.Minutes : D2} . {currentClimeCooldown.Seconds : D2}";
                _dailyRewardView.TimerNewReward.text = timeGetReward; 
            }
        }

        for(var i = 0; i< _slots.Count; i++)
        {
            _slots[i].SetData(_dailyRewardView.Rewards[i], i + 1, i == _dailyRewardView.CurrentSlotInActive);
        }
    }

    
}

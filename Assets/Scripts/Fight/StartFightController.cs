using UnityEngine;


public class StartFightController : BaseController
{
    private StartFightView _startFightView;
    private ProfilePlayer _profilePlayer;


    public StartFightController(ProfilePlayer profilePlayer, StartFightView view, Transform placeForUi)
    {
        _profilePlayer = profilePlayer;

        _startFightView = Object.Instantiate(view, placeForUi);
        AddGameObjects(_startFightView.gameObject);

        SubscribesButtons();
    }

    private void SubscribesButtons()
    {
        _startFightView.StartFightWindow.onClick.AddListener(StartFight);
    }

    private void StartFight()
    {
        _profilePlayer.CurrentState.Value = Profile.GameState.Fight;
    }

    protected override void OnDispose()
    {
        _startFightView.StartFightWindow.onClick.RemoveAllListeners();
        base.OnDispose();
    }
}

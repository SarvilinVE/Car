using UnityEngine;

public class Enemy : IEnemy
{
    private string _name;

    private int _moneyPlayer;
    private int _healthPlayer;
    private int _powerPlayer;

    public Enemy(string name)
    {
        _name = name;
    }
    public void Updater(DataPlayer dataPlayer, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Health:
                var dataHealth = (Health)dataPlayer;
                _healthPlayer = dataHealth.CountHealth;
                break;
            case DataType.Money:
                var dataMoney = (Money)dataPlayer;
                _moneyPlayer = dataMoney.CountMoney;
                break;
            case DataType.Power:
                var dataPower = (Power)dataPlayer;
                _powerPlayer = dataPower.CountPower;
                break;
        }
        Debug.Log($"Enemy {_name} change {dataType}");
    }
    public int Power
    {
        get
        {
            var power = _moneyPlayer + _healthPlayer - _powerPlayer;
            return power;
        }
    }
}

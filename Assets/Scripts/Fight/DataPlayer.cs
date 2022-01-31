using System.Collections.Generic;

public class DataPlayer
{
    private List<IEnemy> _enemies = new List<IEnemy>();
    public void Attach(IEnemy enemy)
    {
        _enemies.Add(enemy);
    }
    public void Deattache(IEnemy enemy)
    {
        _enemies.Remove(enemy);
    }
    protected void Notifier(DataType dataType)
    {
        foreach(var eneme in _enemies)
        {
            eneme.Updater(this, dataType); 
        }
    }
}
public class Money : DataPlayer
{
    private int _countMoney;
    public int CountMoney
    {
        get => _countMoney;
        set
        {
            if(_countMoney != value)
            {
                _countMoney = value;
                Notifier(DataType.Money);
            }
        }
    }
}
public class Health : DataPlayer
{
    private int _countHealth;
    public int CountHealth
    {
        get => _countHealth;
        set
        {
            if (_countHealth != value)
            {
                _countHealth = value;
                Notifier(DataType.Health);
            }
        }
    }
}
public class Power : DataPlayer
{
    private int _countPower;
    public int CountPower
    {
        get => _countPower;
        set
        {
            if (_countPower != value)
            {
                _countPower = value;
                Notifier(DataType.Power);
            }
        }
    }
} 

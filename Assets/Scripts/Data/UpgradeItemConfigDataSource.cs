using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeItemConfigDataSource", menuName = "UpgradeItemConfigDataSource")]
public class UpgradeItemConfigDataSource : ScriptableObject
{
    [SerializeField]
    private UpgradeItemConfig[] _itemConfigs;

    public UpgradeItemConfig[] ItemConfigs => _itemConfigs;
}

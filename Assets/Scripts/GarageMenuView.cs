using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GarageMenuView : MonoBehaviour
{
    [SerializeField]
    private List<Button> _slots;

    public void Init(UnityAction addInventory)
    {
        foreach (var slot in _slots)
        {
            slot.onClick.AddListener(addInventory);
        }
    }

    protected void OnDestroy()
    {
        foreach(var slot in _slots)
        {
            slot.onClick.RemoveAllListeners();
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour, IInventoryView
{
    [SerializeField]
    private Button[] _slots;

    private Dictionary<Button, IItem> _activeSlot = new Dictionary<Button, IItem>();


    public void Init()
    {
    }
    public void OnSelected(Button slot,IItem item)
    {
        Debug.Log($"Click {slot.name} {item.Info.Title}");
        slot.onClick.RemoveAllListeners();
        slot.onClick.AddListener(() => OnDeselected(slot, item));

    }
    public void OnDeselected(Button slot, IItem item)
    {
        Debug.Log($"Click Deselected {slot.name} {item.Info.Title}");
        slot.onClick.RemoveAllListeners();
        slot.onClick.AddListener(() => OnSelected(slot, item));
    }
    public void Display(IReadOnlyList<IItem> items)
    {
        int index = 0;
        foreach(var item in items)
        {
            _slots[index].GetComponent<Image>().sprite = item.Info.Icon;
            _slots[index].interactable = true;
            _activeSlot.Add(_slots[index], item);
            index++;
        }
        foreach(var slot in _activeSlot)
        {
            if (slot.Key.interactable)
            {
                slot.Key.onClick.AddListener(() => OnSelected(slot.Key, slot.Value));
            }
        }
    }
}

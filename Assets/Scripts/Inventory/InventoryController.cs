using System.Collections.Generic;
using UnityEngine;

public class InventoryController : BaseController, IInventoryController
{
    private readonly IInventoryModel _inventoryModel;
    private readonly IInventoryView _inventoryView;
    private readonly IItemsRepository _itemsRepository;

    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/garageMenu" };

    public InventoryController(List<ItemConfig> itemConfigs, Transform placeForUi)
    {
        _inventoryModel = new InventoryModel();
        _itemsRepository = new ItemsRepository(itemConfigs);

        _inventoryView = LoadView(placeForUi);
        ShowInventory();
    }

    public IInventoryView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);

        return objectView.GetComponent<InventoryView>();
    }
    public void ShowInventory()
    {
        foreach (var item in _itemsRepository.Items.Values)
            _inventoryModel.EquipItem(item);

        var equippedItems = _inventoryModel.GetEquippedItems();
        _inventoryView.Display(equippedItems);
    }
}

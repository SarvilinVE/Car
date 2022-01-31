using UnityEngine;

public class CurrencyController : BaseController
{
    public CurrencyController(Transform placeForUi, CurrencyView currencyView)
    {
        var currencyViewInstance = Object.Instantiate(currencyView, placeForUi);
        AddGameObjects(currencyViewInstance.gameObject);
    }
}

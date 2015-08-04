using UnityEngine;
using System.Collections.Generic;
using Soomla;
using Soomla.Store;

using UnityEngine.UI;

public class StorePurchase : MonoBehaviour
{

    public Text StatusText;
    public Text CurrentInGameCurrency;

    void Start()
    {

        SoomlaStore.Initialize(new Store());

        StoreEvents.OnMarketPurchaseStarted += OnMarketPurchaseStarted;
        StoreEvents.OnMarketPurchase += OnMarketPurchase;
        StoreEvents.OnItemPurchaseStarted += OnItemPurchaseStarted;
        StoreEvents.OnItemPurchased += OnItemPurchased;
    }

    string s = "<nothing>";

    public void OnMarketPurchaseStarted(PurchasableVirtualItem pvi)
    {
        StatusText.text = "Market Purchase started";

        Debug.Log("OnMarketPurchaseStarted: " + pvi.ItemId);
        s += "OnMarketPurchaseStarted: " + pvi.ItemId;
    }

    public void OnMarketPurchase(PurchasableVirtualItem pvi, string s1, Dictionary<string, string> arg3)
    {
        Debug.Log("OnMarketPurchase: " + pvi.ItemId);
        s += "OnMarketPurchase: " + pvi.ItemId;
        StatusText.text = s;
    }

    public void OnItemPurchaseStarted(PurchasableVirtualItem pvi)
    {
        StatusText.text = "Item Purchase started";

        Debug.Log("OnItemPurchaseStarted: " + pvi.ItemId);
        s += "OnItemPurchaseStarted: " + pvi.ItemId;
        
    }

    public void OnItemPurchased(PurchasableVirtualItem pvi, string s1)
    {
        Debug.Log("OnItemPurchased: " + pvi.ItemId);
        s += "OnItemPurchased: " + pvi.ItemId;
        StatusText.text = s;
        CurrentInGameCurrency.text = Store.DIAMOND_CURRENCY.GetBalance().ToString() + " Diamonds";
    }

    public void OnStoreControllerInitialized()
    {
        Debug.Log("OnStoreControllerInitialized");
        s += "OnStoreControllerInitialized";
        StatusText.text = s;
    }

    public void OnUnexpectedErrorInStore(string err)
    {
        Debug.Log("OnUnexpectedErrorInStore" + err);
        s += "OnUnexpectedErrorInStore" + err;
    }

    public void PurchaseCurrency(string amount)
    {
        //Store.HUND_DIAMONDS_PACK.Buy(amount);

        StoreInventory.BuyItem(Store.HUND_DIAMONDS_PACK.ItemId);
    
    }
    public void PurchaseTimeMachine(string amount)
    {
        StoreInventory.BuyItem(Store.NO_ADS_LTVG.ItemId);
    }
}

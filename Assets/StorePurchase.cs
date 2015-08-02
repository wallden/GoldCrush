﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;
using Soomla.Store.Example;
using UnityEngine.UI;

public class StorePurchase : MonoBehaviour
{

    public Text StatusText;
    public Text CurrentInGameCurrency;
    void Start()
    {
        StoreEvents.OnMarketPurchase += OnMarketPurchase;

    }

    void Update()
    {
        CurrentInGameCurrency.text = Store.DIAMOND_CURRENCY.GetBalance().ToString();

    }

    private void OnMarketPurchase(PurchasableVirtualItem purchasableVirtualItem, string s, Dictionary<string, string> arg3)
    {
        StatusText.text = "You purchased" + " " + purchasableVirtualItem.Name;
    }

    public void PurchaseCurrency(string amount)
    {
        Store.HUND_DIAMONDS_PACK.Buy(amount);

    }
    public void PurchaseTimeMachine(string amount)
    {
        StoreInventory.BuyItem("1", "1");
    }
}

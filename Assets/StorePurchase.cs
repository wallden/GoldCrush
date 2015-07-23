using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;
using Soomla.Store.Example;
using UnityEngine.UI;

public class StorePurchase : MonoBehaviour
{

    public Text statusText;
    void Start ()
	{
	    StoreEvents.OnMarketPurchase += OnMarketPurchase;
	}
	
	void Update () {
	
	}
    private void OnMarketPurchase(PurchasableVirtualItem purchasableVirtualItem, string s, Dictionary<string, string> arg3)
    {
        var a = "";
        statusText.text = "You purchased" +" "+ purchasableVirtualItem.Name;
    }
    
  public void PurchaseTimeMachine(string amount)
    {
        //Store.TIMEMACHINE_GOOD.Buy(amount);
      StoreInventory.BuyItem("1", "1");
    }
}

using System.Collections.Generic;
using Soomla.Store;

public class StoreEventHandler
{
	public StoreEventHandler()
	{
		StoreEvents.OnSoomlaStoreInitialized += OnSoomlaStoreInitialized;
		StoreEvents.OnMarketPurchase += OnMarketPurchase;
	}

	private void OnMarketPurchase(PurchasableVirtualItem purchasableVirtualItem, string s, Dictionary<string, string> arg3)
	{
		var a = "";
	}

	private void OnSoomlaStoreInitialized()
	{
		Store.TIMEMACHINE_GOOD.Buy("1");
	}

}

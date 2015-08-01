using System.Collections.Generic;
using Soomla.Store;

public class Store : IStoreAssets
{

	public int GetVersion()
	{
		return 0;
	}

	// NOTE: Even if you have no use in one of these functions, you still need to
	// implement them all and just return an empty array.

	public VirtualCurrency[] GetCurrencies()
	{
		return new VirtualCurrency[] { DIAMOND_CURRENCY };
	}

	public VirtualGood[] GetGoods()
	{
		return new VirtualGood[] { TIMEMACHINE_GOOD, NO_ADS_LTVG };
	}

	public VirtualCurrencyPack[] GetCurrencyPacks()
	{
		return new VirtualCurrencyPack[] { HUND_DIAMONDS_PACK };
	}

	public VirtualCategory[] GetCategories()
	{
		return new VirtualCategory[] { GENERAL_CATEGORY };
	}

	/** Virtual Currencies **/

	public static VirtualCurrency DIAMOND_CURRENCY = new VirtualCurrency(
	  "Diamond",                               // Name
	  "Diamond currency",                      // Description
	  "Diamond_currency_ID"                    // Item ID
	);

	/** Virtual Currency Packs **/

	public static VirtualCurrencyPack HUND_DIAMONDS_PACK = new VirtualCurrencyPack(
	  "100 Diamonds",                          // Name
	  "100 Diamond currency units",            // Description
	  "diamonds_100_ID",                       // Item ID
	  100,                                  // Number of currencies in the pack
	  "Diamond_currency_ID",                   // ID of the currency associated with this pack
	  new PurchaseWithMarket(               // Purchase type (with real money $)
		"diamonds_100_PROD_ID",                   // Product ID
		1.99                                   // Price (in real money $)
	  )
	);

	/** Virtual Goods **/

	public static VirtualGood TIMEMACHINE_GOOD = new SingleUseVG(
	  "Time machine",                             // Name
	  "Go forward in time",      // Description
	  "1",                          // Item ID
	  new PurchaseWithMarket(          // Purchase type (with virtual currency)
	   "1",                     // ID of the item used to pay with
		0.99                                    // Price (amount of coins)
	  )
	);

	// NOTE: Create non-consumable items using LifeTimeVG with PurchaseType of PurchaseWithMarket.
	public static VirtualGood NO_ADS_LTVG = new LifetimeVG(
	  "No Ads",                             // Name
	  "No More Ads!",                       // Description
	  "no_ads_ID",                          // Item ID
	  new PurchaseWithMarket(               // Purchase type (with real money $)
		"no_ads_PROD_ID",                      // Product ID
		0.99                                   // Price (in real money $)
	  )
	);

	/** Virtual Categories **/

	public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory(
	  "General", new List<string>(new string[] { TIMEMACHINE_GOOD.ID })
	);
}

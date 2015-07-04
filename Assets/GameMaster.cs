using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine.UI;
using Object = System.Object;

public class GameMaster : MonoBehaviour
{
    public GameObject CoalAutoClicker;
    public Text Text;
    public Dictionary<string,int> MineralIncomes { get; set; }
    public GameMaster()
    {
        MineralIncomes = new Dictionary<string, int>
        {
            {"Coal", 5},
            {"Aluminum", 10}
        };
    }
    private int _currentMoney;
	// Use this for initialization
	void Start ()
	{
	  
	}
	
	// Update is called once per frame
	void Update ()
	{
	  
	}

    public void PlayerBuyAutoClicker(string type)
    {
        switch (type)
        {
            case "Coal":
                Instantiate(CoalAutoClicker);
                break;
        }
    }
    

    private void IncreaseProgressBar(Object source, ElapsedEventArgs e)
    {        
        Debug.Log(e.SignalTime);
    }

    public void AddCurrency(string type)
    {
        var amount = MineralIncomes[type];
        _currentMoney += amount;
        SetCurrency();
    }

    private void SetCurrency()
    {
        Text.text = "$" + _currentMoney.ToString();
    }
}

public class MiningPick
{
    public string Name;
    public float Cooldown;
    public int Cost;

    public MiningPick()
    {
        Name = "MiningPick";
        Cooldown = 1f;
        Cost = 25;
    }
}



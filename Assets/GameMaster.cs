using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public GameObject CoalAutoClicker;
    public Text Text;

    private Dictionary<string, Mineral> Minerals { get; set; }
    private int _currentMoney;

    public GameMaster()
    {
        Minerals = new Dictionary<string, Mineral>
            {
                { "Coal", new Mineral { Name = "Coal", Cooldown = 5, Income = 5 } },
                { "Aluminum", new Mineral { Name = "Aluminum", Cooldown = 10, Income = 10 } }
            };
    }

    public void Start()
    {
        SetCurrency();
    }

    public void PlayerBuyAutoClicker(string type)
    {
        var clickGenerator = Instantiate(CoalAutoClicker).GetComponent<ClickGenerator>();
        clickGenerator.Initialize(this, Minerals[type]);
    }

    private void IncreaseProgressBar(Object source, ElapsedEventArgs e)
    {
        Debug.Log(e.SignalTime);
    }

    public void AddCurrency(string type)
    {
        var mineral = Minerals[type];
        _currentMoney += mineral.Income;
        SetCurrency();
    }

    private void SetCurrency()
    {
        Text.text = "$" + _currentMoney;
    }
}

public class Mineral
{
    public string Name;
    public float Cooldown;
    public int Income;
}
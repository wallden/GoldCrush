using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public GameObject CoalAutoClicker;
    public Text Text;

    private Dictionary<string, ClickerType> Clickers { get; set; }
    private int _currentMoney;

    public GameMaster()
    {
        Clickers = new Dictionary<string, ClickerType>
            {
                { "Grandma", new ClickerType { Name = "Grandma", Cooldown = 10, Income = 5 } },
                { "Worker", new ClickerType { Name = "Worker", Cooldown = 7, Income = 10 } },
                { "Foreman", new ClickerType { Name = "Foreman", Cooldown = 5, Income = 20 } },
                { "Driller", new ClickerType { Name = "Driller", Cooldown = 4, Income = 40 } },
                { "Digger", new ClickerType { Name = "Digger", Cooldown = 3, Income = 80 } },
                { "AlienRobot", new ClickerType { Name = "AlienRobot", Cooldown = 3, Income = 80 } },
            };
    }

    public void Start()
    {
        SetCurrency();
    }

    public void PlayerBuyAutoClicker(string type)
    {
        var clickGenerator = Instantiate(CoalAutoClicker).GetComponent<ClickGenerator>();
        clickGenerator.Initialize(this, Clickers[type]);
    }

    private void IncreaseProgressBar(Object source, ElapsedEventArgs e)
    {
        Debug.Log(e.SignalTime);
    }

    public void AddCurrency(int income)
    {
        _currentMoney += income;
        SetCurrency();
    }

    private void SetCurrency()
    {
        Text.text = "$" + _currentMoney;
    }
}

public class ClickerType
{
    public string Name;
    public float Cooldown;
    public int Income;
}
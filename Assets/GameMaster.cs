using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Timers;
using Soomla.Store;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public GameObject CoalAutoClicker;
    public GameObject Ground;
    public GameObject CameraFocusPoint;
    public Vector3 FocusPointOffset;
    public Text Text;

    private Dictionary<string, ClickerType> Clickers { get; set; }
    private int _currentMoney;
    private int _groundsDestroyed;
    public GameMaster()
    {
        Clickers = new Dictionary<string, ClickerType>
            {
                { "Grandma", new ClickerType { Name = "Grandma", Cooldown = 10, Income = 2, Cost = 5} },
                { "Worker", new ClickerType { Name = "Worker", Cooldown = 7, Income = 3, Cost = 5 } },
                { "Foreman", new ClickerType { Name = "Foreman", Cooldown = 5, Income = 20, Cost = 5  } },
                { "Driller", new ClickerType { Name = "Driller", Cooldown = 4, Income = 40, Cost = 5  } },
                { "Digger", new ClickerType { Name = "Digger", Cooldown = 3, Income = 80, Cost = 5  } },
                { "AlienRobot", new ClickerType { Name = "AlienRobot", Cooldown = 3, Income = 80, Cost = 5  } },
            };

        GroundBlocks = new List<Clickable>();
    }

    public List<Clickable> GroundBlocks { get; set; }

    public void Start()
    {
        Initialize();
        SoomlaStore.Initialize(new Store());
    }

    private void Initialize()
    {
        SetCurrency();
        InitializeGround();
    }

    private void InitializeGround()
    {
        for (int i = 0; i < 5; i++)
        {
            GenerateGround();
        }
    }

    public void GenerateGround()
    {
        var ground = Instantiate(Ground).GetComponent<Clickable>();

        var groundPosition = GroundBlocks.Count > 0 ? GroundBlocks.Last().transform.position : FocusPointOffset;
        groundPosition.y -= ground.transform.localScale.y;
        ground.Initialize(this, 2, groundPosition);
        GroundBlocks.Add(ground);
    }

    public void PlayerBuyAutoClicker(string type)
    {
        RemoveCurrency(Clickers[type].Cost);
        var clickGenerator = Instantiate(CoalAutoClicker).GetComponent<ClickGenerator>();
        clickGenerator.Initialize(this, Clickers[type]);
    }

    public void GroundDestroyed(Clickable clickable)
    {
        GroundBlocks.Remove(clickable);

        _groundsDestroyed++;
        if (_groundsDestroyed >= 3)
        {
            var newY = GroundBlocks.First().transform.position.y;
            CameraFocusPoint.transform.position = CameraFocusPoint.transform.position + new Vector3(0, newY - CameraFocusPoint.transform.position.y, 0) - FocusPointOffset;
            _groundsDestroyed = 0;
        }

        GenerateGround();
    }

    public void AddCurrency(int amount)
    {
        _currentMoney += amount;
        SetCurrency();
    }
    public void RemoveCurrency(int amount)
    {
        _currentMoney -= amount;
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
    public int Cost;
}
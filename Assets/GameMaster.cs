using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public GameObject AutoClickerTemplate;
    public GameObject Ground;
    public GameObject CameraFocusPoint;
    public Vector3 FocusPointOffset;
    public Text Text;

    private Dictionary<string, ClickerType> Clickers { get; }
    private int _currentMoney;
    private int _groundsDestroyed;
    private Vector3 _groundHeight;

    public GameMaster()
    {
        Clickers = new Dictionary<string, ClickerType>
            {
                { "Grandma", new ClickerType { Name = "Grandma", Cooldown = 5, DigTime = 5, Income = 2, Cost = 5} },
                { "Worker", new ClickerType { Name = "Worker", Cooldown = 4, DigTime = 4, Income = 3, Cost = 5 } },
                { "Foreman", new ClickerType { Name = "Foreman", Cooldown = 3.5f, DigTime = 3.5f, Income = 20, Cost = 5  } },
                { "Driller", new ClickerType { Name = "Driller", Cooldown = 3, DigTime = 3, Income = 40, Cost = 5  } },
                { "Digger", new ClickerType { Name = "Digger", Cooldown = 2.5f, DigTime = 2.5f, Income = 80, Cost = 5  } },
                { "AlienRobot", new ClickerType { Name = "AlienRobot", Cooldown = 2, DigTime = 2, Income = 80, Cost = 5  } },
            };

        GroundBlocks = new List<Clickable>();
    }

    private Vector3 GroundLevel { get
    {
        var lastBlockPosition = GroundBlocks.Count > 0 ? GroundBlocks.Last().transform.position : FocusPointOffset;
        return lastBlockPosition;
    }
    }

    public List<Clickable> GroundBlocks { get; set; }

    public void Start()
    {
        Initialize();
        _groundHeight = new Vector3(0, Ground.transform.localScale.y);
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
        ground.Initialize(this, 2, groundPosition - _groundHeight);
        GroundBlocks.Add(ground);
    }

    public void PlayerBuyAutoClicker(string type)
    {
        RemoveCurrency(Clickers[type].Cost);
        var clickGenerator = Instantiate(AutoClickerTemplate).GetComponent<ClickGenerator>();
        clickGenerator.Initialize(this, Clickers[type]);
        clickGenerator.transform.position = FocusPointOffset;
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
    public float DigTime;
    public int Income;
    public int Cost;
}
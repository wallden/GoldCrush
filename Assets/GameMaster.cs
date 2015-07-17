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

    private Dictionary<string, ClickerType> Clickers { get; set; }
    private List<ClickGenerator> ActiveAutoclickers { get; set; }
    private int _currentMoney;
    private int _groundsDestroyed;
    private float _groundHeightOffset;

    public GameMaster()
    {
        Clickers = new Dictionary<string, ClickerType>
            {
                { "Grandma", new ClickerType { Name = "Grandma", MoveTime = 5, DigTime = 5, Income = 2, Cost = 5} },
                { "Worker", new ClickerType { Name = "Worker", MoveTime = 4, DigTime = 4, Income = 3, Cost = 5 } },
                { "Foreman", new ClickerType { Name = "Foreman", MoveTime = 3.5f, DigTime = 3.5f, Income = 20, Cost = 5  } },
                { "Driller", new ClickerType { Name = "Driller", MoveTime = 3, DigTime = 3, Income = 40, Cost = 5  } },
                { "Digger", new ClickerType { Name = "Digger", MoveTime = 2.5f, DigTime = 2.5f, Income = 80, Cost = 5  } },
                { "AlienRobot", new ClickerType { Name = "AlienRobot", MoveTime = 2, DigTime = 2, Income = 80, Cost = 5  } },
            };

        ActiveAutoclickers = new List<ClickGenerator>();
        GroundBlocks = new List<Clickable>();
    }

    private float GroundLevel
    {
        get
        {
            var lastBlockPosition = GroundBlocks.Count > 0 ? GroundBlocks.First().transform.position.y : 0;
            return lastBlockPosition - _groundHeightOffset;
        }
    }

    public List<Clickable> GroundBlocks { get; set; }

    public void Start()
    {
        _groundHeightOffset = -Ground.transform.localScale.y/2;

        Initialize();
        //SoomlaStore.Initialize(new Store());
    }

    private void Initialize()
    {
        SetCurrency();
        InitializeGround();
    }

    private void InitializeGround()
    {
        for (int i = 0; i < 8; i++)
        {
            GenerateGround();
        }
    }

    public void GenerateGround()
    {
        var ground = Instantiate(Ground).GetComponent<Clickable>();

        var groundPosition = GroundBlocks.Count > 0 ? GroundBlocks.Last().transform.position : FocusPointOffset;
        ground.Initialize(this, 2, groundPosition + new Vector3(0, _groundHeightOffset));
        GroundBlocks.Add(ground);
    }

    public void PlayerBuyAutoClicker(string type)
    {
        RemoveCurrency(Clickers[type].Cost);
        var clickGenerator = Instantiate(AutoClickerTemplate).GetComponent<ClickGenerator>();
        clickGenerator.Initialize(this, Clickers[type], 8);
        clickGenerator.transform.position = new Vector3(0, GroundLevel);
        ActiveAutoclickers.Add(clickGenerator);
    }

    public void GroundDestroyed(Clickable clickable)
    {
        GroundBlocks.Remove(clickable);

        _groundsDestroyed++;
        if (_groundsDestroyed >= 3)
        {
            CameraFocusPoint.transform.position = CameraFocusPoint.transform.position.SetY(0) + new Vector3(0, GroundLevel) - FocusPointOffset;
            _groundsDestroyed = 0;
        }

        GenerateGround();
        ActiveAutoclickers.ForEach(x => x.GroundRemoved(GroundLevel));
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
    public float MoveTime;
    public float DigTime;
    public int Income;
    public int Cost;
}
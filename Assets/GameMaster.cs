using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Soomla.Store;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameMaster : MonoBehaviour
{
    public GameObject AutoClickerTemplate;
    public ParticleSystem MouseClickParticleSystem;
    public GameObject Ground;
    public GameObject BuyAutoClickerButton;
    public GameObject BuyUpgradeButton;
    public GameObject CameraFocusPoint;
    public Vector3 FocusPointOffset;
    public Text Text;
    public RectTransform AutoClickerBuyWindow;
    public RectTransform UpgradesBuyWindow;

    public static int MoneyPerClick = 1;
    public static int IncomeUpgradeValue = 2;
    public static float MoveAndDigUpgradeValue = 1.3f;

    private Dictionary<string, ClickerType> Clickers { get; set; }
    public Dictionary<string, ClickerType> Upgrades { get; set; }
    private List<ClickGenerator> ActiveAutoclickers { get; set; }
    public static int CurrentMoney;
    private int _groundsDestroyed;
    private float _groundHeightOffset;
    
    private const int MaxVisibleClickers = 8;

    public GameMaster()
    {
        Clickers = new Dictionary<string, ClickerType>
            {
                { "Grandma", new ClickerType { Name = "Grandma", MoveTime = 5, MoveSpeed = 0.3f, DigTime = 5, Income = 2, Cost = 10}},
                { "Worker", new ClickerType { Name = "Worker", MoveTime = 4, MoveSpeed = 0.5f, DigTime = 4, Income = 3, Cost = 20 } },
                { "Foreman", new ClickerType { Name = "Foreman", MoveTime = 3.5f, MoveSpeed = 0.55f, DigTime = 3.5f, Income = 5, Cost = 30  } },
                { "Driller", new ClickerType { Name = "Driller", MoveTime = 3, MoveSpeed = 0.65f, DigTime = 3, Income = 8, Cost = 50  } },
                { "Digger", new ClickerType { Name = "Digger", MoveTime = 2.5f, MoveSpeed = 0.8f, DigTime = 2.5f, Income = 13, Cost = 80  } },
                { "AlienRobot", new ClickerType { Name = "AlienRobot", MoveTime = 2, MoveSpeed = 1f, DigTime = 2, Income = 21, Cost = 100  } },
            };

        Upgrades = new Dictionary<string, ClickerType>
            {
                { "Grandma IncomeUpgrade", new ClickerType { Name = "Grandma IncomeUpgrade", Cost = 10}},
                { "Grandma MoveAndDigUpgrade", new ClickerType { Name = "Grandma MoveAndDigUpgrade", Cost = 10}},
                { "Worker IncomeUpgrade", new ClickerType { Name = "Worker IncomeUpgrade", Cost = 20 } },
                { "Worker MoveAndDigUpgrade", new ClickerType { Name = "Worker MoveAndDigUpgrade", Cost = 20 } },
                { "MouseClick ClickUpgrade", new ClickerType { Name = "MouseClick ClickUpgrade", Cost = 20 } },
            };
        ActiveAutoclickers = new List<ClickGenerator>();
        GroundBlocks = new List<Clickable>();
    }


    void Update()
    {
        var autoClickerToUnlock = Clickers.Where(x => !x.Value.SillhouetteUnlocked).OrderBy(x => x.Value.Cost).FirstOrDefault();
        if (autoClickerToUnlock.Value != null)
        {
            if (autoClickerToUnlock.Value.Cost / 4 <= CurrentMoney)
            {
                autoClickerToUnlock.Value.SillhouetteUnlocked = true;
                AddAutoClickerButtonToMenu(autoClickerToUnlock.Value);
            }
        }
        var upgradeToUnlock = Upgrades.Where(x => !x.Value.SillhouetteUnlocked).OrderBy(x => x.Value.Cost).FirstOrDefault();
        if (upgradeToUnlock.Value != null)
        {
            if (upgradeToUnlock.Value.Cost / 4 <= CurrentMoney)
            {
                upgradeToUnlock.Value.SillhouetteUnlocked = true;
                AddUpgradeButtonToMenu(upgradeToUnlock.Value);
            }
        }


    }
    private float GroundLevel
    {
        get
        {
            var lastBlockPosition = GroundBlocks.Count > 0 ? CurrentGround.transform.position.y : 0;
            return lastBlockPosition - _groundHeightOffset;
        }
    }

    private Clickable CurrentGround
    {
        get { return GroundBlocks.First(); }
    }

    public List<Clickable> GroundBlocks { get; set; }

    public void Start()
    {
        var spriteHeight = Ground.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        _groundHeightOffset = -Ground.transform.localScale.y * spriteHeight / 2;

        Initialize();
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
        ground.ParticleSystem = MouseClickParticleSystem;

        var groundPosition = GroundBlocks.Count > 0 ? GroundBlocks.Last().transform.position : FocusPointOffset;
        ground.Initialize(this, _groundsDestroyed, groundPosition + new Vector3(0, _groundHeightOffset, 0));
        GroundBlocks.Add(ground);
    }

    public UnityAction PlayerBuyAutoClicker(string type)
    {
        RemoveCurrency(Clickers[type].Cost);

        if (ActiveAutoclickers.Count >= MaxVisibleClickers)
        {
            var alreadyExistOfSameType = ActiveAutoclickers.Where(x => x.ClickerType.Name == type).ToList();

            var clickerTypeWithLargestNumberOfVisible = ActiveAutoclickers
                .GroupBy(x => x.ClickerType.Name)
                .OrderByDescending(x => x.Count())
                .First();

            if (alreadyExistOfSameType.Count() != 0 && clickerTypeWithLargestNumberOfVisible.Key == type)
            {
                StackOntoExisting(alreadyExistOfSameType);
            }
            else
            {
                if(clickerTypeWithLargestNumberOfVisible.Count() > 1)
                {
                    MergeAndDestroyExistingClicker(clickerTypeWithLargestNumberOfVisible);
                }

                SpawnClicker(type);
            }
        }
        else
        {
            SpawnClicker(type);
        }

        return null;
    }
    public UnityAction PlayerBuyUpgrade(string type)
    {
        var nameAndUpgradeTypeArray = type.Split(' ');
        RemoveCurrency(Upgrades[type].Cost);

        switch (nameAndUpgradeTypeArray[1])
        {
            case("IncomeUpgrade"):
                Clickers[nameAndUpgradeTypeArray[0]].Income *= IncomeUpgradeValue;
                ActiveAutoclickers.Where(x => x.ClickerType.Name == nameAndUpgradeTypeArray[0]).ToList().ForEach(x => x.ClickerType.Income *= IncomeUpgradeValue);
        
                break;
            case("MoveAndDigUpgrade"):
                Clickers[nameAndUpgradeTypeArray[0]].DigTime *= 2-MoveAndDigUpgradeValue;
                Clickers[nameAndUpgradeTypeArray[0]].MoveSpeed *= MoveAndDigUpgradeValue;
                Clickers[nameAndUpgradeTypeArray[0]].MoveTime *= 2-MoveAndDigUpgradeValue;
                var list = ActiveAutoclickers.Where(x => x.ClickerType.Name == nameAndUpgradeTypeArray[0]).ToList();
                foreach (var clickGenerator in list)
                {
                    clickGenerator.ClickerType.DigTime *= 2-MoveAndDigUpgradeValue;
                    clickGenerator.ClickerType.MoveSpeed *= MoveAndDigUpgradeValue;
                    clickGenerator.ClickerType.MoveTime *= 2-MoveAndDigUpgradeValue;
                }
                break;
            case("ClickUpgrade"):
                MoneyPerClick += 1;
                break;
        }
        
        return null;
    }

    private void MergeAndDestroyExistingClicker(IGrouping<string, ClickGenerator> clickerTypeWithLargestNumberOfVisible)
    {
        var clickersWithLeastStacked = clickerTypeWithLargestNumberOfVisible
            .OrderBy(x => x.StackedClickers)
            .Take(2)
            .ToList();

        var existingToMerge = clickersWithLeastStacked
            .Skip(1)
            .First();

        clickersWithLeastStacked
            .First()
            .MergeExistingClicker(
                existingToMerge);

        DestroyClicker(existingToMerge);
    }

    private static void StackOntoExisting(List<ClickGenerator> existingClickersOfSameType)
    {
        existingClickersOfSameType
            .OrderBy(x => x.StackedClickers)
            .First()
            .MergeNewClicker();
    }

    private void SpawnClicker(string type)
    {
        var clickGenerator = Instantiate(AutoClickerTemplate).GetComponent<ClickGenerator>();
        clickGenerator.Initialize(this, Clickers[type].CloneWithRandom());
        clickGenerator.transform.position = new Vector3(0, GroundLevel);
        ActiveAutoclickers.Add(clickGenerator);
    }

    private void DestroyClicker(ClickGenerator existingToMerge)
    {
        ActiveAutoclickers.Remove(existingToMerge);
        Destroy(existingToMerge.gameObject);
    }

    public void GroundDestroyed(Clickable clickable)
    {
        GroundBlocks.Remove(clickable);

        _groundsDestroyed++;
        if (_groundsDestroyed % 3 == 0)
        {
            CameraFocusPoint.transform.position = CameraFocusPoint.transform.position.SetY(0) + new Vector3(0, GroundLevel) - FocusPointOffset;
        }

        GenerateGround();
        ActiveAutoclickers.ForEach(x => x.GroundRemoved(GroundLevel));
    }

    public void MineCurrentGround(int amountMined)
    {
        CurrentMoney += amountMined;
        CurrentGround.RemoveHp(amountMined);
        SetCurrency();
    }

    public void RemoveCurrency(int amount)
    {
        CurrentMoney -= amount;
        SetCurrency();
    }

    private void SetCurrency()
    {
        Text.text = "$" + CurrentMoney;
    }

    private void AddAutoClickerButtonToMenu(ClickerType type)
    {
        var button = Instantiate(BuyAutoClickerButton).GetComponent<Button>();
        button.GetComponent<AutoClickerButton>().Initialize(type);
        button.onClick.AddListener(() => PlayerBuyAutoClicker(type.Name));
        button.transform.SetParent(AutoClickerBuyWindow.transform, false);
    } 
    private void AddUpgradeButtonToMenu(ClickerType type)
    {
        var button = Instantiate(BuyUpgradeButton).GetComponent<Button>();
        button.GetComponent<AutoClickerButton>().Initialize(type);
        button.onClick.AddListener(() => PlayerBuyUpgrade(type.Name));
        button.transform.SetParent(UpgradesBuyWindow.transform, false);
    }
}
public class ClickerType
{
    public string Name;
    public float MoveTime;
    public float MoveSpeed;
    public float DigTime;
    public int Income;
    public int Cost;
    public bool SillhouetteUnlocked;
    public bool FullyUnlocked;

    private const float MaxRandomOffset = 1 / 5f;

    public ClickerType CloneWithRandom()
    {
        var newWithRandom = new ClickerType
        {
            Name = Name,
            MoveTime = GetWithRandomOffset(MoveTime),
            MoveSpeed = MoveSpeed,
            DigTime = GetWithRandomOffset(DigTime),
            Income = Income,
            Cost = Cost
        };

        return newWithRandom;
    }

    private float GetWithRandomOffset(float currentValue)
    {
        return currentValue + Random.Range(-currentValue * MaxRandomOffset, currentValue * MaxRandomOffset);
    }
}
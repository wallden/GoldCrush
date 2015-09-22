using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    public GameMaster GameMaster;
    public GameObject BackgroundPrefab;
    public Transform FocusPoint;

    public float LevelSwitchRatio = 50;

    private int _spawnedCount;
    private float _backgroundHeight;
    private readonly int _maxVariations = 3;

    public void Start ()
    {
        AddNextLevelBackground();
    }

    public void Update()
    {
        var visibilityHeight = (_spawnedCount - 1)*_backgroundHeight;
        if (Mathf.Abs(FocusPoint.position.y) > visibilityHeight)
        {
            AddNextLevelBackground();
        }
    }

    private void AddNextLevelBackground()
    {
        Sprite backGroundSprite;
        var currentLevel = (int)Mathf.Abs(GameMaster.Depth) / (int)LevelSwitchRatio;
        if (currentLevel == 0)
        {
            backGroundSprite = Resources.Load<Sprite>("Backgrounds/Level0");

        }
        else
        {
            backGroundSprite = Resources.Load<Sprite>(string.Format("Backgrounds/Level{0}_{1}", currentLevel + 1, Random.Range(1, _maxVariations + 1)));

        }
        _backgroundHeight = backGroundSprite.bounds.size.y;

        var position = new Vector3(0, (-_spawnedCount * _backgroundHeight)+1.4f);
        var background = (GameObject)Instantiate(BackgroundPrefab, position, new Quaternion());
        background.GetComponent<SpriteRenderer>().sprite = backGroundSprite;
        background.transform.SetParent(transform);

        _spawnedCount += 1;
    }
}
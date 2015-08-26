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
        var currentLevel = (int)Mathf.Abs(GameMaster.Depth) / (int)LevelSwitchRatio;
        var sprite = Resources.Load<Sprite>(string.Format("Backgrounds/Level{0}_{1}", currentLevel + 1, Random.Range(1, _maxVariations+1)));
        _backgroundHeight = sprite.bounds.size.y;

        var position = new Vector3(0, -_spawnedCount * _backgroundHeight);
        var background = (GameObject)Instantiate(BackgroundPrefab, position, new Quaternion());
        background.GetComponent<SpriteRenderer>().sprite = sprite;
        background.transform.SetParent(transform);

        _spawnedCount += 1;
    }
}
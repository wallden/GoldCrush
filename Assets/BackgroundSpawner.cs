using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    public GameObject BackgroundPrefab;
    public Transform FocusPoint;

    private int _levelCount;
    private float _levelHeight;

    public void Start ()
    {
        AddNextLevelBackground();
    }

    public void Update()
    {
        var visibilityHeight = (_levelCount - 1)*_levelHeight;
        if (Mathf.Abs(FocusPoint.position.y) > visibilityHeight)
        {
            AddNextLevelBackground();
        }
    }

    private void AddNextLevelBackground()
    {
        var sprite = Resources.Load<Sprite>("Backgrounds/Level" + (_levelCount + 1));
        _levelHeight = sprite.bounds.size.y;

        var position = new Vector3(0, -_levelCount * _levelHeight);
        var background = (GameObject)Instantiate(BackgroundPrefab, position, new Quaternion());
        background.GetComponent<SpriteRenderer>().sprite = sprite;
        background.transform.SetParent(transform);

        _levelCount += 1;
    }
}
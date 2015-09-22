using UnityEngine;

public static class TextEffectFactory
{
    public static void CreateFlyUpAndFade(string text, Vector3 position)
    {
        var gameObject = Resources.Load<GameObject>("TextFlyUpAndFadeEffect");
        var incomeMineEffect = (GameObject)Object.Instantiate(gameObject, position, Quaternion.identity);
        incomeMineEffect.GetComponentInChildren<TextMesh>().text = text;
    }
}
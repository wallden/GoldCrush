using UnityEngine;
using System.Collections;

public class FadeInAndOut : MonoBehaviour
{
    public AnimationCurve TransparencyCurve;
    public float CurveTime = 1;

    private TextMesh _textMesh;
    private float _elapsedTime;

    void Start ()
    {
        _textMesh = GetComponent<TextMesh>();
    }

    void Update ()
    {
        var transparency = TransparencyCurve.Evaluate(_elapsedTime/CurveTime);
        var oldColor = _textMesh.color;
        _textMesh.color = new Color(oldColor.r, oldColor.g, oldColor.b, transparency);
        _elapsedTime += Time.deltaTime;
    }
}

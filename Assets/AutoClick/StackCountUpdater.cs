using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StackCountUpdater : MonoBehaviour
{
    public TextMesh Text;

    private ClickGenerator _clickGenerator;

    public void Start()
    {
        _clickGenerator = GetComponent<ClickGenerator>();
    }

    public void Update ()
    {
        Text.text = "x" + _clickGenerator.StackedClickers;
    }
}
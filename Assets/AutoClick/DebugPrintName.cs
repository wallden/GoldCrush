using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugPrintName : MonoBehaviour
{
    public TextMesh Text;

    private ClickGenerator _clickGenerator;

    public void Start()
    {
        _clickGenerator = GetComponent<ClickGenerator>();
    }

    public void Update()
    {
        Text.text = _clickGenerator.ClickerType.Name;
    }
}
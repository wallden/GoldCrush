using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngineInternal;

public class PanelWindow : MonoBehaviour
{
    public RectTransform AutoClickersRectTransform;
    public RectTransform UpgradesRectTransform;
    public RectTransform InGameStoreRectTransform;
    private bool _isOpen;

    void Start()
    {
        InGameStoreRectTransform.gameObject.SetActive(false);
    }
    public void ToggleWindow(string type)
    {
        switch (type)
        {
            case "AutoClickers":
                AutoClickersRectTransform.gameObject.SetActive(true);
                
                UpgradesRectTransform.gameObject.SetActive(false);
                break;
            case "Upgrades":
                UpgradesRectTransform.gameObject.SetActive(true);

                AutoClickersRectTransform.gameObject.SetActive(false);
                break;
            case "Store":
                InGameStoreRectTransform.gameObject.SetActive(!InGameStoreRectTransform.gameObject.activeSelf);
                return;
        }
        OpenWindow();
    }

    public void OpenWindow()
    {
        _isOpen = true;
        GetComponent<Animator>().SetBool("IsOpen", _isOpen);
    }

    public void CloseWindow()
    {
        _isOpen = false;
        GetComponent<Animator>().SetBool("IsOpen", _isOpen);
        InGameStoreRectTransform.gameObject.SetActive(false);
    }
}

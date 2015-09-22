using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine.UI;
using UnityEngineInternal;

public class PanelWindow : MonoBehaviour
{
    public RectTransform AutoClickersRectTransform;
    public RectTransform UpgradesRectTransform;
    public RectTransform InGameStoreRectTransform;
    public Button SideMenuButton;
    private Animator Animator;
    private bool _isOpen;

    void Start()
    {
        InGameStoreRectTransform.gameObject.SetActive(false);
        Animator = GetComponent<Animator>();
    }
    public void ToggleWindow(string type)
    {
        switch (type)
        {
            case "AutoClickers":
                AutoClickersRectTransform.gameObject.SetActive(true);
                
                UpgradesRectTransform.gameObject.SetActive(false);
                break;
            case "SideMenu":
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
       Animator.SetBool("IsOpen", _isOpen);
       SideMenuButton.gameObject.SetActive(false);
    }

    public void CloseWindow()
    {
        _isOpen = false;
        Animator.SetBool("IsOpen", _isOpen);
        SideMenuButton.gameObject.SetActive(true);
    }
}

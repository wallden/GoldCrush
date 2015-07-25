﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AutoClickerButton : MonoBehaviour
{

    private ClickerType _autoClickerType;
	
    void Update () {
	    if (_autoClickerType.Cost <= GameMaster.CurrentMoney && !_autoClickerType.FullyUnlocked)
	    {
	        UnlockThis();
	        _autoClickerType.FullyUnlocked = true;
	    }
	}

    private void UnlockThis()
    {
        gameObject.GetComponent<Image>().sprite = ButtonImage.GetButtonImage(_autoClickerType.Name);
    }

    public void Initialize(ClickerType type)
    {
        _autoClickerType = type;
        SetSillhouette();
        SetButtonTextToCost(type);
    }

    private void SetButtonTextToCost(ClickerType type)
    {
        gameObject.GetComponent<Button>().GetComponentInChildren<Text>().text = "$" + type.Cost;
    }

    private void SetSillhouette()
    {
        gameObject.GetComponent<Image>().sprite = ButtonImage.GetButtonImage(_autoClickerType.Name + "Sillhouette");
    }
}

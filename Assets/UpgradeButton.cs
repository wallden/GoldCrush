using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{

    private UpgradeType _autoClickerType;
	
    void Update () {
	    if (_autoClickerType.UnlocksAtPlayerLevel >= Player.PlayerLevel && !_autoClickerType.FullyUnlocked)
	    {
	        UnlockThis();
	        _autoClickerType.FullyUnlocked = true;
	    }
	}

    private void UnlockThis()
    {
        gameObject.GetComponent<Image>().sprite = ButtonImage.GetButtonImage(_autoClickerType.Name);
        GetComponent<Button>().interactable = true;

    }

    public void Initialize(UpgradeType type)
    {
        _autoClickerType = type;
        SetSillhouette();
        SetButtonTextToCost(type);
        GetComponent<Button>().interactable = false;
    }

    private void SetButtonTextToCost(UpgradeType type)
    {
        gameObject.GetComponent<Button>().GetComponentInChildren<Text>().text = "$" + type.Cost;
    }

    private void SetSillhouette()
    {
        gameObject.GetComponent<Image>().sprite = ButtonImage.GetButtonImage(_autoClickerType.Name + "Sillhouette");
    }
}

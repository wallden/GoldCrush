using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    private ClickerType _upgradeType;

    void Update()
    {
        if (_upgradeType.Cost <= GameMaster.CurrentMoney && !_upgradeType.FullyUnlocked)
        {
            UnlockThis();
            _upgradeType.FullyUnlocked = true;
        }
    }

    private void UnlockThis()
    {
        gameObject.GetComponent<Image>().sprite = ButtonImage.GetButtonImage(_upgradeType.Name);
    }

    public void Initialize(ClickerType type)
    {
        _upgradeType = type;
        SetSillhouette();
        SetButtonTextToCost(type);
    }

    private void SetButtonTextToCost(ClickerType type)
    {
        gameObject.GetComponent<Button>().GetComponentInChildren<Text>().text = "$" + type.Cost;
    }

    private void SetSillhouette()
    {
        gameObject.GetComponent<Image>().sprite = ButtonImage.GetButtonImage(_upgradeType.Name + "Sillhouette");
    }
}

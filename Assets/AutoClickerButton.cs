using UnityEngine;
using System.Collections;

public class AutoClickerButton : MonoBehaviour
{

    private ClickerType _autoClickerType;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (GameMaster.CurrentMoney <= _autoClickerType.Cost)
	    {
	        UnlockThis();
	    }
	}

    private void UnlockThis()
    {
        
    }

    public void Initialize(ClickerType type)
    {
        _autoClickerType = type;
    }
}

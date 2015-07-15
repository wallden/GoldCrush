using UnityEngine;
using System.Collections;

public class ClickGenerator : MonoBehaviour
{
    private float _elapsedTime;
    private GameMaster _gameMaster;
    private Mineral _mineral;

    void Start ()
	{
	
	}

	void Update ()
	{
	    if (_elapsedTime > _mineral.Cooldown)
	    {
	        _gameMaster.AddCurrency(_mineral.Name);
	        _elapsedTime = 0;
	    }

	    _elapsedTime += Time.deltaTime;
	}

    public void Initialize(GameMaster gameMaster, Mineral mineral)
    {
        _mineral = mineral;
        _gameMaster = gameMaster;
    }
}
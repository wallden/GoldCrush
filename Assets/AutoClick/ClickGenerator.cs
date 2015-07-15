using UnityEngine;
using System.Collections;

public class ClickGenerator : MonoBehaviour
{
    public Animator Animator;

    private float _elapsedTime;
    private GameMaster _gameMaster;
    private ClickerType _clickerType;
    private Vector3 _moveDirection;

    void Start ()
	{
	
	}

	void Update ()
	{
	    if (_elapsedTime > _clickerType.Cooldown)
	    {
	        AddIncome();
	        SetNewTarget();
            Animator.SetBool("IsMining", true);
	    }

	    MoveToTarget();
	    _elapsedTime += Time.deltaTime;
	}

    private void AddIncome()
    {
        _gameMaster.AddCurrency(_clickerType.Income);
        _elapsedTime = 0;
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _moveDirection, Time.deltaTime);
    }

    private void SetNewTarget()
    {
        _moveDirection = new Vector3(Random.Range(-1f, 1f), 0, 0);
    }

    public void Initialize(GameMaster gameMaster, ClickerType clickerType)
    {
        _clickerType = clickerType;
        _gameMaster = gameMaster;
    }
}
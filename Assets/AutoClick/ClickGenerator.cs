using UnityEngine;
using System.Collections;

public class ClickGenerator : MonoBehaviour
{
    public Animator Animator;

    private float _elapsedTime;
    private GameMaster _gameMaster;
    private ClickerType _clickerType;
    private Vector3 _moveDirection;
    private CharacterState _characterState;

    enum CharacterState
    {
        Walking,
        Mining
    }

    void Start ()
	{
	    
	}

	void Update ()
	{
	    if (_characterState == CharacterState.Walking)
	    {
            MoveToTarget();

            if (_elapsedTime > _clickerType.Cooldown)
            {
                SetCharacterState(CharacterState.Mining);
            }
        }

        if (_characterState == CharacterState.Mining)
	    {
            if (_elapsedTime > _clickerType.DigTime)
            {
                SetCharacterState(CharacterState.Walking);
                AddIncome();
                SetNewMoveDirection();
            }
        }

	    _elapsedTime += Time.deltaTime;
	}

    private void SetCharacterState(CharacterState newState)
    {
        _characterState = newState;
        Animator.SetBool("IsMining", _characterState == CharacterState.Mining);
        _elapsedTime = 0;
    }

    private void AddIncome()
    {
        _gameMaster.AddCurrency(_clickerType.Income);
        _elapsedTime = 0;
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _moveDirection, Time.deltaTime);
        var absoluteXScale = Mathf.Abs(transform.localScale.x);
        transform.localScale = transform.localScale.SetX(_moveDirection.x >= 0 ? absoluteXScale : -absoluteXScale);
    }

    private void SetNewMoveDirection()
    {
        _moveDirection = new Vector3(Random.Range(-1f, 1f), 0, 0);
    }

    public void Initialize(GameMaster gameMaster, ClickerType clickerType)
    {
        _clickerType = clickerType;
        _gameMaster = gameMaster;
    }
}
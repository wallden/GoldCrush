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
    private float _distanceToWalkEdges;
    private float _groundLevel;

    enum CharacterState
    {
        Walking,
        Mining,
        Falling
    }

    public void Initialize(GameMaster gameMaster, ClickerType clickerType, float distanceToWalkEdges)
    {
        _clickerType = clickerType;
        _gameMaster = gameMaster;
        _distanceToWalkEdges = distanceToWalkEdges;
        SetNewMoveDirection();
    }

    void Update ()
	{
	    if (_characterState == CharacterState.Walking)
	    {
            MoveToTarget();

            if (_elapsedTime > _clickerType.MoveTime)
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

        if (_characterState == CharacterState.Falling)
        {
            Fall();

            if (transform.position.y <= _groundLevel)
            {
                EndFall();
            }
        }

	    _elapsedTime += Time.deltaTime;
	}

    private void Fall()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, _groundLevel), Time.deltaTime);
    }

    private void EndFall()
    {
        Animator.SetBool("IsFalling", false);
        _characterState = CharacterState.Walking;
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
        _moveDirection = new Vector3(Random.Range(-1f, 1f), 0, 0).normalized;

        if (Mathf.Abs(_moveDirection.x*_clickerType.MoveTime + transform.position.x) > _distanceToWalkEdges)
        {
            _moveDirection.x = -_moveDirection.x;
        }
    }

    public void GroundRemoved(float newGroundLevel)
    {
        _groundLevel = newGroundLevel;
        _characterState = CharacterState.Falling;
        Animator.SetBool("IsFalling", true);
        //transform.position = transform.position.SetY(newGroundLevel);
    }
}
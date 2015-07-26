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

    private ParticleSystem _particleSystem;

    private const float FallSpeed = 1.5f;

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
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void DoDig()
    {
        _particleSystem.Play();
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
                FinishMining();
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
        transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, _groundLevel), Time.deltaTime*FallSpeed);
    }

    private void EndFall()
    {
        Animator.SetBool("IsFalling", false);
        SetCharacterState(CharacterState.Walking);
        SetNewMoveDirection();
    }

    private void SetCharacterState(CharacterState newState)
    {
        _characterState = newState;
        Animator.SetBool("IsMining", _characterState == CharacterState.Mining);
        Animator.SetBool("IsFalling", _characterState == CharacterState.Falling);
        _elapsedTime = 0;
    }

    private void FinishMining()
    {
        _gameMaster.MineCurrentGround(_clickerType.Income);
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _moveDirection, Time.deltaTime*_clickerType.MoveSpeed);
        var absoluteXScale = Mathf.Abs(transform.localScale.x);
        transform.localScale = transform.localScale.SetX(_moveDirection.x >= 0 ? absoluteXScale : -absoluteXScale);
    }

    private void SetNewMoveDirection()
    {
        _moveDirection = new Vector3(Random.Range(-1f, 1f), 0, 0).normalized;

        if (Mathf.Abs(_moveDirection.x*_clickerType.MoveTime*_clickerType.MoveSpeed + transform.position.x) > _distanceToWalkEdges)
        {
            _moveDirection.x = -_moveDirection.x;
        }
    }

    public void GroundRemoved(float newGroundLevel)
    {
        _groundLevel = newGroundLevel;
        SetCharacterState(CharacterState.Falling);
    }
}
using UnityEngine;
using System.Collections;

public class ClickGenerator : MonoBehaviour
{
    public Animator Animator;
    public Transform View;
    public ClickerType ClickerType { get; private set; }
    public int StackedClickers { get; private set; }

    private float _elapsedTime;
    private GameMaster _gameMaster;
    private Vector3 _moveDirection;
    private CharacterState _characterState;
    private float _groundLevel;
    private Vector3 _screenBounds;

    private ParticleSystem _particleSystem;

    private const float FallSpeed = 1.5f;
    private const float ScreenPadding = 0.9f;

    enum CharacterState
    {
        Walking,
        Mining,
        Falling
    }

    public void Initialize(GameMaster gameMaster, ClickerType clickerType)
    {
        ClickerType = clickerType;
        StackedClickers = 1;
        _gameMaster = gameMaster;
        SetNewMoveDirection();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void DoDig()
    {
        _particleSystem.Play();
    }

    void Update ()
	{
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height))*ScreenPadding;

        if (_characterState == CharacterState.Walking)
	    {
            MoveToTarget();

            if (_elapsedTime > ClickerType.MoveTime)
            {
                SetCharacterState(CharacterState.Mining);
            }
        }

        if (_characterState == CharacterState.Mining)
	    {
            if (_elapsedTime > ClickerType.DigTime)
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
        _gameMaster.MineCurrentGround(ClickerType.Income*StackedClickers);
    }

    private void MoveToTarget()
    {
        FlipMoveDirectionIfAtScreenEdge();

        transform.position = Vector3.MoveTowards(transform.position, transform.position + _moveDirection, Time.deltaTime*ClickerType.MoveSpeed);
        var absoluteXScale = Mathf.Abs(View.localScale.x);
        View.localScale = View.localScale.SetX(_moveDirection.x >= 0 ? absoluteXScale : -absoluteXScale);
    }

    private void FlipMoveDirectionIfAtScreenEdge()
    {
        if (Mathf.Abs(transform.position.x) > _screenBounds.x)
        {
            _moveDirection.x = transform.position.x < -_screenBounds.x ? 1 : -1;
        }
    }

    private void SetNewMoveDirection()
    {
        _moveDirection = new Vector3(Random.Range(-1f, 1f), 0, 0).normalized;
    }

    public void GroundRemoved(float newGroundLevel)
    {
        _groundLevel = newGroundLevel;
        SetCharacterState(CharacterState.Falling);
    }

    public void MergeNewClicker()
    {
        StackedClickers += 1;
    }
}
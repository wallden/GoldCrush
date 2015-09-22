using UnityEngine;

public class ClickGenerator : MonoBehaviour
{
    public Animator Animator;
    public Transform View;
    public ClickerType ClickerType { get; private set; }
    public int StackedClickers;
    public GameObject FinishedMiningEffect;

    private float _elapsedTime;
    private GameMaster _gameMaster;
    private Vector3 _moveDirection;
    private CharacterState _characterState;
    private CharacterState _previousState;
    private float _groundLevel;
    private Vector3 _screenBounds;

    private ParticleSystem _particleSystem;
    private ClickerMerge _clickerMerge;

    private const float FallSpeed = 1.5f;
    private const float ScreenPadding = 0.9f;

    enum CharacterState
    {
        Walking,
        Mining,
        Falling,
        Merging
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
            MoveToTarget(transform.position + _moveDirection);

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

        if (_characterState == CharacterState.Merging)
        {
            MoveToTarget(_clickerMerge.CenterPoint);
            if (transform.position == _clickerMerge.CenterPoint)
            {
                _gameMaster.FinishMerge(_clickerMerge);
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
        SetCharacterState(_previousState);
        SetNewMoveDirection();
    }

    private void SetCharacterState(CharacterState newState)
    {
        _previousState = _characterState;
        _characterState = newState;
        Animator.SetBool("IsMining", _characterState == CharacterState.Mining);
        Animator.SetBool("IsFalling", _characterState == CharacterState.Falling);
        _elapsedTime = 0;
    }

    private void FinishMining()
    {
        var amountMined = ClickerType.Income*StackedClickers;
        var incomeMineEffect = (GameObject)Instantiate(FinishedMiningEffect, transform.position + Vector3.up*0.5f, Quaternion.identity);
        incomeMineEffect.GetComponentInChildren<TextMesh>().text = "+" + amountMined + "$";
        _gameMaster.MineCurrentGround(amountMined);
    }

    private void MoveToTarget(Vector3 target)
    {
        FlipMoveDirectionIfAtScreenEdge();

        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime*ClickerType.MoveSpeed*0.2f);
        var absoluteXScale = Mathf.Abs(View.localScale.x);

        var moveDirection = target - transform.position;
        View.localScale = View.localScale.SetX(moveDirection.x >= 0 ? absoluteXScale : -absoluteXScale);
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

    public void MergeExistingClicker(ClickGenerator existingClicker)
    {
        StackedClickers += existingClicker.StackedClickers;
    }

    public void Merge(ClickerMerge clickerMerge)
    {
        _clickerMerge = clickerMerge;
        SetCharacterState(CharacterState.Merging);
    }
}
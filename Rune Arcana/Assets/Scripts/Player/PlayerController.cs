using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Coroutine _AttckRoutine = null;
    private Coroutine _StepRoutine = null;
    private Coroutine _hitRoutine = null;
    
    [Header("Get Components")]
    [SerializeField] private Player_Actions _playerActions;
    [SerializeField] public Animator _animator;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _playerSprite;
    [SerializeField] private GameObject _fireballPrefab;
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private PlayerSoundController _soundController;

    public StateMachine _stateMachine;
    public IdleState Idle { get; private set; }
    public MoveState Move { get; private set; }
    public AttackState Attack { get; private set; }
    public HitState Hit { get; private set; }
    public DeadState Dead { get; private set; }

    [SerializeField] Vector2 _mousePosition;
    public Vector2 _direction;

    [Space(20)]
    [Header("Player Stats")]
    [SerializeField] private PlayerStat _playerStat;
    [SerializeField] private float _hp = 1;

    public float HP
    {
        get { return _hp; }
        set 
        {
            _hp = value;  
            DebugUtil.DebugingColor($"플레이어 체력 : {HP}", "D9FF2F");
        } 
    }
    
    [SerializeField] private float _Damage;
    public float Damage { get => _Damage; set => _Damage = value; }
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _stepInterval;
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    
    public Vector2 MoveInput { get; private set; }
    public bool CanMove { get; set; } = true;
    public bool IsMove { get; set; }
    public bool AttackInput { get; set; }
    public bool CanHit { get; set; } = true;
    public bool isHit { get; set; }
    public bool isDead { get; set; }
    
    public bool InteractInput { get; private set; }

    private void Awake()
    {
        Init();
        StatInit();
    }

    private void Start()
    {
        _stateMachine.ChangeState(Idle);
    }

    private void OnEnable()
    {
        _playerActions.Enable();

        _playerActions.Player.Move.performed += OnMove;
        _playerActions.Player.Move.canceled += OnMove;
        _playerActions.Player.Attack.started += OnAttack;
        _playerActions.Player.Interact.started += OnInteract;
        _playerActions.Player.Interact.canceled += OnInteract;
        _playerActions.Player.Mouse.performed += OnMouse;
    }

    private void OnDisable()
    {
        _playerActions.Player.Move.performed -= OnMove;
        _playerActions.Player.Move.canceled -= OnMove;
        _playerActions.Player.Attack.started -= OnAttack;
        _playerActions.Player.Interact.started -= OnInteract;
        _playerActions.Player.Interact.canceled -= OnInteract;
        _playerActions.Player.Mouse.performed -= OnMouse;
        
        _playerActions.Disable();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        _stateMachine.Update();
        DebugUtil.DebugingColor($"{_stateMachine._currentState}", "D9FF2F");
        CheckHP();  
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            AttackInput = true;
            CanMove = false;
            if(_AttckRoutine == null)
                _AttckRoutine = StartCoroutine(FireBall());
        }
    }
    
    private void CheckHP()
    {
        if (HP <= 0)
        {
            DebugUtil.DebugingColor("Player Died!", "AA3A26");
            isHit = true;
            CanMove = false;
            _rb.linearVelocity = Vector2.zero;
        }
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            InteractInput = true;
        }

        if (ctx.canceled)
        {
            InteractInput = false;
        }
    }

    public void OnMouse(InputAction.CallbackContext ctx)
    {
        _mousePosition = ctx.ReadValue<Vector2>();
        
        _mousePosition = new Vector2(_mousePosition.x - Screen.width/2f,
            _mousePosition.y - Screen.height/2f);
        
        _mousePosition.Normalize();
        _direction = _mousePosition;
        PointDirection();
    }

    public void Movement()
    {
        if(AttackInput)
        {
            _rb.linearVelocity = Vector2.zero;
            IsMove = false;
            return;
        }
        
        if (CanMove)
        {
            IsMove = true;
            
            _rb.linearVelocity = MoveInput * MoveSpeed;
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
        }
    }
   
    private void PointDirection()
    {
        if (_mousePosition.x < 0)
            _playerSpriteRenderer.flipX = true;
        if (_mousePosition.x > 0)
            _playerSpriteRenderer.flipX = false;
    }

    public void StepSound()
    {
        if(_StepRoutine == null)
            _StepRoutine = StartCoroutine(FootStep());
    }

    public void PlayerDamage(float damage)
    {
        if(CanHit)
        {
            _soundController.PlayerHitSound();
            HP -= damage;
            isHit = true;
            CanHit = false;
            if(_hitRoutine == null)
                _hitRoutine = StartCoroutine(HitDelay());
        }
        else
        {
            Debug.Log("무적 시간");
        }
    }

    private IEnumerator HitDelay()
    {
        CanMove = false;
        _soundController.PlayerHitSound();
        yield return YieldContainer.Wait(0.18f);
        isHit = false;
        CanMove = true;
        yield return YieldContainer.Wait(0.18f);
        CanHit = true;
        IsMove = false;
        _hitRoutine = null;
    }
    
    private IEnumerator FootStep()
    {
        _soundController.FootStepSound();
        yield return YieldContainer.Wait(0.5f);
        _StepRoutine = null;
    }
    public void ChangeState(IState state)
    {
        _stateMachine.ChangeState(state);
    }
    
    public IEnumerator FireBall()
    {
        yield return YieldContainer.Wait(0.28f);
        
        Vector2 ballDir = new Vector2(transform.position.x + _direction.x / 10f,
            transform.position.y + _direction.y / 10f);
        Instantiate(_fireballPrefab, ballDir, Quaternion.identity);
        _soundController.FireballSound();
        yield return YieldContainer.Wait(0.28f);
        
        AttackInput = false;
        _AttckRoutine = null;
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody2D>();
        _soundController = GetComponent<PlayerSoundController>();
        
        _stateMachine = new StateMachine();
        _playerActions = new Player_Actions();
        
        Idle = new IdleState(this);
        Move = new MoveState(this);
        Attack = new AttackState(this);
        Hit = new HitState(this);
        Dead = new DeadState(this);
    }

    private void StatInit()
    {
        HP = _playerStat.HP;
        Damage = _playerStat.Damage;
        MoveSpeed = _playerStat.MoveSpeed;
    }
}
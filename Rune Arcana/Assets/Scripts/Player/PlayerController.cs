using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Coroutine _AttckRoutine = null;
    private Coroutine _StepRoutine = null;
    
    [Header("Get Components")]
    [SerializeField] private Player_Actions _playerActions;
    [SerializeField] public Animator _animator;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _playerSprite;
    [SerializeField] private GameObject _fireballPrefab;
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
    [SerializeField] private float _hp;
    public float HP { get => _hp; set => _hp = value; }
    [SerializeField] private float _Damage;
    public float Damage { get => _Damage; set => _Damage = value; }
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _stepInterval;
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    
    
    
    public Vector2 MoveInput { get; private set; }
    public bool CanMove { get; set; } = true;
    public bool IsMove { get; set; }
    public bool AttackInput { get; set; }
    public bool isHit { get; private set; }
    public bool CanHit { get;private set; }
    public bool isDead { get; private set; }
    
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
        if(AttackInput)
            _rb.linearVelocity = Vector2.zero;
    }

    private void Update()
    {
        _stateMachine.Update();
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
        if (CanMove)
        {
            IsMove = true;
            if(_StepRoutine == null)
                _StepRoutine = StartCoroutine(FootStep());
            _rb.linearVelocity = MoveInput * MoveSpeed;
        }
        else
            StopCoroutine(FootStep());
    }
   
    private void PointDirection()
    {
        if (_mousePosition.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        if (_mousePosition.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }
    
    public IEnumerator FireBall()
    {
        yield return YieldContainer.Wait(0.28f);
        
        Vector2 ballDir = new Vector2(transform.position.x + _direction.x / 10f,
            transform.position.y + _direction.y / 10f);
        Instantiate(_fireballPrefab, ballDir, Quaternion.identity);
        
        yield return YieldContainer.Wait(0.28f);
        
        AttackInput = false;
        _AttckRoutine = null;
    }
    
    private IEnumerator FootStep()
    {
        _soundController.FootStep();
        yield return YieldContainer.Wait(_stepInterval);
        _StepRoutine = null;
    }
    public void ChangeState(IState state)
    {
        _stateMachine.ChangeState(state);
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
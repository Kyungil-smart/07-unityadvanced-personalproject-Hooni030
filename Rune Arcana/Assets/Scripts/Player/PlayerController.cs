using System;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.VirtualTexturing;

public class PlayerController : MonoBehaviour
{
    [Header("Get Components")]
    [SerializeField] private Player_Actions _playerActions;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private GameObject _playerSprite;

    private StateMachine _stateMachine;
    public IdleState Idle { get; private set; }
    public MoveState Move { get; private set; }
    public AttackState Attack { get; private set; }
    public SkillState Skill { get; private set; }
    public HitState Hit { get; private set; }
    public TeleportState Teleport { get; private set; }
    public DeadState Dead { get; private set; }

    [Space(20)]
    [Header("Player Stats")]
    [SerializeField] private PlayerStat _playerStat;
    [SerializeField] private float _hp;
    public float HP { get => _hp; set => _hp = value; }
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    [SerializeField] private float _avoidDistance;
    public float AvoidDistance { get => _avoidDistance; set => _avoidDistance = value; }
    [SerializeField] private float _gold;
    public float Gold { get => _gold; set => _gold = value; }
    
    public Vector2 MoveInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool SkillInput { get; private set; }
    public bool isHit { get; private set; }
    public bool TeleInput { get; private set; }
    public bool isDead { get; private set; }

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
        _playerActions.Player.Skill.started += OnSkill;
        _playerActions.Player.Teleport.started += OnTeleport;
    }

    private void OnDisable()
    {
        _playerActions.Disable();
        _playerActions.Player.Move.performed -= OnMove;
        _playerActions.Player.Move.canceled -= OnMove;
        _playerActions.Player.Attack.started -= OnAttack;
        _playerActions.Player.Skill.started -= OnSkill;
        _playerActions.Player.Teleport.started -= OnTeleport;

        _playerActions.Disable();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
        PointDirection();
        // DebugUtil.DebugingColor($"{_rb.linearVelocity}, 방향 : {MoveInput}, 속도 : {MoveSpeed}", "F31C41");
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            DebugUtil.DebugingColor("Attack Activated", "f5ee5b");
        }
    }

    public void OnSkill(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            DebugUtil.DebugingColor("Skill Activated", "f5ee5b");
        }
    }

    public void OnTeleport(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            DebugUtil.DebugingColor("Avoid Activated", "f5ee5b");
        }
    }

    private void Movement()
    {
        _rb.linearVelocity = MoveInput * MoveSpeed;
    }

    private void Init()
    {
        _animator = _playerSprite.GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        
        _stateMachine = new StateMachine();
        _playerActions = new Player_Actions();
        
        Idle = new IdleState(this);
        Move = new MoveState(this);
        Attack = new AttackState(this);
        Hit = new HitState(this);
        Dead = new DeadState(this);
        Skill = new SkillState(this);
    }

    private void StatInit()
    {
        HP = _playerStat.HP;
        MoveSpeed = _playerStat.MoveSpeed;
        AvoidDistance = _playerStat.AvoidDistance;
        Gold = _playerStat.Gold;
    }

    public void ChangeState(IState state)
    {
        _stateMachine.ChangeState(state);
    }

    private void PointDirection()
    {
        if (MoveInput.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        if (MoveInput.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControlller : MonoBehaviour
{
    private Coroutine _StepRoutine;
    private Coroutine _AttackRoutine;
    private Coroutine _HurtRoutine;
    
    [Header("Get Components")]
    [SerializeField] private GameObject _player;
    
    [SerializeField] private Player_Actions _playerActions;
    [SerializeField] public Animator _animator;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private CapsuleCollider2D _collider;
    [SerializeField] private CircleCollider2D _attackCol;
    [SerializeField] private MonsterSoundController _monsterSound;
    
    public StateMachine _stateMachine;
    public Mon_IdleState Idle { get; private set; }
    public Mon_MoveState Move { get; private set; }
    public Mon_AttackState Attack { get; private set; }
    public Mon_HurtState Hurt { get; private set; }
    public Mon_DeadState Dead { get; private set; }
    
    private Vector2 _direction;
    private Vector2 _distance;
    
    private LayerMask _playerLayer;
    private RaycastHit2D _hit;
    
    [Space(20)]
    [Header("Monster Stats")]
    [SerializeField] private MonsterStat _monster;
    [SerializeField] private string _name;
    public string Name { get => _name; set { _name = value; } }
    [SerializeField] private float _hp = 1;
    public float HP { get => _hp; set { _hp = value; } }
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed { get => _moveSpeed; set { _moveSpeed = value; } }
    [SerializeField] private float _damage;
    public float Damage { get => _damage; set { _damage = value; } }
    [SerializeField] private float _range;
    public float Range { get => _range; set { _range = value; } }
    [SerializeField] private float _detectRange;
    public float DetectRange { get => _detectRange; set { _detectRange = value; } }
    
    

    public bool canMove = true;
    public bool isMove;
    public bool attack;
    public bool isHurt;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        ChangeState(Idle);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        _stateMachine.Update();
        CheckHp();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 0.5f, 0f, 0.3f);
        Gizmos.DrawSphere(transform.position, Range);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, Range);
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _direction * Range);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack") && !isHurt && _HurtRoutine == null)
        {
            isHurt = true;
            _HurtRoutine = StartCoroutine(HurtDelay());
            _monsterSound.SkeletonHit();
            FireballController fireball = other.GetComponent<FireballController>();
            HP -= fireball.Damage;
            DebugUtil.DebugingColor($"스켈레톤 체력 : {HP}", "ff007f");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canMove = false;
            Movement();
            if (_AttackRoutine == null)
            {
                attack = true;
                _AttackRoutine = StartCoroutine(AttackPlayer());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(!attack)
                canMove = true;
        }
    }

    private void CheckHp()
    {
        if (HP <= 0)
        {
            _collider.enabled = false;
            canMove = false;
            _rb.linearVelocity = Vector2.zero;
            Destroy(gameObject, 1.1f);
        }
    }

    private void Movement()
    {
        GetDirection();

        if (canMove && _distance.magnitude <= DetectRange)
        {
            isMove = true;
            FlipSprite();
            _rb.linearVelocity = _direction * MoveSpeed;
        }
        else
        {
            isMove = false;
            _rb.linearVelocity = Vector2.zero;
        }
    }

    private void GetDirection()
    {
        _direction = _player.transform.position - transform.position;
        _distance = _direction;
        _direction.Normalize();
    }

    private void FlipSprite()
    {
        if (_direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (_direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }
    public void StepSound()
    {
        if(_StepRoutine == null)
            _StepRoutine = StartCoroutine(FootStep());
    }

    private void AttackRay()
    {
        _hit = Physics2D.Raycast(transform.position, _direction, Range * 1.1f, _playerLayer);
        _monsterSound.SkeletonSmash();
        if (_hit.collider == null)
        {
            Debug.Log("Player not found");
            return;
        }
        
        PlayerController playerController = _hit.collider.GetComponent<PlayerController>();
        
        if (playerController != null)
        {
            playerController.PlayerDamage(Damage);
        }
        else
            Debug.Log("Player component not found");
    }


    private IEnumerator AttackPlayer()
    {
        // 몬스터 멈춤
        yield return YieldContainer.Wait(0.3f);
        AttackRay();
        yield return YieldContainer.Wait(1.2f);
        attack = false;
        canMove = true;
        yield return YieldContainer.Wait(1f);
        _AttackRoutine = null;
    }

    private IEnumerator HurtDelay()
    {
        canMove = false;
        _rb.linearVelocity = Vector2.zero;
        yield return YieldContainer.Wait(0.5f);
        isHurt = false;
        _HurtRoutine = null;
    }

    private IEnumerator FootStep()
    {
        _monsterSound.SkeletonStep();
        yield return YieldContainer.Wait(1f);
        _StepRoutine = null;
    }
    
    public void ChangeState(IState state)
    {
        _stateMachine.ChangeState(state);
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _collider = GetComponent<CapsuleCollider2D>();
        _monsterSound = GetComponent<MonsterSoundController>();

        _playerLayer = LayerMask.GetMask("Player");
        
        _stateMachine = new StateMachine();
        Idle = new Mon_IdleState(this);
        Move = new Mon_MoveState(this);
        Attack = new Mon_AttackState(this);
        Dead = new Mon_DeadState(this);
        Hurt = new Mon_HurtState(this);

        Name = _monster.Name;
        HP = _monster.HP;
        MoveSpeed = _monster.MoveSpeed;
        Range = _monster.Attack_Range;
        Damage = _monster.Attack_Damage;
        DetectRange = _monster.Detect_Range;
        
        _attackCol.radius = Range;
    }
}

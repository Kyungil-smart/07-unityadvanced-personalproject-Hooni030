using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControlller : MonoBehaviour
{
    private Coroutine _AttackRoutine;
    private Coroutine _HurtRoutine;
    
    [Header("Get Components")]
    [SerializeField] private GameObject _player;
    
    [SerializeField] private Player_Actions _playerActions;
    [SerializeField] public Animator _animator;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private CircleCollider2D _attackCol;
    
    public StateMachine _stateMachine;
    public Mon_IdleState Idle { get; private set; }
    public Mon_MoveState Move { get; private set; }
    public Mon_AttackState Attack { get; private set; }
    public Mon_HurtState Hurt { get; private set; }
    public Mon_DeadState Dead { get; private set; }
    
    private Vector2 _direction;
    private Vector2 _distance;
    
    [Space(20)]
    [Header("Monster Stats")]
    [SerializeField] private MonsterStat _monster;
    [SerializeField] private string _name;
    public string Name { get => _name; set { _name = value; } }
    [SerializeField] private float _hp;
    public float HP { get => _hp; set { _hp = value; } }
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed { get => _moveSpeed; set { _moveSpeed = value; } }
    [SerializeField] private float _damage;
    public float Damage { get => _damage; set { _damage = value; } }
    [SerializeField] private float _range;
    public float Range { get => _range; set { _range = value; } }

    public bool _canMove = true;
    public bool _isMove = false;
    public bool _canAttack = false;
    public bool _canHurt = true;

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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 0.5f, 0f, 0.3f);
        Gizmos.DrawSphere(transform.position, Range);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, Range);

    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Attack!");
        if (other.CompareTag("Player"))
        {
            _rb.linearVelocity = Vector2.zero;
            _canMove = false;
            _canAttack = true;
            _isMove = false;
            if (_AttackRoutine == null)
            {
                _AttackRoutine = StartCoroutine(AttackPlayer());
            }
        }

        if (other.CompareTag("Attack") && _canHurt)
        {
            FireballController fireball = other.GetComponent<FireballController>();
            HP -= fireball.Damage;
            DebugUtil.DebugingColor($"스켈레톤 체력 : {HP}", "Green");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _canAttack = false;
        }
    }

    private void Movement()
    {
        Debug.DrawRay(transform.position, _direction, Color.yellow);
        
        GetDirection();
        
        if (_canMove)
        {
            _isMove = true;
            FlipSprite();
            _rb.linearVelocity = _direction * MoveSpeed;
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


    private IEnumerator AttackPlayer()
    {
        // 몬스터 멈춤
        _canAttack = false;
        _canMove = false;
        yield return YieldContainer.Wait(1.5f);
        _canMove = true;
        _AttackRoutine = null;
    }

    private IEnumerator HurtDelay()
    {
        _canHurt = false;
        yield return YieldContainer.Wait(1.5f);
        _canHurt = true;
    }

    
    public void ChangeState(IState state)
    {
        _stateMachine.ChangeState(state);
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        
        _stateMachine = new StateMachine();
        Idle = new Mon_IdleState(this);
        Move = new Mon_MoveState(this);
        Attack = new Mon_AttackState(this);
        Dead = new Mon_DeadState(this);

        Name = _monster.Name;
        HP = _monster.HP;
        MoveSpeed = _monster.MoveSpeed;
        Range = _monster.Attack_Range;
        Damage = _monster.Attack_Damage;
        
        _attackCol.radius = Range;
    }
}

using UnityEngine;

public class FireballController : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _circleCol;
    [SerializeField] private Rigidbody2D _rb;
    public PlayerController _player;
    
    [SerializeField] private float _lifeTime;
        
    [SerializeField][Range(0f, 30f)] private float _speed;
    private Vector2 _direction;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();
        _circleCol = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        _direction = _player._direction;
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        _rb.linearVelocity = _direction * _speed;
    }
}

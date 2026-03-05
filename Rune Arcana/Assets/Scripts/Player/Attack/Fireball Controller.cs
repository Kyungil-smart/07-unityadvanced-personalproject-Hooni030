using UnityEngine;

public class FireballController : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _circleCol;
    [SerializeField] private Rigidbody2D _rb;
    public PlayerController _player;
    
    [SerializeField] private float _lifeTime;
        
    [SerializeField][Range(0f, 30f)] private float _speed;
    private Vector2 _direction;

    [SerializeField] private float _damage;
    public float Damage { get => _damage; private set => _damage = value; }
    

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();
        _circleCol = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        _direction = _player._direction;
        _damage = _player.Damage;
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemies") || other.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject, 0.2f);
        }
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _direction * _speed;
    }
}

using UnityEngine;

public class FireballController : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _circleCol;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private AudioSource _audioSource;
    public PlayerController _player;
    
    [SerializeField] private float _lifeTime;
        
    [SerializeField][Range(0f, 30f)] private float _speed;
    private Vector2 _direction;

    [SerializeField] private float _damage;
    

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();
        _circleCol = GetComponent<CircleCollider2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SoundManager.Instance.PlaySFX(_audioSource, 0.25f);
        _direction = _player._direction;
        _damage = _player.Damage;
        
        Destroy(gameObject, _lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemies"))
        {
            
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        _rb.linearVelocity = _direction * _speed;
    }
}

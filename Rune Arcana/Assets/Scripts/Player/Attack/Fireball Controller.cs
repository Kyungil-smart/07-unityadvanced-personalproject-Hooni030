using UnityEngine;

public class FireballController : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _circleCol;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    
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
        
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
            _audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.clip = _audioClip;
        _direction = _player._direction;
        _damage = _player.Damage;
        FireballSound();
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemies"))
        {
            Destroy(gameObject, 0.2f);
        }
    }

    public void FireballSound()
    {
        _audioSource.PlayOneShot(_audioSource.clip);
    }


    private void FixedUpdate()
    {
        _rb.linearVelocity = _direction * _speed;
    }
}

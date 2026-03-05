using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip[] _footStep = new AudioClip[3];
    [SerializeField] private float _footStepVolume;
    [SerializeField] private AudioClip _fireball;
    [SerializeField] private float _fireballVolume;
    [SerializeField] private AudioClip _playerHit;
    [SerializeField] private float _playerHitVolume;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void FootStepSound()
    {
        _source.clip = _footStep[Random.Range(0, _footStep.Length)];
        _source.volume = _footStepVolume;
        _source.PlayOneShot(_source.clip);
    }

    public void FireballSound()
    {
        _source.clip = _fireball;
        _source.volume = _fireballVolume;
        _source.PlayOneShot(_source.clip);
    }

    public void PlayerHitSound()
    {
        _source.clip = _playerHit;
        _source.volume = _playerHitVolume;
        _source.PlayOneShot(_source.clip);
    }
}

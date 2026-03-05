using UnityEngine;

public class MonsterSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    
    [SerializeField] private AudioClip _skeletonStep;
    [SerializeField] private float _stepVolume;
    [SerializeField] private AudioClip _skeletonHit;
    [SerializeField] private float _skeletonHitVolume;
    [SerializeField] private AudioClip _skeletonSmash;
    [SerializeField] private float _skeletonSmashVolume;
    

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SkeletonStep()
    {
        _audioSource.volume = _stepVolume;
        _audioSource.clip = _skeletonStep;
        _audioSource.PlayOneShot(_skeletonStep);
    }

    public void SkeletonHit()
    {
        _audioSource.volume = _skeletonHitVolume;
        _audioSource.clip = _skeletonHit;
        _audioSource.PlayOneShot(_skeletonHit);
    }

    public void SkeletonSmash()
    {
        _audioSource.volume = _skeletonSmashVolume;
        _audioSource.clip = _skeletonSmash;
        _audioSource.PlayOneShot(_skeletonSmash);
    }
}

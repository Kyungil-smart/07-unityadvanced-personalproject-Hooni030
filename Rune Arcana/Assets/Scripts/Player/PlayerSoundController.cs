using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] private AudioClip[] _footStep = new AudioClip[3];
    [SerializeField] private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }
    
    private void Start()
    {
        _source.volume = 0.15f;
    }

    public void FootStep()
    {
        _source.clip = _footStep[Random.Range(0, _footStep.Length)];
        _source.PlayOneShot(_source.clip);
    }
}

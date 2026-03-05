using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] private AudioClip[] _footStep = new AudioClip[3];
    [SerializeField] private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void FootStep()
    {
        _source.clip = _footStep[Random.Range(0, _footStep.Length)];
        SoundManager.Instance.PlaySFX(_source, 0.15f);
    }
}

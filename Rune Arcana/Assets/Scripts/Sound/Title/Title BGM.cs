using UnityEngine;

public class TitleBGM : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField][Range(0f,1.0f)] private float volume = 0.5f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SoundManager.Instance.PlaySound(_audioSource,volume);
    }
}

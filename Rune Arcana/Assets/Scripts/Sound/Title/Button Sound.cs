using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField][Range(0f,1.0f)] private float volume = 0.5f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void ButtonClick()
    {
        SoundManager.Instance.PlaySFX(_audioSource, volume);
    }
}

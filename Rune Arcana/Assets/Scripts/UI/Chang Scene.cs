using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangScene : MonoBehaviour
{
    private static readonly int Out = Animator.StringToHash("Change");
    
    [SerializeField] private Animator _animator;
    [SerializeField] [Range(0.1f, 10.0f)] private float _waitTime = 1f;
    private AudioSource _source;
    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
    }

    public void ChangeScene()
    {
        StartCoroutine(Change(_waitTime));
        _animator.SetBool(Out, true);
    }
    
    private void OnDisable()
    {
        StopCorouito
    }
    

    private IEnumerator Change(float time)
    {
        yield return YieldContainer.Wait(time);
        GameSceneManager.Instance.ChangeScene((int)SceneIndex.Tutorial);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangScene : MonoBehaviour
{
    private static readonly int Out = Animator.StringToHash("Out");
    
    [SerializeField] private Animator _animator;
    [SerializeField] private Canvas  _canvas;
    
    [SerializeField] [Range(0.1f, 10.0f)] private float _inTime = 1f;
    [SerializeField] [Range(0.1f, 10.0f)] private float _outTime = 2f;
    
    private AudioSource _source;
    
    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        TurnOnScene();
    }

    private void TurnOnScene()
    {
        StartCoroutine(OnScene(_inTime));
    }

    public void TurnOffScene()
    {
        _canvas.sortingOrder = 10;
        StartCoroutine(OffScene(_outTime));
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator OffScene(float time)
    {
        _canvas.sortingOrder = 10;
        _animator.SetBool(Out, true);
        yield return new WaitForSeconds(time);
        GameSceneManager.Instance.ChangeScene((int)SceneIndex.Tutorial);
    }

    private IEnumerator OnScene(float time)
    {
        _animator.SetBool(Out, false);
        yield return YieldContainer.Wait(time);
        _canvas.sortingOrder = -1;
    }
}

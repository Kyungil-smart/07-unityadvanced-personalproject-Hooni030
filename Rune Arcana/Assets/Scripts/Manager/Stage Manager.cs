using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private AudioSource _stageSound;
    [SerializeField] private AudioClip[] _stageSounds;
    [SerializeField][Range(0f, 1.0f)] private float _volume;
    [SerializeField] private PlayerController _playerController;
    
    [SerializeField] private List<GameObject> _monsters;
    
    [SerializeField] private GameObject _bossPrefab;
    
    [SerializeField] public int _monsterCount = 1;
    
    public bool BossApear;

    private void Awake()
    {
        _bossPrefab = GameObject.FindGameObjectWithTag("Boss");
        _stageSound =  GetComponent<AudioSource>();
    }
    private void Start()
    {
        _bossPrefab.SetActive(false);
        _stageSound.volume = _volume;
        _stageSound.clip = _stageSounds[(int)SoundType.Stage];
        _stageSound.Play();
    }

    private void Update()
    {
        CheckMonsterCount();
        KillAllMonsters();
        if (_playerController.isDead)
        {
            _stageSound.Stop();
        }
    }

    private void KillAllMonsters()
    {
        if (_monsterCount <= 0 && !BossApear)
        {
            BossApear = true;
            _bossPrefab.SetActive(true);
            _stageSound.clip = _stageSounds[(int)SoundType.Boss];
            _stageSound.Play();
        }
    }
    
    private void CheckMonsterCount()
    {
        _monsters.AddRange(GameObject.FindGameObjectsWithTag("Enemies"));
        _monsterCount = _monsters.Count;
        _monsters.Clear();
    }

    private enum SoundType
    {
        Stage,
        Boss
    }
}

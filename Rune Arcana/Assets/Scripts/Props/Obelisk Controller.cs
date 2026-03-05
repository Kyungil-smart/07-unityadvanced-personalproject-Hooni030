using System;
using UnityEngine;

public class ObeliskController : MonoBehaviour
{
    public GameObject _key;
    public BoxCollider2D _obeliskCol;
    public PlayerController _playerController;
    public ChangeScene ChangeScene;
    [SerializeField] private int _sceneIndex;

    private void Awake()
    {
        _obeliskCol = GetComponent<BoxCollider2D>();
    }
    
    private void Start()
    {
        _key.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _key.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_playerController.InteractInput)
        {
            ChangeScene.TurnOffScene(_sceneIndex);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _key.SetActive(false);
    }
}

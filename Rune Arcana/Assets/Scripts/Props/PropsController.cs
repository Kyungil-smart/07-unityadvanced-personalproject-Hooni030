using UnityEngine;

public class PropsController : MonoBehaviour
{
    public GameObject _player;
    public SpriteRenderer _sprite;

    private void Awake()
    {
        _sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        if (_player.transform.position.y >= transform.position.y - 1)
            _sprite.sortingLayerName = "Temp";
        else
            _sprite.sortingLayerName = "Props";
    }
}

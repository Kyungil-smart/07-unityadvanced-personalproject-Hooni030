using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BridgeController : MonoBehaviour
{
    public BoxCollider2D _boxCol2D;
    public TilemapCollider2D _riverCol;

    private void Awake()
    {
        _boxCol2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        RiverColliderSet(false);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        RiverColliderSet(true);
    }

    private void RiverColliderSet(bool set)
    {
        _riverCol.enabled = set;
    }
}

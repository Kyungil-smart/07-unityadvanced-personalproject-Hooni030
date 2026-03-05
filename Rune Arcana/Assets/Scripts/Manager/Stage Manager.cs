using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private int _monsterCount;
    [SerializeField] private List<GameObject> _monsters = new();

    private void Start()
    {
        _monsters.AddRange(GameObject.FindGameObjectsWithTag("Enemies"));
    }

    private void Update()
    {
        _monsterCount = _monsters.Count;
    }
}

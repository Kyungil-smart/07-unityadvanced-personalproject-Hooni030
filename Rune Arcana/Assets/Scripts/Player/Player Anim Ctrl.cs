using UnityEngine;

public class PlayerAnimCtrl : MonoBehaviour
{
    private static readonly int Move = Animator.StringToHash("Move");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Skill = Animator.StringToHash("Skill");
    private static readonly int Teleport = Animator.StringToHash("Teleport");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Dead = Animator.StringToHash("Dead");
    
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}

using UnityEngine;

public class PlayerAnimCtrl : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerController _playerController;
    
    private StateMachine _stateMachine;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _stateMachine = _playerController._stateMachine;
    }

    private void Update()
    {
        Movement();
        FireBall();
    }

    private void Movement()
    {
    }

    private void FireBall()
    {
       // _animator.SetBool(Attack, _playerController.AttackInput);
    }
}

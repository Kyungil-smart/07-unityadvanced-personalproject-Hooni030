using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class BookDescription : MonoBehaviour
{
    private static readonly int HowTo = Animator.StringToHash("HowTo");
    private static readonly int Credits = Animator.StringToHash("Credits");
    
    [SerializeField] private Animator BookAnimator;

    [SerializeField] private bool howtoPlay = false;
    [SerializeField] private bool credits = false;
    
    private void Awake()
    {
        BookAnimator = GetComponent<Animator>();
    }
    
    public void OnHowToPlay()
    {
        howtoPlay = !howtoPlay;
        credits = false;
        BookAnimator.SetBool(HowTo, howtoPlay);
        BookAnimator.SetBool(Credits, credits);
    }

    public void OnCredits()
    {
        credits = !credits;
        howtoPlay = false;
        BookAnimator.SetBool(HowTo, howtoPlay);
        BookAnimator.SetBool(Credits, credits);
    }

    public void StartQuit()
    {
        howtoPlay = false;
        credits = false;
        BookAnimator.SetBool(HowTo, howtoPlay);
        BookAnimator.SetBool(Credits, credits);
    }
}

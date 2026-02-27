using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class TitleUIController : MonoBehaviour
{
    // 파라미터 문자열 해싱
    private static readonly int PageDir = Animator.StringToHash("PageDir");
    private static readonly int Index = Animator.StringToHash("Index");
    private static readonly int Canceled = Animator.StringToHash("Canceled");
    private static readonly int Selected = Animator.StringToHash("Selected");

    [SerializeField] private Animator ButtonsAnimator;
    [SerializeField] private Image Book;
    [SerializeField] private Animator BookAnimator;
    
    private AudioSource _audioSource;

    private bool _firstClick = false;
    private int _currentPage = -1;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        ButtonsAnimator = GetComponent<Animator>();
        BookAnimator = Book.GetComponent<Animator>();
    }

    // 버튼 클릭시 실행
    public void OnClick(int button)
    {
        ChangeBookState(button);
    }

    /// <summary>
    /// 버튼 클릭 시 버튼 이미지 전환, 책 넘기는 애니메이션 전환, 씬 변경, 게임 종료
    /// </summary>
    /// <param name="button"></param>
    private void ChangeBookState(int button)
    {
        switch(button)
        {
            case 0:
                UIUpdate(true, false, 0, button);
                _currentPage = 0;
                break;
            case 3 :
                UIUpdate(true, false, 0, button);
                break;
            default:
                if (_currentPage == button)
                {
                    UIUpdate(true, false, 0, 0);
                    _currentPage = -1;
                    _firstClick = false;
                    return;
                }

                if (_currentPage < button)
                {
                    if(!_firstClick)
                    {
                        UIUpdate(false, true, 0, button);
                        _firstClick = true;
                    }
                    else
                    {
                        UIUpdate(false, true, 1, button);
                        SoundManager.Instance.PlaySFX(_audioSource, 1.5f);
                    }
                }
                else if (_currentPage > button)
                {
                    UIUpdate(false, true, -1, button);
                    SoundManager.Instance.PlaySFX(_audioSource,1.5f);
                }
                break;
        }
        _currentPage = button;
    }

    // 실제 파라미터 변경 함수
    private void UIUpdate(bool canceled, bool selected, int dir, int index)
    {
        ButtonsAnimator.SetInteger(Index, index);
        BookAnimator.SetInteger(PageDir, dir);
        BookAnimator.SetBool(Canceled, canceled);
        BookAnimator.SetBool(Selected, selected);
    }
}

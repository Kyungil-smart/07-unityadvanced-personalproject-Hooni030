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

    private bool _firstClick = false;
    private int _currentPage = -1;
    
    private void Awake()
    {
        ButtonsAnimator = GetComponent<Animator>();
        BookAnimator = Book.GetComponent<Animator>();
    }

    public void OnClick(int button)
    {
        ChangeBookState(button);
    }

    private void ChangeBookState(int button)
    {
        switch(button)
        {
            case 0:
                UIUpdate(true, false, 0, button);
                _currentPage = 0;
                GameSceneManager.Instance.ChangeScene((int)SceneIndex.Tutorial);
                break;
            case 3 :
                UIUpdate(true, false, 0, button);
                GameSceneManager.Instance.GameQuit();
                break;
            default:
                if (_currentPage == button)
                {
                    UIUpdate(true, false, 0, 0);
                    _currentPage = -1;
                    _firstClick = false;
                }
                else if (_currentPage < button)
                {
                    if(!_firstClick)
                    {
                        UIUpdate(false, true, 0, button);
                        _currentPage = -1;
                        _firstClick = true;
                    }
                    else
                    {
                        UIUpdate(false, true, 1, button);
                        _currentPage = button;
                    }
                }
                else if (_currentPage > button)
                {
                    UIUpdate(false, true, -1, button);
                    _currentPage = button;
                }
                break;
        }
        Debug.Log($"{_currentPage} : {button}");
    }

    private void UIUpdate(bool canceled, bool selected, int dir, int index)
    {
        ButtonsAnimator.SetInteger(Index, index);
        BookAnimator.SetInteger(PageDir, dir);
        BookAnimator.SetBool(Canceled, canceled);
        BookAnimator.SetBool(Selected, selected);
    }
}

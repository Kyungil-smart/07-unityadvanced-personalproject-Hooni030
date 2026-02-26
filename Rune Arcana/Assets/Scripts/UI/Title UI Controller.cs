using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class TitleUIController : MonoBehaviour
{
    [SerializeField] private Animator ButtonsAnimator;
    [SerializeField] private Image Book;
    [SerializeField] private Animator BookAnimator;

    private bool isCanceled = true;
    private bool isSelected = false;

    private bool isPressed = false;

    private int _currentPage = -1;
    
    private void Awake()
    {
        ButtonsAnimator = GetComponent<Animator>();
        BookAnimator = Book.GetComponent<Animator>();
    }

    private void Update()
    {
        BookAnimator.SetInteger("PageDir", 0);
    }

    public void OnClick(int button)
    {
        ChangeBookState(button);
    }

    private void ChangeBookState(int page)
    {
        switch(page)
        {
            case 0:
                UIUpdate(true, false, 0, page);
                _currentPage = 0;
                GameSceneManager.Instance.ChangeScene((int)SceneIndex.Tutorial);
                break;
            case 3 :
                UIUpdate(true, false, 0, page);
                GameSceneManager.Instance.GameQuit();
                break;
            default:
                StartCoroutine(PageCoroutine(page));
                if (_currentPage == page)
                {
                    UIUpdate(!isCanceled, !isSelected, 0, page);
                    isCanceled = !isCanceled;
                    isSelected = !isSelected;
                }
                else if (_currentPage < page)
                {
                    UIUpdate(false, true, 1, page);
                    Debug.Log($"{_currentPage}, {page}");
                }
                else if (_currentPage > page)
                {
                    UIUpdate(false, true, -1, page);
                    Debug.Log($"{_currentPage}, {page}");
                }
                break;
        }
        
        _currentPage = page;
    }

    private void UIUpdate(bool canceled, bool selected, int dir, int index)
    {
        ButtonsAnimator.SetInteger("Index", index);
        
        BookAnimator.SetInteger("PageDir", dir);
        BookAnimator.SetBool("Canceled", canceled);
        BookAnimator.SetBool("Selected", selected);
    }

    private IEnumerator PageCoroutine(int page)
    {
        yield return new WaitUntil(() => isPressed);
        UIUpdate(false, true, -1, page);
        
    }
}

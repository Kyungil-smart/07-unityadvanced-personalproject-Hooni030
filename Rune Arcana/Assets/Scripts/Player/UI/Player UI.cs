using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private StageManager _stageManager;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _count;
    [SerializeField] private TextMeshProUGUI _boss;
    
    [SerializeField] private Image _hpBar;
    
    [SerializeField] private PlayerController player;
    [SerializeField] private float _hp;
    [SerializeField] private float _maxHp;
    [SerializeField] private float _hpPercent;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        _hp = player.HP;
        _maxHp = player.HP;
        _boss.gameObject.SetActive(false);
    }
    
    private void Update()
    {
        MonsterCount();
        BossAppear();
        HpBar();
    }
    
    private void MonsterCount()
    {
        _count.text = _stageManager._monsterCount.ToString();
    }

    private void BossAppear()
    {
        if (_stageManager.BossAppear)
        {
            _boss.gameObject.SetActive(true);
            _count.gameObject.SetActive(false);
            _text.gameObject.SetActive(false);
        }
    }

    private void HpBar()
    {
        _hp = player.HP;
        _hpPercent = _hp / _maxHp;
        _hpBar.fillAmount = _hpPercent;
    }
}

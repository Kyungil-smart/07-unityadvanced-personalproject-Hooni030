using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Scriptable Objects/Monster")]
public class Monster : ScriptableObject
{
    public string Name;
    public float HP;
    public float MoveSpeed;
    public float Attack_Damage;
    public float Attack_Range;
    
    public float Drop_Gold;
}

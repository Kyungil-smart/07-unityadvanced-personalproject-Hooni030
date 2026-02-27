using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "Scriptable Objects/PlayerStat")]
public class PlayerStat : ScriptableObject
{
    public float HP;
    public float MoveSpeed;
    public float AvoidDistance;
    public float Gold;
}

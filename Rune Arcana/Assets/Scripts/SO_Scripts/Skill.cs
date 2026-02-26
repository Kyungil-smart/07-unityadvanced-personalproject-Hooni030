using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Objects/Skill")]
public class Skill : ScriptableObject
{
    public Sprite SkillIcon;
    public string Name;
    public string Description;
    public float Cooldown;
    public float Damage;
    public float Range;
    public float ProjectileSpeed;
    public int Skill_Purchase_Cost;
}

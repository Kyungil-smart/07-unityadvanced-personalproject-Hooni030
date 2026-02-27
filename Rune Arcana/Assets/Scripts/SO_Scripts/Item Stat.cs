using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public Sprite ItemIcon;
    public string Name;
    public string Rarity;
    public string Description;
    public int Item_Purchase_Cost;
    
}

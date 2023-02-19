using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "New Item")]
public class Item : ScriptableObject
{
    public string itemId;
	public string itemName;
	public string itemDescription;
	public int stackMax, stackCurrent;
	public ItemType itemType;
	public int cost;
	public Sprite icon;
	
}
public enum ItemType
{
	Weapon,
	Armour,
	Consumable,
	Materials,
	Quest,
	Misc
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This creates a menu, we name the menu according to what it is, e.g Items / Weapons
//The fileName is the default name for the object.
[CreateAssetMenu(menuName = "Item", fileName = "New Item")]
public class Item : ScriptableObject
{
    public string itemId;
    public string itemName;
    public string ItemDescription;
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
    Quests,
    Misc
    //Dont put money in here lol (You should use a seperate thing or ensure currency cannot be sold)
}
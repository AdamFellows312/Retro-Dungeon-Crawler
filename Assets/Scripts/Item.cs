using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item", fileName = "NewItem")]
public class Item : ScriptableObject
{
    public string itemName;
    [Space(10)]

    public int ID;
    [Space(10)]

    public Sprite itemSprite;
}

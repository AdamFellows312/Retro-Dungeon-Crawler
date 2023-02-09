using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCore : MonoBehaviour
{
    [SerializeField] private ItemSlot[] itemSlots;
    [SerializeField] private List<Item> startingItems;
    [Space(10)]

    [SerializeField] private Transform slotHolder;

    private void Start()
    {
        // Set the inventory's starting items
        SetStartingItems();
    }

    private void OnValidate()
    {
        if (slotHolder != null)
        {
            // Get the item slots
            itemSlots = slotHolder.GetComponentsInChildren<ItemSlot>();
        }
    }

    private void SetStartingItems()
    {
        int i = 0;

        for (; i < startingItems.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = startingItems[i];
        }


        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null)
            {
                itemSlots[i].Item = item;
                return true;
            }
        }
        return false;
    }
}

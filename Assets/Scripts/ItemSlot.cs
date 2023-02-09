using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private Color normalColour = Color.white;
    private Color disabledColour = Color.clear;

    public Image slotImage;

    [SerializeField] private Item _item;

    public Item Item
    {
        get { return _item; }
        set
        {
            _item = value;

            if (_item == null)
            {
                slotImage.color = disabledColour;
            }
            else if (_item != null)
            {
                slotImage.color = normalColour;
                slotImage.sprite = _item.itemSprite;
            } 
        }
    }

    protected virtual void OnValidate()
    {
        // Get the slot image component
        if (slotImage == null) { slotImage = transform.GetChild(0).GetComponent<Image>(); }
    } 
}

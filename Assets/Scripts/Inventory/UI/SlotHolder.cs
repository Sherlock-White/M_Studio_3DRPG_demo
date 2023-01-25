using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotTyope { BAG,WEAPON,ARMOR, ACTION }
public class SlotHolder : MonoBehaviour
{
    public SlotTyope slotTyope;
    public ItemUI itemUI;

    public void UpdateItem()
    {
        switch (slotTyope)
        {
            case SlotTyope.BAG:
                itemUI.Bag = InventoryManager.Instance.inventoryData;
                break;
            case SlotTyope.WEAPON:

                break;
            case SlotTyope.ARMOR:

                break;
            case SlotTyope.ACTION:

                break;
        }
        var item = itemUI.Bag.items[itemUI.Index];
        itemUI.SetupItemUI(item.itemData, item.amount);
    }

}

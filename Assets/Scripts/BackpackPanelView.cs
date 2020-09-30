using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackPanelView : MonoBehaviour
{
    [SerializeField]
    Image[] slotViews;
    [SerializeField]
    Sprite[] itemSprites;
    [SerializeField]
    Backpack backpack;

    void Start()
    {
        
    }

    public void ShowBackpack()
    {
        for (int i = 0; i < slotViews.Length; i++)
        {
            BackpackSlot slot = backpack[i];

            if (slot.StoredItem == null) {
                slotViews[i].sprite = null;
            }
            else 
            {
                slotViews[i].sprite = itemSprites[(int)slot.StoredItem.Config.ItemType];
            }
        }
    }

}

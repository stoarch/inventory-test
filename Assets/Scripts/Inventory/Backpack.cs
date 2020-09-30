using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Backpack : MonoBehaviour
{
    public enum BackpackOperation
    {
        Unknown,
        Placing,
        Removing
    }

    [SerializeField]
    UnityEvent OnShowInventory;
    [SerializeField]
    UnityEvent OnHideInventory;
    [SerializeField]
    int maxItems = 12;
    [SerializeField]
    BackpackSlot[] slots;
    [SerializeField]
    UnityEvent OnPlacingItem;
    [SerializeField]
    UnityEvent OnRemovingItem;

    int activeSlotNo = 0;//we need to track last inventory position
    private BackpackOperation activeOperation;

    public BackpackSlot this[int index] => slots[index];
    public Item ActiveItem {get; private set;}
    public BackpackOperation ActiveOperation => activeOperation;




    void Start()
    {
        
    }

    void Update()
    {
        
    }

    const int LEFT_BUTTON = 0;

    private void OnMouseDown()
    {
        if(Input.GetMouseButton(LEFT_BUTTON))//pressed
        {
           if(OnShowInventory != null)
            {
                OnShowInventory.Invoke();
            } 
        }
    }

    private void OnMouseUp()
    {
        if(!Input.GetMouseButton(LEFT_BUTTON)) //released
        {
            if(OnHideInventory != null)
            {
                OnHideInventory.Invoke();
            }
        }
    }

    /// <summary>
    /// Place item and game object inside free slot in backpack (up to max items)
    /// </summary>
    /// <remarks>
    ///     1. Game object is initiated to animate (tween) from it position to free slot in
    ///     inventory. Tweening also set scale for it.
    ///     But item is added instant. 
    ///     2. Items is stored in list with max_items size
    ///     3. Items pushed from back, but can be retrieved from anywhere. So packing on retrieval 
    ///         is a must.
    /// </remarks>
    /// <param name="item">to place into inventory</param>
    /// <returns>slot in which it placed or null if nothing found</returns>
    internal BackpackSlot PlaceInside(Item item)
    {
        if(activeSlotNo == -1)
        {
            Debug.LogWarning("Backpack is full");
            return null;
        }

        ActiveItem = item;
        activeOperation = BackpackOperation.Placing;

        var slot = slots[activeSlotNo];

        slot.PlaceInsideAnimated(item);

        FindNewActiveSlotNo();

        if (OnPlacingItem != null) 
        {
            OnPlacingItem.Invoke();
        }

        return slot;
    }

    internal void ReleaseItem(Item storedItem)
    {
        activeOperation = BackpackOperation.Removing;
        ActiveItem = storedItem;

        if(OnRemovingItem != null)
        {
            OnRemovingItem.Invoke();
        }
    }

    private void FindNewActiveSlotNo()
    {
        activeSlotNo = -1;

        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].StoredItem == null)
            {
                activeSlotNo = i;
                break;
            }
        }
    }


    /// <summary>
    /// Shift all items from last slot up to this one (with reparenting) and change 
    /// activeSlotNo to valid positions;
    /// </summary>
    /// <param name="slotNo">from which to start shift to end of active slots</param>
    public void ShiftItems(int slotNo)
    {
        if((slotNo < 0)||(slotNo >= slots.Length)||(slotNo > activeSlotNo))
        {
            Debug.LogWarning($"Slot number {slotNo} is invalid. Must be [0..{slots.Length})");
            return;
        }

        for (int i = slotNo; i < activeSlotNo; i++)
        {
            slots[i].ClearItem();//we not need it
            slots[i].PlaceInsideInstant(slots[i + 1].StoredItem);
        }

        slots[activeSlotNo].ClearItem(); //and last one should be clear
        activeSlotNo -= 1;
    }
}

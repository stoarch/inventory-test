using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BackpackSlot : MonoBehaviour
{
    [SerializeField]
    float moveTime = 1.0f;//sec
    [SerializeField]
    float scaleTime = 1.0f;//sec
    [SerializeField]
    Transform itemPlacer;
    [SerializeField]
    Transform freeObjects;
    [SerializeField]
    UnityEvent OnReleaseItem;

    Item storedItem;

    public Item StoredItem => storedItem;

    Vector3 originalScale;

    void Start()
    {
        if(itemPlacer == null)
        {
            Debug.LogError("Item placer not set");
            gameObject.SetActive(false);
            return;
        }        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Set current slot item to this item and animate placing inside it.
    /// </summary>
    /// <param name="item">item to store</param>
    internal void PlaceInsideAnimated(Item item)
    {
        PlaceInsideInternal(item);

        LeanTween.move(item.gameObject, itemPlacer.transform, moveTime);//.setOnComplete(MakeParentForItem);
    }

    internal void PlaceInsideInstant(Item item)
    {
        PlaceInsideInternal(item);
    }

    private void PlaceInsideInternal(Item item)
    {
        if(storedItem != null)
        {
            Debug.LogWarning("Item already placed");
            return;
        }

        storedItem = item;

        MakeParentForItem();
    }

    private void MakeParentForItem()
    {
        if(storedItem == null)
        {
            return;
        }

        originalScale = storedItem.transform.localScale;
        storedItem.gameObject.transform.parent = itemPlacer.transform;
        storedItem.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    /// <summary>
    /// Restore item scale and free it from slot
    /// </summary>
    internal void ReleaseItem()
    {
        if(OnReleaseItem != null)
        {
            OnReleaseItem.Invoke();
        }

        if (storedItem != null)
        {
            storedItem.transform.parent = freeObjects;
            storedItem.transform.localScale = originalScale;
            storedItem.Drop();
        }

        ClearItem();
    }

    internal void ClearItem()
    {
        storedItem = null;
    }
}

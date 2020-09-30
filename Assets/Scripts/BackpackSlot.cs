using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackSlot : MonoBehaviour
{
    [SerializeField]
    float moveTime = 1.0f;//sec
    [SerializeField]
    float scaleTime = 1.0f;//sec
    [SerializeField]
    Transform itemPlacer;

    Item storedItem;

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
    internal void PlaceInside(Item item)
    {
        storedItem = item;

        MakeParentForItem();
        LeanTween.move(item.gameObject, itemPlacer.transform, moveTime);//.setOnComplete(MakeParentForItem);
    }

    private void MakeParentForItem()
    {
        originalScale = storedItem.transform.localScale;
        storedItem.gameObject.transform.parent = itemPlacer.transform;
        storedItem.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}

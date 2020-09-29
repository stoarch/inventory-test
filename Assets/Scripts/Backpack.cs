using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Backpack : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnShowInventory;
    [SerializeField]
    UnityEvent OnHideInventory;

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
}

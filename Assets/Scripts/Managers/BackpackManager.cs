using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackpackManager : MonoBehaviour
{
    [SerializeField]
    Backpack backpack;
    [SerializeField]
    RectTransform panel;
    [SerializeField]
    BackpackPanelView backpackView;


    public void HidePanel()
    {
        if(panel == null)
        {
            return;
        }

        panel.gameObject.SetActive(false);

    }

    public void ShowPanel()
    {
        if(panel == null)
        {
            return;
        }

        panel.gameObject.SetActive(true);

        backpackView.ShowBackpack();
    }
}

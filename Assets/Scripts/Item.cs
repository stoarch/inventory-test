using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemConfig config;

    private bool dragging = false;
    private float distance;
    private Camera mainCamera;
    private Rigidbody body;

    void Start()
    {
        mainCamera = Camera.main; //for performance 
        if(mainCamera == null)
        {
            Debug.LogError("Unable to find main camera");
            gameObject.SetActive(false);
            return;
        }

        body = GetComponent<Rigidbody>();

        if(body == null)
        {
            Debug.LogError("Unable to find component rigidbody");
            gameObject.SetActive(false);
            return;
        }
    }

    void Update()
    {
        if (dragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            var rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;
        }        
    }

    private void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, mainCamera.transform.position);
        dragging = true;
        body.isKinematic = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
        body.isKinematic = false;
    }

}

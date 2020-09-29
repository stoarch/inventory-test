using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemConfig config;
    [SerializeField]
    private Color originalColor = Color.white;
    [SerializeField]
    private Color highlightColor = Color.green;
    [SerializeField]
    private Color draggingColor = Color.yellow;

    private bool dragging = false;
    private float distance;
    private Camera mainCamera;
    private Rigidbody body;
    private Renderer renderer;

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

        renderer = GetComponent<Renderer>();
        if(renderer == null)
        {
            Debug.LogError("Renderer not found");
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
        renderer.material.color = draggingColor;
    }

    private void OnMouseUp()
    {
        dragging = false;
        body.isKinematic = false;
        renderer.material.color = originalColor;
    }

    private void OnMouseEnter()
    {
        renderer.material.color = highlightColor;
    }

    private void OnMouseExit()
    {
        if (dragging)
        {
            renderer.material.color = draggingColor;
        }
        else
        {
            renderer.material.color = originalColor;
        }
    }


}

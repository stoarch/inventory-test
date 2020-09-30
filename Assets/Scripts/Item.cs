using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
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
    [SerializeField]
    private LayerMask backpackMask;
    [SerializeField]
    private float workDistance = 3.0f;

    private bool dragging = false;
    private float distance;
    private Camera mainCamera;
    private Rigidbody body;
    private new Renderer renderer;

    private const int MAX_RAYCAST_DIST = 1000;
    private const int LEFT_BUTTON = 0;

    public bool IsPlacedInSlot { get; set; }

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

        IsPlacedInSlot = false;
    }

    void FixedUpdate()
    {
        if (dragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            var rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;

            if(Input.GetMouseButton(LEFT_BUTTON)) //we actively hold it, skip
            {
                return;
            }

            //check if we over backpack
            RaycastHit rayHit;

            if (Physics.Raycast(ray.origin, ray.direction, out rayHit, MAX_RAYCAST_DIST, backpackMask))
            {
                if(rayHit.collider.tag == "Backpack")//place it into backpack
                {
                    Backpack backpack = rayHit.collider.GetComponent<Backpack>();

                    if(backpack == null)
                    {
                        Debug.LogError("On backpack game object not found Backpack script");
                        return;
                    }

                    backpack.PlaceInside(this);

                    IsPlacedInSlot = true;
                }
            }
        }        
    }

    private void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, mainCamera.transform.position);
        if(distance > workDistance)
        {
            distance = workDistance;
        }

        dragging = true;
        body.isKinematic = true;
        renderer.material.color = draggingColor;
    }

    private void OnMouseUp()
    {
        dragging = false;
        if (!IsPlacedInSlot)
        {
            body.isKinematic = false;
        }

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

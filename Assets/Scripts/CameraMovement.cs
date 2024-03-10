using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraMovement : MonoBehaviour
{
    private Vector3 _offset;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    private bool isDragging;

    private Vector3 previousPosition;

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        // {
        //     previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        // }

        // if(Input.GetMouseButton(0) || Input.GetMouseButtonDown(1))
        // {
        //     Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

        //     cam.transform.position = target.position;

        //     cam.transform.Rotate(new Vector3(1,0,0), direction.y * 180);
        //     cam.transform.Rotate(new Vector3(0,1,0), -direction.x * 180, Space.World);
        //     cam.transform.Translate(new Vector3(0,0,-10));

        //     previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        // }

        // else
        // {
        //     cam.transform.position = target.position;
        //     cam.transform.Translate(new Vector3(0,0,-10));
        //     previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        // }

        // if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        // {
        //     previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        // }

        // if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(1))
        // {
        //     Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

        //     cam.transform.position = target.position;

        //     cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
        //     cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
        //     cam.transform.Translate(new Vector3(0, 0, -10));

        //     previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        // }
        // else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        // {
        //     // Recenter camera
        //     cam.transform.position = target.position;
        //     cam.transform.Translate(new Vector3(0, 0, -10));
        // }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            isDragging = true;
        }

        if (isDragging && (Input.GetMouseButton(0) || Input.GetMouseButton(1)))
        {
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            cam.transform.position = target.position;

            cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
            cam.transform.Translate(new Vector3(0, 0, -10));

            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            // Recenter camera only if dragging was happening
            if (isDragging)
            {
                cam.transform.position = target.position;
                cam.transform.Translate(new Vector3(0, 0, -10));
                isDragging = false;
            }
        }
    }
}

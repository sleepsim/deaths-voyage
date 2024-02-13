using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    private Vector3 _input;
    public Camera playerCamera;


    public GameObject targetEnemy;
    public float stoppingDistance;

    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void Update() 
    {
        Look();
        Target();
    }

    private void FixedUpdate() 
    {
        GatherInput();
        Move();
    }

    private void GatherInput() 
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Get input relative to the camera's forward direction
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        _input = horizontalInput * cameraRight + verticalInput * cameraForward;
    }

    private void Look() 
    {
        if(_input == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(_input, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
    }

    private void Move() 
    {
        _rb.MovePosition(transform.position + _input.normalized * _speed * Time.deltaTime);
    }


    // Targeting and combat
    public void Target()
    {   
        // Checking for mouse clicks seen by camera
        RaycastHit hit;
    
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            // Check if gameobject is enemy
            if(hit.collider.tag == "Enemy")
            {
                if(Input.GetMouseButtonDown(0))
                {
                    if(hit.collider.gameObject == targetEnemy)
                    {   
                        // Turn off outline and remove target
                        targetEnemy = null;
                        hit.collider.gameObject.GetComponent<DisableOutline>().toggleOutline(); 
                    }
                    else if(targetEnemy != null)
                    {   
                        // Turn off outline and remove old target
                        targetEnemy.GetComponent<DisableOutline>().toggleOutline();
                        targetEnemy = null;

                        // Assign new target and outline
                        targetEnemy = hit.collider.gameObject;
                        hit.collider.gameObject.GetComponent<DisableOutline>().toggleOutline(); 
                    }
                    else
                    {
                        // Assign new target and outline
                        targetEnemy = hit.collider.gameObject;
                        hit.collider.gameObject.GetComponent<DisableOutline>().toggleOutline(); 
                    }
                }
            }
        }
    }

    // Checking for AOE circles
}

public static class Helpers 
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
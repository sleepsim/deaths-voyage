using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 45;
    [SerializeField] private float _rotationSpeed = 0.2f;
    private Vector3 _input;
    public Camera playerCamera;

    private bool isMoving;

    public GameObject targetEnemy;
    public float stoppingDistance;
    private float decelRate = 0.5f;

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

    //Old movement Script

    // private void GatherInput2()
    // {
    //     float horizontalInput = Input.GetAxisRaw("Horizontal");
    //     float verticalInput = Input.GetAxisRaw("Vertical");

    //     // Get input relative to the camera's forward direction
    //     Vector3 cameraForward = playerCamera.transform.forward;
    //     Vector3 cameraRight = playerCamera.transform.right;
    //     cameraForward.y = 0f;
    //     cameraRight.y = 0f;
    //     cameraForward.Normalize();
    //     cameraRight.Normalize();

    //     // Adjust the horizontal input to make left and right movement slower
    //     float adjustedHorizontalInput = horizontalInput * _rotationSpeed;

    //     _input = adjustedHorizontalInput * cameraRight + verticalInput * cameraForward;
    // }

    private void GatherInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (verticalInput > 0)
        {
            // Get input relative to the boat's forward direction
            Vector3 boatForward = transform.forward;
            Vector3 boatRight = transform.right;
            boatForward.y = 0f;
            boatRight.y = 0f;
            boatForward.Normalize();
            boatRight.Normalize();

            // Adjust the horizontal input to make left and right movement slower
            float adjustedHorizontalInput = horizontalInput * _rotationSpeed;

            // Calculate the input direction based on boat's orientation
            _input = adjustedHorizontalInput * boatRight + verticalInput * boatForward;

            isMoving = true;
        }
        else
        {
            // If no forward/backward movement, set input to zero
            _input = Vector3.zero;
            isMoving = false;
        }
    }

    private void Look()
    {
        if (_input == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(_input, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        while (isMoving)
        {
            // _rb.AddForce(transform.position + _input.normalized * _speed * Time.deltaTime);
            Debug.Log(transform.position + _input.normalized);

            _rb.velocity = transform.position + _input.normalized * _speed * Time.deltaTime;

        }


    }


    // Targeting and combat
    public void Target()
    {
        // Checking for mouse clicks seen by camera
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            // Check if gameobject is enemy
            if (hit.collider.tag == "Enemy")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.gameObject == targetEnemy)
                    {
                        // Turn off outline and remove target
                        targetEnemy = null;
                        hit.collider.gameObject.GetComponent<EnemyController>().toggleOutline();
                    }
                    else if (targetEnemy != null && hit.collider.gameObject != targetEnemy)
                    {
                        // Turn off outline and remove old target
                        targetEnemy.GetComponent<EnemyController>().toggleOutline();
                        targetEnemy = null;

                        // Assign new target and outline
                        targetEnemy = hit.collider.gameObject;
                        hit.collider.gameObject.GetComponent<EnemyController>().toggleOutline();
                    }
                    else
                    {
                        // Assign new target and outline
                        targetEnemy = hit.collider.gameObject;
                        hit.collider.gameObject.GetComponent<EnemyController>().toggleOutline();
                    }
                }
            }
        }

        if (targetEnemy != null)
        {
            if (CheckDistance(targetEnemy))
            {
                targetEnemy.GetComponent<EnemyController>().SetOutlineGreen(true);
            }
            else
            {
                targetEnemy.GetComponent<EnemyController>().SetOutlineGreen(false);
            }
        }
    }

    // Check distance between player and targeted enemy
    public bool CheckDistance(GameObject other)
    {
        return Vector3.Distance(other.transform.position, transform.position) <= GetComponent<Stats>().range;
    }
}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
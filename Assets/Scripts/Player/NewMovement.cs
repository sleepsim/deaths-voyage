// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class NewMovement : MonoBehaviour
// {
//     [SerializeField] private Rigidbody _rb;
//     [SerializeField] private float _rotationSpeed = 1f; //Actual turning speed
//     [SerializeField] private float _turnSpeed = 20; //For model rotation
//     [SerializeField] private float _speed = 7;
//     [SerializeField] private float _acceleration = 2f; // accel
//     [SerializeField] private float _deceleration = 2f; // decel
//     [SerializeField] private float _dashSpeed = 10; // Speed for dashing
//     [SerializeField] private float _dashDuration = 0.5f; // Duration of the dash
//     [SerializeField] private float _dashCooldown = 2f; // cd
//     private Vector3 _input;
//     public Vector3 _currentVelocity;
//     private bool isMoving;
//     public bool isDashing;
//     public float dashTimer;
//     public float dashCooldownTimer;

//     void Start()
//     {

//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // Check if Dash button pressed, and not under cooldown
//         if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && dashCooldownTimer <= 0)
//         {
//             StartDash();
//         }
//         Look();
//     }

//     void FixedUpdate()
//     {
//         GatherInput();
//         Move();
//         UpdateDash();
//     }

//     private void StartDash()
//     {
//         isDashing = true;
//         dashTimer = _dashDuration;
//         dashCooldownTimer = _dashCooldown;

//         // Apply a sudden burst of speed in the current direction
//         _rb.velocity = transform.forward * _dashSpeed;
//     }

//     private void UpdateDash()
//     {
//         if (isDashing)
//         {

//             dashTimer -= Time.deltaTime;
//             if (dashTimer <= 0)
//             {
//                 // End dash
//                 isDashing = false;
//                 // Apply deceleration after dash ends
//                 // _currentVelocity = Vector3.MoveTowards(_currentVelocity, Vector3.zero, _deceleration * 100f * Time.deltaTime);
//             }
//         }
//         else
//         {
//             if (dashCooldownTimer > 0)
//             {
//                 dashCooldownTimer -= Time.deltaTime;
//             }
//             // if (_currentVelocity.magnitude > _speed)
//             // {
//             //     _currentVelocity = Vector3.MoveTowards(_currentVelocity, Vector3.zero, _deceleration * 100f * Time.deltaTime);
//             // }
//         }
//     }

//     private void Look()
//     {
//         if (_input == Vector3.zero) return;

//         Quaternion targetRotation = Quaternion.LookRotation(_input, Vector3.up);
//         transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
//     }

//     private void GatherInput()
//     {
//         float horizontalInput = Input.GetAxisRaw("Horizontal");
//         float verticalInput = Input.GetAxisRaw("Vertical");

//         if (verticalInput != 0 || horizontalInput != 0)
//         {
//             // Get input relative to the boat's forward direction
//             Vector3 boatForward = transform.forward;
//             Vector3 boatRight = transform.right;
//             boatForward.y = 0f;
//             boatRight.y = 0f;
//             boatForward.Normalize();
//             boatRight.Normalize();

//             // Adjust the horizontal input to make left and right movement slower
//             float adjustedHorizontalInput = horizontalInput * _rotationSpeed;

//             // Calculate the input direction based on boat's orientation
//             _input = adjustedHorizontalInput * boatRight + verticalInput * boatForward;
//             isMoving = true;
//         }
//         else
//         {
//             // If no forward/backward movement, set input to zero
//             _input = Vector3.zero;
//             isMoving = false;
//         }
//     }

//     private void Move()
//     {
//         if (isMoving && !isDashing)
//         {
//             // Calculate target velocity based on input and speed
//             Vector3 targetVelocity = _input.normalized * _speed;

//             // Apply acceleration
//             _currentVelocity = Vector3.MoveTowards(_currentVelocity, targetVelocity, _acceleration * Time.deltaTime);
//         }
//         else if (!isDashing)
//         {
//             // If not moving, apply deceleration
//             _currentVelocity = Vector3.MoveTowards(_currentVelocity, Vector3.zero, _deceleration * Time.deltaTime);
//         }

//         Debug.Log(_currentVelocity);
//         // Move the rigidbody
//         _rb.MovePosition(transform.position + _currentVelocity * Time.deltaTime);
//     }
// }



using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to set the speed of the boat
    public float rotationSpeed = 2f; // Adjust this to set the rotation speed of the boat
    public float dashSpeedMultiplier = 2f; // Multiplier for dash speed
    public float dashDuration = 0.5f; // Duration of dash in seconds

    private Rigidbody rb;
    private bool isDashing = false;
    private float originalMoveSpeed;

    [Header("Dash")]
    public int maxDash = 3;
    public int currentDash;
    public float dashIncreaseInterval = 3f;
    public float timeUntilDashRefill = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalMoveSpeed = moveSpeed;
        StartCoroutine(RegenerateDash());
        currentDash = maxDash;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && currentDash > 0)
        {
            StartCoroutine(Dash());
            currentDash--;
        }

        if (currentDash < maxDash)
        {
            timeUntilDashRefill -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        // Get input from player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move the boat
        Vector3 movement = transform.forward * verticalInput * moveSpeed;
        rb.velocity = movement;

        // Rotate the boat
        float rotation = horizontalInput * rotationSpeed;
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotation);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        moveSpeed *= dashSpeedMultiplier; // Increase move speed during dash
        yield return new WaitForSeconds(dashDuration);
        moveSpeed = originalMoveSpeed; // Reset move speed after dash
        isDashing = false;

        //Add ammo everytime u dash
        if (GetComponent<NewCombat>().currentAmmo < GetComponent<NewCombat>().maxAmmo)
        {
            GetComponent<NewCombat>().currentAmmo += 1;
        }
    }

    IEnumerator RegenerateDash()
    {
        while (true)
        {
            yield return new WaitForSeconds(dashIncreaseInterval);
            timeUntilDashRefill = dashIncreaseInterval;
            if (currentDash < maxDash)
            {
                currentDash++;
            }
        }
    }
}

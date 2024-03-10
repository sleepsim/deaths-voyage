using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _rotationSpeed = 0.2f; //Actual turning speed
    [SerializeField] private float _turnSpeed = 45; //For model rotation
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _acceleration = 2f; // accel
    [SerializeField] private float _deceleration = 2f; // decel
    [SerializeField] private float _dashSpeed = 10; // Speed for dashing
    [SerializeField] private float _dashDuration = 0.5f; // Duration of the dash
    [SerializeField] private float _dashCooldown = 2f; // cd
    private Vector3 _input;
    public Vector3 _currentVelocity;
    private bool isMoving;
    public bool isDashing;
    public float dashTimer;
    public float dashCooldownTimer;




    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check if Dash button pressed, and not under cooldown
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && dashCooldownTimer <= 0)
        {
            StartDash();
        }
        Look();
    }

    void FixedUpdate()
    {
        GatherInput();
        Move();
        UpdateDash();
    }

    private void StartDash()
    {
        isDashing = true;
        dashTimer = _dashDuration;
        dashCooldownTimer = _dashCooldown;

        // Apply a sudden burst of speed in the current direction
        _currentVelocity = transform.forward * _dashSpeed;
    }

    private void UpdateDash()
    {
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                // End dash
                isDashing = false;
                // Apply deceleration after dash ends
                _currentVelocity = Vector3.MoveTowards(_currentVelocity, Vector3.zero, _deceleration * Time.deltaTime);
            }
        }
        else
        {
            if (dashCooldownTimer > 0)
            {
                dashCooldownTimer -= Time.deltaTime;
            }
        }
    }

    private void Look()
    {
        if (_input == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(_input, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
    }

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

    private void Move()
    {
        if (isMoving && !isDashing)
        {
            // Calculate target velocity based on input and speed
            Vector3 targetVelocity = _input.normalized * _speed;

            // Apply acceleration
            _currentVelocity = Vector3.MoveTowards(_currentVelocity, targetVelocity, _acceleration * Time.deltaTime);
        }
        else if (!isDashing)
        {
            // If not moving, apply deceleration
            _currentVelocity = Vector3.MoveTowards(_currentVelocity, Vector3.zero, _deceleration * Time.deltaTime);
        }

        // Move the rigidbody
        _rb.MovePosition(transform.position + _currentVelocity * Time.deltaTime);
    }
}



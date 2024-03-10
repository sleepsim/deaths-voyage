using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _rotationSpeed = 0.2f;
    [SerializeField] private float _turnSpeed = 45;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _acceleration = 2f; // Adjust this value for acceleration
    [SerializeField] private float _deceleration = 2f;
    private Vector3 _input;
    private Vector3 _currentVelocity;
    private bool isMoving;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    void FixedUpdate()
    {
        GatherInput();
        Move();
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
        if (isMoving)
        {
            // Calculate target velocity based on input and speed
            Vector3 targetVelocity = _input.normalized * _speed;

            // Apply acceleration
            _currentVelocity = Vector3.MoveTowards(_currentVelocity, targetVelocity, _acceleration * Time.deltaTime);

            // Move the rigidbody
            _rb.MovePosition(transform.position + _currentVelocity * Time.deltaTime);
        }
        else
        {
            // If not moving, apply deceleration
            _currentVelocity = Vector3.MoveTowards(_currentVelocity, Vector3.zero, _deceleration * Time.deltaTime);

            // Move the rigidbody
            _rb.MovePosition(transform.position + _currentVelocity * Time.deltaTime);
        }
    }
}



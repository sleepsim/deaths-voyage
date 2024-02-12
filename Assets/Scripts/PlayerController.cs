using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    private Vector3 _input;

    public GameObject targetEnemy;
    public float stoppingDistance;
    public Camera playerCamera;

    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void Update() {
        GatherInput();
        Look();
    }

    private void FixedUpdate() {
        Move();
    }

    // private void GatherInput() {
    //     _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    // }

    private void GatherInput() {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Get input relative to the camera's forward direction
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // _input = Input.GetAxisRaw("Horizontal") * cameraRight + Input.GetAxisRaw("Vertical") * cameraForward;

        _input = horizontalInput * cameraRight + verticalInput * cameraForward;
    }

    private void Look() {
        // if (_input == Vector3.zero) return;

        // var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);

        if(_input == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(_input, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
    }

    private void Move() {
        // _rb.MovePosition(transform.position + transform.forward * _input.normalized.magnitude * _speed * Time.deltaTime);

        _rb.MovePosition(transform.position + _input.normalized * _speed * Time.deltaTime);
    }
}

public static class Helpers 
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
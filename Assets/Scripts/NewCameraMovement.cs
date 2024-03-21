using UnityEngine;
using Cinemachine;

public class NewCameraMovement : MonoBehaviour
{
    public bool allowRotation = false; // Start with rotation disabled
    [SerializeField] public CinemachineFreeLook cinemachineFreeLook;
    [SerializeField] float yAxisSpeed = 2;
    [SerializeField] float xAxisSpeed = 200;

    void Start()
    {
        // cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        HandleCamera();
    }

    void HandleCamera()
    {
        if (Input.GetMouseButton(1)) // Check if right mouse button is clicked
        {
            allowRotation = true; // Enable rotation
            Cursor.visible = false;
        }
        else
        {
            allowRotation = true; // Disable rotation
            Cursor.visible = false;
        }

        if (allowRotation)
        {
            cinemachineFreeLook.m_XAxis.m_MaxSpeed = xAxisSpeed;
            cinemachineFreeLook.m_YAxis.m_MaxSpeed = yAxisSpeed;

        }
        else
        {
            cinemachineFreeLook.m_XAxis.m_MaxSpeed = 0;
            cinemachineFreeLook.m_YAxis.m_MaxSpeed = 0;
        }
    }
}
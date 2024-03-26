using UnityEngine;


public class TestBuoyancy : MonoBehaviour
{
    // The water level
    public float waterLevel = 0f;

    // The buoyancy force multiplier
    public float buoyancyForce = 10f;

    // The depth at which the object starts to be affected by buoyancy
    public float depthThreshold = 1f;

    // The rigidbody of the object
    private Rigidbody rb;

    void Start()
    {
        // Get the rigidbody component of the object
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Check if the object is below the water level
        if (transform.position.y < waterLevel)
        {
            // Calculate the depth of the object
            float depth = waterLevel - transform.position.y;

            // Check if the object is below the depth threshold
            if (depth < depthThreshold)
            {
                // Calculate the buoyancy force based on the depth
                float buoyancy = Mathf.Abs(Physics.gravity.y) * depth * buoyancyForce;

                // Apply the buoyancy force to the rigidbody
                rb.AddForceAtPosition(new Vector3(0, buoyancy, 0), transform.position, ForceMode.Force);
            }
        }
    }
}
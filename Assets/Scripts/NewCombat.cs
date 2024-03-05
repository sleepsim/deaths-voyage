using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCombat : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public float launchSpeed = 50f;
    public float rOffset = 1f;
    public float yOffset = 1f;
    private Stats playerStats;
    private int projectileNumber;


    void Start()
    {
        playerStats = GetComponent<Stats>();
        projectileNumber = playerStats.projectileNumber;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        Vector3 rightLaunchPosition = launchPoint.position + launchPoint.right * rOffset + Vector3.up * yOffset;
        Vector3 leftLaunchPosition = launchPoint.position - launchPoint.right * rOffset + Vector3.up * yOffset;
        // Instantiate the projectile at the launch point position and rotation

        for (int i = 0; i < projectileNumber; i++)
        {
            // Instantiate left and right projectiles
            GameObject leftProjectile = Instantiate(projectilePrefab, leftLaunchPosition, launchPoint.rotation);
            GameObject rightProjectile = Instantiate(projectilePrefab, rightLaunchPosition, launchPoint.rotation);

            // Access the Projectile script component of the newly instantiated projectile
            Projectile leftProjectileScript = leftProjectile.GetComponent<Projectile>();
            Projectile rightProjectileScript = rightProjectile.GetComponent<Projectile>();

            // Shoot the left projectile
            if (leftProjectileScript != null)
            {
                // Set the speed of the projectile using the launch speed variable
                leftProjectileScript.FireProjectile(launchSpeed, "left");
            }
            else
            {
                Debug.LogWarning("Projectile script not found!");
            }

            // Shoot the right projectile
            if (rightProjectileScript != null)
            {
                // Set the speed of the projectile using the launch speed variable
                rightProjectileScript.FireProjectile(launchSpeed, "right");
            }
            else
            {
                Debug.LogWarning("Projectile script not found!");
            }
        }

    }
}

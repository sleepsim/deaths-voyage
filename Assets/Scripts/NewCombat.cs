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
    public float upwardAngle = 30f;
    public float timeBetweenAttacks;
    private bool canAttack = true;
    public float timeUntilNextAttack = 0f;
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
        if (canAttack)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(AttackWithDelay());
            }
        }
        else
        {
            // Decrease the time until next attack
            timeUntilNextAttack -= Time.deltaTime;
        }
    }

    IEnumerator AttackWithDelay()
    {
        canAttack = false;
        FireProjectile();
        timeUntilNextAttack = timeBetweenAttacks;
        yield return new WaitForSeconds(timeBetweenAttacks);
        canAttack = true;
    }

    private void FireProjectile()
    {
        // Calculate left and right launch positions
        // Vector3 leftLaunchPosition = launchPoint.position - launchPoint.right * rOffset;
        Vector3 leftLaunchPosition = launchPoint.position - launchPoint.right * rOffset + Vector3.up * yOffset;
        Vector3 rightLaunchPosition = launchPoint.position + launchPoint.right * rOffset + Vector3.up * yOffset;

        Vector3 upwardDirection = new Vector3(0, upwardAngle, 0);

        // Instantiate the projectile at the launch point position and rotation
        for (int i = 0; i < projectileNumber; i++)
        {
            // Instantiate left and right projectiles
            GameObject leftProjectile = Instantiate(projectilePrefab, leftLaunchPosition, launchPoint.rotation);
            GameObject rightProjectile = Instantiate(projectilePrefab, rightLaunchPosition, launchPoint.rotation);

            leftProjectile.GetComponent<Projectile>().SetDamage(playerStats.damage);
            rightProjectile.GetComponent<Projectile>().SetDamage(playerStats.damage);

            // Access the Rigidbody component of the newly instantiated projectiles
            Rigidbody leftRigidbody = leftProjectile.GetComponent<Rigidbody>();
            Rigidbody rightRigidbody = rightProjectile.GetComponent<Rigidbody>();

            // Shoot the projectiles upwards before falling
            if (leftRigidbody != null && rightRigidbody != null)
            {
                // Calculate the initial velocity including the upward component
                Vector3 leftInitialVelocity = (-launchPoint.right + upwardDirection).normalized * launchSpeed;
                Vector3 rightInitialVelocity = (launchPoint.right + upwardDirection).normalized * launchSpeed;

                leftRigidbody.velocity = leftInitialVelocity;
                rightRigidbody.velocity = rightInitialVelocity;
            }
            else
            {
                Debug.LogWarning("Rigidbody component not found!");
            }

            // Set the attacker to ignore collisions with the projectile
            Physics.IgnoreCollision(GetComponent<Collider>(), leftProjectile.GetComponent<Collider>());
            Physics.IgnoreCollision(GetComponent<Collider>(), rightProjectile.GetComponent<Collider>());

        }
    }
}

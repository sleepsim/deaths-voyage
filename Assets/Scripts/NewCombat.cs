using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCombat : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;

    // Projectile Physics
    [Header("Projectile")]
    public float launchSpeed = 50f;
    public float rOffset = 1f;
    public float yOffset = 1f;
    public float upwardAngle = 30f;
    public float coneAngle = 30;

    [Header("Player")]
    public float timeBetweenAttacks;
    private bool canAttack = true;
    public float timeUntilNextAttack = 0f;
    private Stats playerStats;
    private int projectileNumber;


    // Ammo
    [Header("Ammo")]
    public int maxAmmo = 10;
    public int currentAmmo;
    public float ammoIncreaseInterval = 10f;
    public float ammoIncreaseTimer = 0;
    private bool ammoCoroutineActive = false;
    public float timeUntilAmmoRefill = 0;


    void Start()
    {
        playerStats = GetComponent<Stats>();
        currentAmmo = maxAmmo;
        StartCoroutine(RegenerateAmmo());
        timeUntilAmmoRefill = ammoIncreaseInterval;
    }
    // Update is called once per frame
    void Update()
    {
        // Update the number of projectiles
        projectileNumber = playerStats.projectileNumber;

        if (currentAmmo < maxAmmo)
        {
            timeUntilAmmoRefill -= Time.deltaTime;
        }

        if (canAttack && currentAmmo > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(AttackWithDelay());
                currentAmmo--;
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

    // private void FireProjectile()
    // {
    //     // Calculate left and right launch positions
    //     // Vector3 leftLaunchPosition = launchPoint.position - launchPoint.right * rOffset;

    //     Vector3 upwardDirection = new Vector3(0, upwardAngle, 0);

    //     // Instantiate the projectile at the launch point position and rotation
    //     for (int i = 0; i < projectileNumber; i++)
    //     {
    //         Debug.Log("I fired: " + projectileNumber);
    //         Vector3 leftLaunchPosition = launchPoint.position - (launchPoint.right * rOffset) + (Vector3.up * yOffset) + (Vector3.forward * zOffset * projectileNumber);
    //         Vector3 rightLaunchPosition = launchPoint.position + (launchPoint.right * rOffset) + (Vector3.up * yOffset) + (Vector3.forward * zOffset * projectileNumber);
    //         // Instantiate left and right projectiles
    //         GameObject leftProjectile = Instantiate(projectilePrefab, leftLaunchPosition, launchPoint.rotation);
    //         GameObject rightProjectile = Instantiate(projectilePrefab, rightLaunchPosition, launchPoint.rotation);

    //         leftProjectile.GetComponent<Projectile>().SetDamage(playerStats.damage);
    //         rightProjectile.GetComponent<Projectile>().SetDamage(playerStats.damage);

    //         // Access the Rigidbody component of the newly instantiated projectiles
    //         Rigidbody leftRigidbody = leftProjectile.GetComponent<Rigidbody>();
    //         Rigidbody rightRigidbody = rightProjectile.GetComponent<Rigidbody>();

    //         // Shoot the projectiles upwards before falling
    //         if (leftRigidbody != null && rightRigidbody != null)
    //         {
    //             // Calculate the initial velocity including the upward component
    //             Vector3 leftInitialVelocity = (-launchPoint.right + upwardDirection).normalized * launchSpeed;
    //             Vector3 rightInitialVelocity = (launchPoint.right + upwardDirection).normalized * launchSpeed;

    //             leftRigidbody.velocity = leftInitialVelocity;
    //             rightRigidbody.velocity = rightInitialVelocity;
    //         }
    //         else
    //         {
    //             Debug.LogWarning("Rigidbody component not found!");
    //         }

    //         // Set the attacker to ignore collisions with the projectile
    //         Physics.IgnoreCollision(GetComponent<Collider>(), leftProjectile.GetComponent<Collider>());
    //         Physics.IgnoreCollision(GetComponent<Collider>(), rightProjectile.GetComponent<Collider>());

    //     }
    // }

    private void FireProjectile()
    {
        // Calculate the angle between projectiles in the cone
        float angleStep = coneAngle / (projectileNumber - 1);

        // Loop through each projectile
        for (int i = 0; i < projectileNumber; i++)
        {
            // Calculate the angle for this projectile relative to the center of the cone
            float currentAngle = -coneAngle / 2f + angleStep * i;

            // Calculate the direction based on the angle and the upward angle
            Quaternion rotation = Quaternion.Euler(-upwardAngle, currentAngle, 0f);
            Vector3 direction = transform.rotation * rotation * Vector3.right;

            // Calculate the launch position for this projectile
            Vector3 rhorizontalOffset = launchPoint.right * rOffset;
            Vector3 lhorizontalOffset = -launchPoint.right * rOffset;
            Vector3 verticalOffset = transform.up * yOffset;
            Vector3 forwardOffset = transform.forward;
            Vector3 rlaunchPosition = launchPoint.position + rhorizontalOffset + verticalOffset + forwardOffset;
            Vector3 llaunchPosition = launchPoint.position + lhorizontalOffset + verticalOffset + forwardOffset;

            // Instantiate the projectile at the launch position
            GameObject rNewProjectile = Instantiate(projectilePrefab, rlaunchPosition, Quaternion.identity);
            GameObject lNewProjectile = Instantiate(projectilePrefab, llaunchPosition, Quaternion.identity);

            // Set the damage of the projectile
            rNewProjectile.GetComponent<Projectile>().SetDamage(playerStats.damage);
            lNewProjectile.GetComponent<Projectile>().SetDamage(playerStats.damage);

            // Access the Rigidbody component of the newly instantiated projectile
            Rigidbody rProjectileRigidbody = rNewProjectile.GetComponent<Rigidbody>();
            Rigidbody lProjectileRigidbody = lNewProjectile.GetComponent<Rigidbody>();

            // Shoot the projectile in the calculated direction
            if (rProjectileRigidbody != null && lProjectileRigidbody != null)
            {
                rProjectileRigidbody.velocity = direction.normalized * launchSpeed;
                lProjectileRigidbody.velocity = -direction.normalized * launchSpeed;
            }
            else
            {
                Debug.LogWarning("Rigidbody component not found!");
            }

            // Set the attacker to ignore collisions with the projectile
            Physics.IgnoreCollision(GetComponent<Collider>(), rNewProjectile.GetComponent<Collider>());
            Physics.IgnoreCollision(GetComponent<Collider>(), lNewProjectile.GetComponent<Collider>());
        }
    }

    IEnumerator RegenerateAmmo()
    {
        while (true)
        {
            yield return new WaitForSeconds(ammoIncreaseInterval);
            timeUntilAmmoRefill = ammoIncreaseInterval;
            if (currentAmmo < maxAmmo)
            {
                currentAmmo++;
            }
        }
    }

}

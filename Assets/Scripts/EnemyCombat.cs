using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : MonoBehaviour
{
    // Movement
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacks
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public Transform launchPoint;
    public float upwardsOffset;
    private Stats enemyStats;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public float rotationSpeed;

    private void Start()
    {
        enemyStats = GetComponent<Stats>();
    }
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);

            Vector3 direction = walkPoint - transform.position;

            // Create a rotation towards the walk point
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly interpolate between the current rotation and the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }

    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        // transform.LookAt(player);

        Vector3 direction = player.position - transform.position;
        // Quaternion targetRotation = Quaternion.LookRotation(direction);
        // transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (!alreadyAttacked)
        {
            Vector3 directionWithUpwardsOffset = direction + Vector3.up * upwardsOffset;
            //Attack code here
            GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);
            projectile.GetComponent<Projectile>().SetDamage(enemyStats.damage);

            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
            if (projectileRigidbody != null)
            {
                // projectileRigidbody.velocity = direction.normalized * projectileSpeed;
                projectileRigidbody.velocity = directionWithUpwardsOffset.normalized * projectileSpeed;
            }

            // Set the attacker to ignore collisions with the projectile
            Physics.IgnoreCollision(GetComponent<Collider>(), projectile.GetComponent<Collider>());

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void takeDamage(int damage)
    {
        // health -= damage

    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            // Debug.Log("On Patrol");
            Patrol();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            // Debug.Log("Chasing Player");
            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange)
        {
            // Debug.Log("Attacking PLayer");
            AttackPlayer();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// [RequireComponent(typeof(PlayerController)), RequireComponent(typeof(Stats))]

public class RangedCombat : MonoBehaviour
{
    private PlayerController moveScript;
    private Stats stats;
    private Animator anim;

    [Header("Target")]
    public GameObject targetEnemy;

    [Header("Melee Attack Variables")]
    public bool performRangedAttack = true;
    private float attackInterval;
    private float nextAttackTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<PlayerController>();
        stats = GetComponent<Stats>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        attackInterval = stats.attackSpeed / ((500 + stats.attackSpeed) * 0.01f);

        targetEnemy = moveScript.targetEnemy;

        if(targetEnemy != null && performRangedAttack && Time.time > nextAttackTime)
        {
            // if()
        }
    }
}

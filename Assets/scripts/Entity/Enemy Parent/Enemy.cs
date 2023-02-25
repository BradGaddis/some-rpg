using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Health))]
public class Enemy: MonoBehaviour, IDamageable
{
    [SerializeField]
    protected GameObject currentTarget;
    
    [Header("Enemy Stats")]
    // Enemy Health
    // Enemy Speed
    [SerializeField]
    protected float moveSpeed = 1f;
    // Enemy Damage
    [SerializeField]
    protected int damage = 10;
    // Enemy Attack Range
    [SerializeField]
    protected float attackRange = 1f;
    // Enemy Attack Speed
    private float attackTimer = 0f;
    [SerializeField]
    protected float attackSpeed = 1f;
    [SerializeField]
    protected float chaseRadius = 2f;

    [SerializeField]
    protected List<GameObject> targets = new List<GameObject>();

    [SerializeField]
    protected bool isBoss = false; // TODO Bosses

    [Header("Basic Enemy Attributes")]
    // Toggle enemy attributes
    [SerializeField]
    protected bool chasesTarget = false;

    [SerializeField]
    protected bool attacksTarget = false;
    [SerializeField]
    protected bool takesDamage = false;
    
    Health health;


    virtual protected void Start() {
        // resize circle collider to chase radius
        GetComponent<CircleCollider2D>().radius = chaseRadius;
        if (currentTarget == null) {
            currentTarget = GameObject.FindGameObjectWithTag("Player");
            targets.Add(currentTarget);
        }
        health = GetComponent<Health>();
    }

    virtual protected void Update() {
        if (targets.Contains(currentTarget)) {
            if (chasesTarget) {
                ChasePlayer(attackRange, chaseRadius, currentTarget);
            }
        }
    }

    // enemy type specific chase
    virtual protected void ChasePlayer(float attackRadius, float chaseRadius, GameObject target) {
    float distanceToTarget = GetDistanceToTarget(target.transform);
    if (distanceToTarget <= chaseRadius) {
        if (distanceToTarget <= attackRadius) {
            // If in range, stop moving, commence sequence flags
            switch (attacksTarget) {
                case true:
                    AttackTarget(target);
                    break;
                default:
                    Idle();
                    break;
            }    
        } else {
            // move towards the player's position
            Vector2 direction = (currentTarget.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        }
        }
    }

    private void Idle()
    {
    }

    virtual protected void AttackTarget(GameObject target) {
        // if attack timer is 0, attack
        if (attackTimer <= 0) {
            // attack
            DealDamage(damage);
            // reset attack timer
            attackTimer = attackSpeed;
        } else {
            // decrement attack timer
            attackTimer -= Time.deltaTime;
        }
    }

    virtual protected void DealDamage(float amount) {
        // deal damage to target
        // print remaining health
        PlayerHealth player = currentTarget.GetComponent<Health>() as PlayerHealth;
        if (player != null) {
            player.ReduceHealth(amount);
        }

    }


    // get distance to player
    virtual protected float GetDistanceToTarget(Transform target) {
        return Vector2.Distance(transform.position, target.transform.position);
    }

    virtual protected void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void TakeDamage(float damage)
    {
        health.ReduceHealth(damage);
    }

    public float GetHealth()
    {
        return -1;
    }
}
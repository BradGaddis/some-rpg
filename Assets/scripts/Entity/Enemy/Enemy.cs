using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField]
    protected GameObject currentTarget;
    
    [Header("Enemy Stats")]
    // Enemy Health
    [SerializeField]
    protected int health = 100;
    [SerializeField]
    protected int healthModifier = 1;
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
    

    virtual protected void Awake() {
        // set health
        health = health * healthModifier;
    }


    virtual protected void Start() {
        // resize circle collider to chase radius
        GetComponent<CircleCollider2D>().radius = chaseRadius;
    }

    virtual protected void Update() {
        if (targets.Contains(currentTarget)) {
            if (chasesTarget) {
                ChasePlayer(attackRange, chaseRadius, currentTarget);
            }
        }
        if (takesDamage) {
            if (health <= 0) {
                Die();
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
            Debug.Log("Attacking " + target.name);
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
        Debug.Log("Dealt " + amount + " damage to " + currentTarget.name);
        // print remaining health
        PlayerHealth player = currentTarget.GetComponent<Health>() as PlayerHealth;
        if (player != null) {
            Debug.Log(currentTarget.name + " has " + player.GetHealth() + " health remaining");
            player.TakeDamage(amount);
        }

    }

    // Take Damage
    virtual protected void TakeDamage(int damage) {
        health -= damage;
    }
    
    virtual protected void Die() {
        // Destroy enemy
        Destroy(gameObject);
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
}
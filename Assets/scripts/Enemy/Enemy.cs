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
    [SerializeField]
    protected float attackSpeed = 1f;
    [SerializeField]
    protected float chaseRadius = 2f;

    [SerializeField]
    protected List<GameObject> targets = new List<GameObject>();

    [SerializeField]
    protected bool isDead = false;

    [SerializeField]
    protected bool isBoss = false;

    [Header("Enemy Attributes")]
    // Toggle enemy attributes
    [SerializeField]
    protected bool chasesTarget = false;

    [SerializeField]
    protected bool attacksTarget = false;
    [SerializeField]
    protected bool takesDamage = false;
    

    private void Awake() {
        // set health
        health = health * healthModifier;
    }


    private void Start() {
        // resize circle collider to chase radius
        GetComponent<CircleCollider2D>().radius = chaseRadius;
    }

    virtual protected void Update() {
        if (targets.Contains(currentTarget)) {
            if (chasesTarget) {
                ChasePlayer(attackRange, chaseRadius, currentTarget);
            }
        }
    }

    // enemy type specific chase
    protected void ChasePlayer(float attackRadius, float chaseRadius, GameObject target) {
    float distanceToTarget = GetDistanceToTarget(target.transform);
    if (distanceToTarget <= chaseRadius) {
        if (distanceToTarget <= attackRadius) {
            AttackTarget();
            // Debug.Log("Attacking Player");
            } else {
                // move towards the player's position
                Vector2 direction = (currentTarget.transform.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    protected virtual void AttackTarget() {
        StartCoroutine(Attack());
    }

    protected virtual IEnumerator Attack() {
        // wait for attack speed
        yield return new WaitForSeconds(attackSpeed);
        // deal damage to player
        DealDamage(damage);
    }

    protected void DealDamage(float amount) {
    }

    // Take Damage
    protected void TakeDamage(int damage) {
        health -= damage;
    }
    
    // get distance to player
    float GetDistanceToTarget(Transform target) {
        return Vector2.Distance(transform.position, target.transform.position);
    }

    private void OnDrawGizmos() {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
    }
}
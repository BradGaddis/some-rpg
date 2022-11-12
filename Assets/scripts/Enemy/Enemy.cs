using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    // Enemy Health
    [SerializeField]
    protected int health = 100;
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
    Vector2 target;

    // Chase Player
    void ChasePlayer(float attackRadius, float chaseRadius, Vector2 target) {
        // get distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, target);
        // if player is within chase radius
        if(distanceToPlayer <= chaseRadius) {
            // if player is within attack radius
            if(distanceToPlayer <= attackRadius) {
                // attack player
                AttackPlayer();
            } else {
                // chase player
                ChasePlayer();
            }
        }
    }

    // enemy type specific chase
    void ChasePlayer() {
        
    }

    // Attack Player
    void AttackPlayer() {
        StartCoroutine(Attack());
    }

    IEnumerator Attack() {
        // wait for attack speed
        yield return new WaitForSeconds(attackSpeed);
        
        // deal damage to player
        DealDamage(damage);
    }

    void DealDamage(float amount) {
        
    }

    // Take Damage
    public void TakeDamage(int damage) {
        health -= damage;
        Debug.Log("Enemy Health: " + health);
    }
    
    // get distance to player
    float GetDistanceToPlayer() {
        return Vector2.Distance(transform.position, target);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Collider might not work the way that I intended it. I used a trigger, but apparently you need some sort of movement to trigger the trigger. I'll have to look into this more.

//  THIS CLASS IS A PARENT CLASS TODO: MAKE THIS A PARENT CLASS


public class PlayerAttack : MonoBehaviour
{
    PlayerAnimationHandler playerAnimation;
    // Collider2D[] attackHitboxes;
    [SerializeField] float attackDuration = 2f;
    [SerializeField] float attackDamage = 10f;
    [SerializeField] float impactTime = 0.5f;
    private float impactTimer = 0f;
    private CircleCollider2D attackCollider;
    private bool canAttack = true;
    private bool isAttacking = false;
    private bool wasHit = false;

    

    protected void Start() {
        playerAnimation = GetComponentInParent<PlayerAnimationHandler>();
        // TEMPORARY
        // attackCollider = attackHitboxes[Array.FindIndex(attackHitboxes,gameo => gameo.gameObject.name == "Forward Punch") ];
        // attackObject.SetActive(false);
        attackCollider = this.gameObject.GetComponent<CircleCollider2D>();
        // attackCollider.enabled = false;
        impactTimer = impactTime;
    }

    virtual protected void Update() {
        // use V key to attack
        DoAttack();
    }

    virtual public void DoAttack() {
        if (Input.GetKeyDown(KeyCode.V) && canAttack) {
            canAttack = false;
            // isAttacking = true;
            playerAnimation.StartForwardPunchAttack();
            StartCoroutine(AttackDuration());
        }
    }

    virtual protected IEnumerator AttackDuration() {
        yield return new WaitForSeconds(impactTime);
        // This is the actual duration of the attack 
        isAttacking = true;
        AttackEnemy();
        yield return new WaitForSeconds(attackDuration);
        // This is the end of the attack
        canAttack = true;
        isAttacking = false;
        wasHit = false;
    }

    // virtual protected  void GetAttackHitboxes() {
    //     attackHitboxes = GetComponentsInChildren<Collider2D>();
    // }

    virtual public void DealDamage(float damage, Enemy enemy) {
        Health enemyHealth = enemy.GetComponent<Health>();
        enemyHealth.TakeDamage(damage);
    }

    virtual protected void AttackEnemy(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCollider.bounds.center, attackCollider.radius);
        // find the enemy by components 
        foreach (Collider2D collider in colliders) {
            Component[] components = collider.gameObject.GetComponents(typeof(Component));
            Enemy enemy = null;
            foreach (Component component in components) {
                if (component as Enemy && isAttacking && !wasHit){
                    enemy = component as Enemy;
                    wasHit = true;          
                    DealDamage(attackDamage, enemy);
                    break;
                }
            }
        }

    }    


    private IEnumerator ImpactTimer() {
        yield return new WaitForSeconds(impactTime);
    }
}
